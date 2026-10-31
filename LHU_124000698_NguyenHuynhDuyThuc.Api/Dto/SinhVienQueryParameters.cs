using LHU_124000698_NguyenHuynhDuyThuc.Api.Dto.Common;

namespace LHU_124000698_NguyenHuynhDuyThuc.Api.Dto
{
    public class SinhVienQueryParameters : PaginationParams
    {
        // Từ khóa tìm kiếm gần đúng trên Mã SV, Họ tên hoặc Email
        public string? Search { get; set; }

        // Lọc chính xác theo tên Lớp sinh hoạt (ví dụ: "24CT113")
        public string? LopSinhHoat { get; set; }

        // Cột cần sắp xếp (mặc định: "HoTen")
        public string? SortBy { get; set; } = "HoTen";

        // Hướng sắp xếp: true = Tăng dần (A-Z), false = Giảm dần (Z-A)
        public bool IsAscending { get; set; } = true;
    }
}
