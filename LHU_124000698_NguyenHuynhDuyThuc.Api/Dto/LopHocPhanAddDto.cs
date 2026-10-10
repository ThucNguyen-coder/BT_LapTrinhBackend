using System.ComponentModel.DataAnnotations;

namespace LHU_124000698_NguyenHuynhDuyThuc.Api.Dto
{
    public class LopHocPhanAddDto
    {
        public int MonHocID { get; set; }
        public string MaLopHP { get; set; } = null!;
        public int HocKy { get; set; }
        public int NamHoc { get; set; }
        public int SiSoToiDa { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (NgayBatDau != null && NgayKetThuc != null)
            {
                if (NgayKetThuc <= NgayBatDau)
                {
                    yield return new ValidationResult("Ngày k?t thúc ph?i l?n h?n ngày b?t ??u");
                }
            }
        }
    }
}
