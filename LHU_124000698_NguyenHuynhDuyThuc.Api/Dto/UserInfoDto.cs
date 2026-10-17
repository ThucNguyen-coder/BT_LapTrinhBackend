namespace LHU_124000698_NguyenHuynhDuyThuc.Api.Dto
{
    public class UserInfoDto
    {
        public int Id { get; set; }
        public string TenDangNhap { get; set; } = string.Empty;
        public string HoTen { get; set; } = string.Empty;
        public string VaiTro { get; set; } = string.Empty;
        public int? SinhVienId { get; set; }
    }
}
