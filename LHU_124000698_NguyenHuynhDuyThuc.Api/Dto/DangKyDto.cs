namespace LHU_124000698_NguyenHuynhDuyThuc.Api.Dto
{
    public class DangKyDto
    {
        public int DangKyId { get; set; }

        // Thông tin Sinh viên
        public int SinhVienId { get; set; }
        public string MaSV { get; set; } = string.Empty;
        public string HoTen { get; set; } = string.Empty;

        // Thông tin Lớp học phần & Môn học
        public int LopHocPhanId { get; set; }
        public string MaLopHP { get; set; } = string.Empty;
        public string TenMon { get; set; } = string.Empty;
        public int SoTinChi { get; set; }

        // Thông tin Đăng ký
        public DateTime NgayDangKy { get; set; }
        public string TrangThai { get; set; } = string.Empty;
    }
}
