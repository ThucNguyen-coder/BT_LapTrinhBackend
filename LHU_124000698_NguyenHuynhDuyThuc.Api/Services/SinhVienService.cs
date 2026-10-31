using LHU_124000698_NguyenHuynhDuyThuc.Api.Database;
using LHU_124000698_NguyenHuynhDuyThuc.Api.Dto;
using LHU_124000698_NguyenHuynhDuyThuc.Api.Dto.Common;
using Microsoft.EntityFrameworkCore;

namespace LHU_124000698_NguyenHuynhDuyThuc.Api.Services
{
    public class SinhVienService
    {
        private readonly DatabaseContext db;

        public SinhVienService(DatabaseContext db)
        {
            this.db = db;
        }

        public async Task<PagedResult<SinhVienDto>> GetPagedAsync(SinhVienQueryParameters query)
        {
            // Bước 1: Khởi tạo IQueryable chỉ đọc với AsNoTracking() (giải phóng RAM Change Tracker)
            var queryable = db.sinhVien.AsNoTracking().AsQueryable();

            // Bước 2: Tìm kiếm mềm (Search) trên nhiều trường
            if (!string.IsNullOrWhiteSpace(query.Search))
            {
                var keyword = query.Search.Trim().ToLower();
                queryable = queryable.Where(s =>
                    s.MaSV.ToLower().Contains(keyword) ||
                    s.HoTen.ToLower().Contains(keyword) ||
                    (s.Email != null && s.Email.ToLower().Contains(keyword)));
            }

            // Lọc theo lớp sinh hoạt (Filter)
            if (!string.IsNullOrWhiteSpace(query.LopSinhHoat))
            {
                var lop = query.LopSinhHoat.Trim().ToLower();
                queryable = queryable.Where(s => s.LopSinhHoat.ToLower() == lop);
            }

            // Bước 3: Sắp xếp an toàn theo Whitelist các cột cho phép (chống SQL Injection / Crash)
            queryable = query.SortBy?.Trim().ToLower() switch
            {
                "masv" => query.IsAscending
                    ? queryable.OrderBy(s => s.MaSV)
                    : queryable.OrderByDescending(s => s.MaSV),
                "ngaysinh" => query.IsAscending
                    ? queryable.OrderBy(s => s.NgaySinh)
                    : queryable.OrderByDescending(s => s.NgaySinh),
                "lopsinhhoat" => query.IsAscending
                    ? queryable.OrderBy(s => s.LopSinhHoat)
                    : queryable.OrderByDescending(s => s.LopSinhHoat),
                _ => query.IsAscending
                    ? queryable.OrderBy(s => s.HoTen)
                    : queryable.OrderByDescending(s => s.HoTen)
            };

            // Bước 4: Đếm tổng số bản ghi thỏa điều kiện lọc tại SQL Server
            var totalCount = await queryable.CountAsync();

            // Bước 5: Cắt trang (Paging) và Projection sang DTO (triệt tiêu Over-fetching)
            var items = await queryable
                .Skip((query.PageIndex - 1) * query.PageSize)
                .Take(query.PageSize)
                .Select(s => new SinhVienDto
                {
                    SinhVienId = s.SinhVienID,
                    MaSV = s.MaSV,
                    HoTen = s.HoTen,
                    Email = s.Email,
                    NgaySinh = s.NgaySinh,
                    LopSinhHoat = s.LopSinhHoat
                })
                .ToListAsync();

            return new PagedResult<SinhVienDto>(items, totalCount, query.PageIndex, query.PageSize);
        }
    }
}
