using LHU_124000698_NguyenHuynhDuyThuc.Api.Dto.Common;

namespace LHU_124000698_NguyenHuynhDuyThuc.Api.Dto
{
    public class DangKyQueryParameters : PaginationParams
    {
        public int? SinhVienId { get; set; }

        // Lọc theo Lớp học phần
        public int? LopHocPhanId { get; set; }

        // Lọc theo Trạng thái (ví dụ: "ThanhCong", "Huy", ...)
        public string? TrangThai { get; set; }

        // Lọc theo khoảng ngày đăng ký
        public DateTime? TuNgay { get; set; }
        public DateTime? DenNgay { get; set; }

        // Cột cần sắp xếp (mặc định: "NgayDangKy")
        public string? SortBy { get; set; } = "NgayDangKy";

        // Hướng sắp xếp: true = Tăng dần, false = Giảm dần
        public bool IsAscending { get; set; } = false;
    }
}
