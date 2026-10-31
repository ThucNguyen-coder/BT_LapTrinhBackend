using LHU_124000698_NguyenHuynhDuyThuc.Api.Database;
using LHU_124000698_NguyenHuynhDuyThuc.Api.Dto;
using LHU_124000698_NguyenHuynhDuyThuc.Api.Dto.Common;
using Microsoft.EntityFrameworkCore;

namespace LHU_124000698_NguyenHuynhDuyThuc.Api.Services
{
    public class LopHocPhanService
    {
        private readonly DatabaseContext db;

        public LopHocPhanService(DatabaseContext context)
        {
            db = context;
        }

        public async Task<PagedResult<LopHocPhanDto>> GetPagedAsync(
            LopHocPhanQueryParameters query)
        {
            var queryable = db.lopHocPhan.AsNoTracking().AsQueryable();

            // Tìm kiếm theo Mã Lớp HP hoặc Tên môn học (truy xuất qua Navigation Property L.MonHoc)
            if (!string.IsNullOrWhiteSpace(query.Search))
            {
                var keyword = query.Search.Trim().ToLower();
                queryable = queryable.Where(l =>
                    l.MaLopHP.ToLower().Contains(keyword) ||
                    l.MonHoc.TenMon.ToLower().Contains(keyword));
            }

            if (query.HocKy.HasValue)
                queryable = queryable.Where(l => l.HocKy == query.HocKy.Value);

            if (query.NamHoc.HasValue)
                queryable = queryable.Where(l => l.NamHoc == query.NamHoc.Value);

            queryable = query.SortBy?.Trim().ToLower() switch
            {
                "tenmon" => query.IsAscending
                    ? queryable.OrderBy(l => l.MonHoc.TenMon)
                    : queryable.OrderByDescending(l => l.MonHoc.TenMon),
                "hocky" => query.IsAscending
                    ? queryable.OrderBy(l => l.HocKy)
                    : queryable.OrderByDescending(l => l.HocKy),
                "namhoc" => query.IsAscending
                    ? queryable.OrderBy(l => l.NamHoc)
                    : queryable.OrderByDescending(l => l.NamHoc),
                "sisotoida" => query.IsAscending
                    ? queryable.OrderBy(l => l.SiSoToiDa)
                    : queryable.OrderByDescending(l => l.SiSoToiDa),
                _ => query.IsAscending
                    ? queryable.OrderBy(l => l.MaLopHP)
                    : queryable.OrderByDescending(l => l.MaLopHP)
            };

            var totalCount = await queryable.CountAsync();

            // Projection DTO: EF Core tự động dịch L.MonHoc thành INNER JOIN trong SQL duy nhất
            var items = await queryable
                .Skip((query.PageIndex - 1) * query.PageSize)
                .Take(query.PageSize)
                .Select(l => new LopHocPhanDto
                {
                    LopHocPhanID = l.LopHocPhanID,
                    MonHocID = l.MonHocID,
                    MaLopHP = l.MaLopHP,
                    TenMon = l.MonHoc.TenMon,       // EF Core tự sinh INNER JOIN MonHoc
                    SoTinChi = l.MonHoc.SoTinChi,   // Không cần gọi hàm Join thủ công
                    HocKy = l.HocKy,
                    NamHoc = l.NamHoc,
                    SiSoToiDa = l.SiSoToiDa
                })
                .ToListAsync();

            return new PagedResult<LopHocPhanDto>(
                items, totalCount, query.PageIndex, query.PageSize);
        }
        public async Task<List<LopHocPhanThongKeDto>> ThongKeSiSoAsync()
        {
            // EF Core tự biên dịch l.DangKies.Count() thành subquery COUNT(*) trong SQL
            return await db.lopHocPhan
                .AsNoTracking()
                .Select(l => new LopHocPhanThongKeDto
                {
                    LopHocPhanId = l.LopHocPhanID,  // Đảm bảo hoa/thường theo đúng Entity LopHocPhan của bạn
            MaLopHP = l.MaLopHP,
                    TenMon = l.MonHoc.TenMon,
                    SiSoToiDa = l.SiSoToiDa,
                    SoLuongDaDangKy = l.dangKys.Count() // Hàm đếm trên Navigation tập hợp
        })
                .ToListAsync();
        }
    }
}
