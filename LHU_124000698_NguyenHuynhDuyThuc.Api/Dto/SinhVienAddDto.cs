using System.ComponentModel.DataAnnotations;

namespace LHU_124000698_NguyenHuynhDuyThuc.Api.Dto
{
    public class SinhVienAddDto
    {
        [Key]
        public string MaSV { get; set; } = null!;
        [Required(ErrorMessage = "Vui lòng nhập họ tên")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Họ tên từ 2 -> 50 ký tự")]
        public string HoTen { get; set; } = null!;
        [EmailAddress]
        public string? Email { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string? LopSinhHoat { get; set; }
    }
}
