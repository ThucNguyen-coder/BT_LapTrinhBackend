using System.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using LHU_124000698_NguyenHuynhDuyThuc.Api.Dto;
using Dapper;
using Microsoft.AspNetCore.Authorization;

namespace LHU_124000698_NguyenHuynhDuyThuc.Api.Controllers
{
    [Route("api/dang-ky")]
    [ApiController]
    public class DangKyController : ControllerBase
    {
        SqlConnection cn;

        public DangKyController(SqlConnection connection)
        {
            cn = connection;
        }

        [Authorize(Roles = "SinhVien")]
        [HttpPost("them-moi")]
        public async Task<IActionResult> ThemMoi([FromBody] DangKyHocPhanAnToanDto req)
        {
            // BẢO VỆ CHỐNG IDOR: Trích xuất SinhVienId trực tiếp từ Token đã được ký số!
            var sinhVienIdClaim = User.FindFirst("SinhVienId")?.Value;
            if (string.IsNullOrEmpty(sinhVienIdClaim))
            {
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

            if (ketQua == 1) return Ok(new { message = "Đăng ký học phần thành công!", lopHocPhanId = req.LopHocPhanId, sinhVienId = currentSinhVienId });

            return BadRequest(new { message = thongBao });
        }
    }
}
