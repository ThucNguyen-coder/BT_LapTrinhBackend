namespace LHU_124000698_NguyenHuynhDuyThuc.Api.Dto
{
    public class LopHocPhanAddDto
    {
        public int MonHocID { get; set; }
        public string MaLopHP { get; set; } = null!;
        public int HocKy { get; set; }
        public int NamHoc { get; set; }
        public int SiSoToiDa { get; set; }
    }
}
