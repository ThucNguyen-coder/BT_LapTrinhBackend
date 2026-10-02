namespace LHU_124000698_NguyenHuynhDuyThuc.Api.Dto
{
    public class SinhVienAddDto
    {
        public string MaSV { get; set; } = null!;
        public string HoTen { get; set; } = null!;
        public string? Email { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string? LopSinhHoat { get; set; }
    }
}
