namespace LHU_124000698_NguyenHuynhDuyThuc.Api.Dto
{
    public class LoginResponseDto
    {
        public string Token { get; set; } = string.Empty;
        public DateTime ExpiresAt { get; set; }
        public string HoTen { get; set; } = string.Empty;
        public string VaiTro { get; set; } = string.Empty;
        public int? SinhVienId { get; set; }
    }
}
