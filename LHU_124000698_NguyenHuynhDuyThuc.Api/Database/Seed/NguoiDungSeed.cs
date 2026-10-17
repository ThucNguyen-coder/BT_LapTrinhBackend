using LHU_124000698_NguyenHuynhDuyThuc.Api.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LHU_124000698_NguyenHuynhDuyThuc.Api.Database.Seed
{
    public class NguoiDungSeed : IEntityTypeConfiguration<NguoiDungEntity>
    {
        public void Configure(EntityTypeBuilder<NguoiDungEntity> builder)
        {
            builder.HasData(Data);
        }

        public static readonly NguoiDungEntity[] Data = new[]
        {
            new NguoiDungEntity
            {
                Id = 1,
                TenDangNhap = "daotao",
                MatKhauHash = BCrypt.Net.BCrypt.HashPassword("MatKhau@123"), // MatKhau@123
                VaiTro = "DaoTao",
                SinhVienId = null,
                GiangVienId = null
            },
            new NguoiDungEntity
            {
                Id = 2,
                TenDangNhap = "giangvien",
                MatKhauHash = BCrypt.Net.BCrypt.HashPassword("MatKhau@123"),
                VaiTro = "GiangVien",
                SinhVienId = null,
                GiangVienId = 1
            },
            new NguoiDungEntity
            {
                Id = 3,
                TenDangNhap = "sinhvien",
                MatKhauHash = BCrypt.Net.BCrypt.HashPassword("MatKhau@123"),
                VaiTro = "SinhVien",
                SinhVienId = 1,
                GiangVienId = null
            }
        };
    }
}
