using LHU_124000698_NguyenHuynhDuyThuc.Api.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LHU_124000698_NguyenHuynhDuyThuc.Api.Database.Seed
{
    public class LopHocPhanSeed : IEntityTypeConfiguration<LopHocPhanEntity>
    {
        public void Configure(EntityTypeBuilder<LopHocPhanEntity> builder)
        {
            builder.HasData(Data);
        }

        public static readonly LopHocPhanEntity[] Data = new[]
        {
        new LopHocPhanEntity
        {
            LopHocPhanID = 5,
            MonHocID = 1,
            MaLopHP = "IT101-01",
            HocKy = 1,
            NamHoc = 2025,
            SiSoToiDa = 50,
            NgayBatDau = new DateTime(2025, 9, 1),
            NgayKetThuc = new DateTime(2025, 12, 20)
        },
        new LopHocPhanEntity
        {
            LopHocPhanID = 6,
            MonHocID = 2,
            MaLopHP = "IT102-01",
            HocKy = 1,
            NamHoc = 2025,
            SiSoToiDa = 45,
            NgayBatDau = new DateTime(2025, 9, 2),
            NgayKetThuc = new DateTime(2025, 12, 21)
        }
    };
    }
}
