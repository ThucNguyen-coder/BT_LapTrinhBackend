using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LHU_124000698_NguyenHuynhDuyThuc.Api.Database.Entities
{
    [Table("LopHocPhan")]
    public class LopHocPhanEntity
    {
        [Key]
        public int LopHocPhanID { get; set; }
        public int MonHocID { get; set; }
        [Required]
        [StringLength(20)]
        public string MaLopHP { get; set; } = null!;
        public int HocKy { get; set; }
        public int NamHoc { get; set; }
        public int SiSoToiDa { get; set; }
        [ForeignKey(nameof(MonHocID))]
        public MonHocEntity MonHoc { get; set; } = null!;
        public ICollection<DangKyEntity> dangKys { get; set; } = new List<DangKyEntity>();
    }
}
