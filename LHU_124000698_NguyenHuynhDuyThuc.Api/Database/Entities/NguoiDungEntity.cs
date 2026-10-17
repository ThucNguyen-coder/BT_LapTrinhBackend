using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LHU_124000698_NguyenHuynhDuyThuc.Api.Database.Entities
{
    [Table("NguoiDung")]
    [Index(nameof(TenDangNhap), IsUnique = true)]
    public class NguoiDungEntity
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(50)]
        public string TenDangNhap { get; set; } = null!;
        [Required, StringLength(255)]
        public string MatKhauHash { get; set; } = null!; // Chuỗi băm an toàn từ BCrypt
        [Required, StringLength(20)]
        public string VaiTro { get; set; } = null!; // "SinhVien", "GiangVien", "DaoTao"
        public int? SinhVienId { get; set; }
        [ForeignKey(nameof(SinhVienId))]
        public SinhVienEntity? SinhVien { get; set; }
        public int? GiangVienId { get; set; }
    }
}
