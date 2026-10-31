using LHU_124000698_NguyenHuynhDuyThuc.Api.Dto.Common;

namespace LHU_124000698_NguyenHuynhDuyThuc.Api.Dto
{
    public class LopHocPhanQueryParameters : PaginationParams
    {
        // Từ khóa tìm kiếm theo Mã Lớp HP hoặc Tên môn học
        public string? Search { get; set; }

        // Lọc theo Học kỳ
        public int? HocKy { get; set; }

        // Lọc theo Năm học
        public int? NamHoc { get; set; }

        // Cột cần sắp xếp (mặc định: "MaLopHP")
        public string? SortBy { get; set; } = "MaLopHP";

        // Hướng sắp xếp: true = Tăng dần (A-Z), false = Giảm dần (Z-A)
        public bool IsAscending { get; set; } = true;
    }
}
