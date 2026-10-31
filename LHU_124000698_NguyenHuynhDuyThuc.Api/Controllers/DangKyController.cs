using System.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using LHU_124000698_NguyenHuynhDuyThuc.Api.Dto;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging; // Đã thêm để dùng ILogger
using LHU_124000698_NguyenHuynhDuyThuc.Api.Dto.Common;
using LHU_124000698_NguyenHuynhDuyThuc.Api.Database;

namespace LHU_124000698_NguyenHuynhDuyThuc.Api.Controllers
{
    [Route("api/dang-ky")]
    [ApiController]
    public class DangKyController : ControllerBase
    {
        private readonly SqlConnection cn;
        private readonly ILogger<DangKyController> _logger; // Đã thêm khai báo _logger
        private readonly DatabaseContext db;
        // Inject thêm ILogger vào Constructor
        public DangKyController(SqlConnection connection, DatabaseContext context, ILogger<DangKyController> logger)
        {
            cn = connection;
            _logger = logger;
            db = context;
        }

        [Authorize(Roles = "SinhVien")]
        // Bỏ attribute [EnableRateLimiting] vì dự án dùng AspNetCoreRateLimit qua appsettings.json
        [HttpPost("them-moi")]
        public async Task<IActionResult> ThemMoi([FromBody] DangKyHocPhanAnToanDto req)
        {
            // BẢO VỆ CHỐNG IDOR: Trích xuất SinhVienId trực tiếp từ Token đã được ký số!
            var sinhVienIdClaim = User.FindFirst("SinhVienId")?.Value;
            if (string.IsNullOrEmpty(sinhVienIdClaim))
            {
                _logger.LogWarning("Yêu cầu đăng ký học phần bị từ chối do thiếu Claim SinhVienId trong Token.");
                return Unauthorized("Token không hợp lệ hoặc thiếu thông tin định danh sinh viên.");
            }

            int currentSinhVienId = int.Parse(sinhVienIdClaim);

            // Thực hiện đăng ký đúng cho Sinh viên đang đăng nhập
            var param = new DynamicParameters();
            param.Add("@SinhVienId", currentSinhVienId, dbType: DbType.Int32);
            param.Add("@LopHocPhanId", req.LopHocPhanId, dbType: DbType.Int32);
            param.Add("@KetQua", dbType: DbType.Int32, direction: ParameterDirection.Output);
            param.Add("@ThongBao", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);

            await cn.ExecuteAsync("sp_DangKyHocPhan", param, commandType: CommandType.StoredProcedure);

            int ketQua = param.Get<int>("@KetQua");
            string thongBao = param.Get<string>("@ThongBao");

            if (ketQua == 1)
            {
                _logger.LogInformation("Sinh viên {SinhVienId} đăng ký thành công lớp {LopHocPhanId}",
                    currentSinhVienId, req.LopHocPhanId);
                return Ok(new
                {
                    message = "Đăng ký học phần thành công!",
                    lopHocPhanId = req.LopHocPhanId,
                    sinhVienId = currentSinhVienId
                });
            }

            return BadRequest(new { message = thongBao });
        }

        [Authorize(Roles = "GiangVien,DaoTao")]
        [HttpGet("danh-sach-paged")]
        public async Task<ActionResult<PagedResult<DangKyDto>>> GetDanhSachPaged(
            [FromQuery] DangKyQueryParameters query)
        {
            var queryable = db.dangKy.AsNoTracking().AsQueryable();

            if (query.SinhVienId.HasValue)
                queryable = queryable.Where(d => d.SinhVienId == query.SinhVienId.Value);

            if (query.LopHocPhanId.HasValue)
                queryable = queryable.Where(d => d.LopHocPhanId == query.LopHocPhanId.Value);

            if (!string.IsNullOrWhiteSpace(query.TrangThai))
            {
                var tt = query.TrangThai.Trim().ToLower();
                queryable = queryable.Where(d => d.TrangThai.ToLower() == tt);
            }

            // Lọc theo khoảng ngày đăng ký
            if (query.TuNgay.HasValue)
                queryable = queryable.Where(d => d.NgayDangKy >= query.TuNgay.Value);

            if (query.DenNgay.HasValue)
                queryable = queryable.Where(d => d.NgayDangKy <= query.DenNgay.Value);

            queryable = query.SortBy?.Trim().ToLower() switch
            {
                "masv" => query.IsAscending
                    ? queryable.OrderBy(d => d.SinhVien.MaSV)
                    : queryable.OrderByDescending(d => d.SinhVien.MaSV),
                "malophp" => query.IsAscending
                    ? queryable.OrderBy(d => d.LopHocPhan.MaLopHP)
                    : queryable.OrderByDescending(d => d.LopHocPhan.MaLopHP),
                _ => query.IsAscending
                    ? queryable.OrderBy(d => d.NgayDangKy)
                    : queryable.OrderByDescending(d => d.NgayDangKy)
            };

            var totalCount = await queryable.CountAsync();

            // Truy xuất đa cấp: d.SinhVien, d.LopHocPhan.MonHoc (EF Core tự sinh JOIN 3 bảng)
            var items = await queryable
                .Skip((query.PageIndex - 1) * query.PageSize)
                .Take(query.PageSize)
                .Select(d => new DangKyDto
                {
                    DangKyId = d.DangKyId,
                    SinhVienId = d.SinhVienId,
                    MaSV = d.SinhVien.MaSV,
                    HoTen = d.SinhVien.HoTen,
                    LopHocPhanId = d.LopHocPhanId,
                    MaLopHP = d.LopHocPhan.MaLopHP,
                    TenMon = d.LopHocPhan.MonHoc.TenMon,      // Navigation cấp 3: DangKy -> LopHP -> MonHoc
                    SoTinChi = d.LopHocPhan.MonHoc.SoTinChi,
                    NgayDangKy = d.NgayDangKy,
                    TrangThai = d.TrangThai
                })
                .ToListAsync();

            return Ok(new PagedResult<DangKyDto>(
                items, totalCount, query.PageIndex, query.PageSize));
        }
    }
}