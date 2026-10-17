using LHU_124000698_NguyenHuynhDuyThuc.Api.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LHU_124000698_NguyenHuynhDuyThuc.Api.Database.Seed
{
    public class MonHocSeed : IEntityTypeConfiguration<MonHocEntity>
    {
        public void Configure(EntityTypeBuilder<MonHocEntity> builder)
        {
            builder.HasData(Data);
        }

        public static readonly MonHocEntity[] Data = new[]
        {
        new MonHocEntity
        {
            MonHocID = 7,
            MaMon = "IT101",
            TenMon = "Nhập môn lập trình",
            SoTinChi = 3,
            SoTietLyThuyet = 30
        },
        new MonHocEntity
        {
            MonHocID = 8,
            MaMon = "IT102",
            TenMon = "Kỹ thuật lập trình",
            SoTinChi = 3,
            SoTietLyThuyet = 30
        },
        new MonHocEntity
        {
            MonHocID = 9,
            MaMon = "IT201",
            TenMon = "Cấu trúc dữ liệu",
            SoTinChi = 3,
            SoTietLyThuyet = 30
        }
    };
    }
}
