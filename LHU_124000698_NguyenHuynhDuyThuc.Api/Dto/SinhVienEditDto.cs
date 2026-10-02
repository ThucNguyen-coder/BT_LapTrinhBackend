namespace LHU_124000698_NguyenHuynhDuyThuc.Api.Dto
{
    public class SinhVienEditDto
    {
        public int SinhVienId { get; set; }
        public string MaSV { get; set; } = null!;
        public string HoTen { get; set; } = null!;
        public string? Email { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string? LopSinhHoat { get; set; }
    }
}
