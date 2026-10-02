using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LHU_124000698_NguyenHuynhDuyThuc.Api.Database.Entities
{
    [Table("SinhVien")]
    public class SinhVienEntity
    {
        [Key]
        public int SinhVienID { get; set; }
        [Required]
        [MaxLength(20)]
        public string MaSV { get; set; } = null!;
        [Required]
        [MaxLength(100)]
        public string HoTen { get; set; } = null!;
        [MaxLength(100)]
        public string Email { get; set; } = "";
        public DateTime? NgaySinh { get; set; } = null;
        [StringLength(50)]
        public string LopSinhHoat { get; set; } = "";
        public ICollection<DangKyEntity> dangKys { get; set; } = new List<DangKyEntity>();
    }
}
