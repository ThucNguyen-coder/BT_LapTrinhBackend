namespace LHU_124000698_NguyenHuynhDuyThuc.Api.Dto
{
    public class LopHocPhanDto
    {
        public int LopHocPhanID { get; set; }
        public int MonHocID { get; set; }
        public string MaLopHP { get; set; } = null!;
        public string TenMon { get; set; } = string.Empty;
        public int SoTinChi { get; set; }
        public int HocKy { get; set; }
        public int NamHoc { get; set; }
        public int SiSoToiDa { get; set; }
    }
}
