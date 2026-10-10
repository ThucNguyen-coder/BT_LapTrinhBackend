using System.ComponentModel.DataAnnotations;

namespace LHU_124000698_NguyenHuynhDuyThuc.Api.Dto;

public class MonHocAddDto
{
    [Required(ErrorMessage = "Vui lòng nhập mã môn học")]
    [StringLength(20, MinimumLength = 6, ErrorMessage = "Mã môn học từ 6 -> 20 ký tự")]
    public string MaMon { get; set; } = null!;
    [Required(ErrorMessage = "Vui lòng nhập tên môn học")]
    [MaxLength(100, ErrorMessage = "Tên môn học tối đa 100 ký tự")]
    public string TenMon { get; set; } = null!;
    [Range(1, 10, ErrorMessage = "Giá trị số tín chỉ từ 1 -> 10")]
    public int SoTinChi { get; set; }
    [Range(0, 100, ErrorMessage = "Giá trị số tiết lý thuyết 0 -> 100")]
    public int SoTietLyThuyet { get; set; }
}
