using LHU_124000698_NguyenHuynhDuyThuc.Api.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace LHU_124000698_NguyenHuynhDuyThuc.Api.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }
        public DbSet<MonHocEntity> MonHoc => Set<MonHocEntity>();
        public DbSet<LopHocPhanEntity> lopHocPhan => Set<LopHocPhanEntity>();
        public DbSet<SinhVienEntity> sinhVien => Set<SinhVienEntity>();
        public DbSet<DangKyEntity> dangKy => Set<DangKyEntity>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SinhVienEntity>()
                .HasIndex(s => s.MaSV)
                .IsUnique();

            // Cấu hình OnDelete(Restrict) từ SinhVien -> DangKy
            modelBuilder.Entity<DangKyEntity>()
                .HasOne(d => d.SinhVien)
                .WithMany(s => s.dangKys)
                .HasForeignKey(d => d.SinhVienId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MonHocEntity>().HasData(new List<MonHocEntity>() {
                new MonHocEntity
                {
                    MonHocID = 1,
                    MaMon = "IT101",
                    TenMon = "Nhập môn lập trình",
                    SoTinChi = 3,
                    SoTietLyThuyet = 30
                },
                new MonHocEntity
                {
                    MonHocID = 2,
                    MaMon = "IT102",
                    TenMon = "Kỹ thuật lập trình",
                    SoTinChi = 3,
                    SoTietLyThuyet = 30
                }
            });
            modelBuilder.Entity<SinhVienEntity>().HasData(new List<SinhVienEntity>() {
                new SinhVienEntity
                {
                    SinhVienID = 1,
                    MaSV = "SV001",
                    HoTen = "Nguyễn Văn A",
                    Email = "nguyenvana@gmail.com",
                    NgaySinh = new DateTime(2004, 1, 15),
                    LopSinhHoat = "22DTH1"
                },
                new SinhVienEntity
                {
                    SinhVienID = 2,
                    MaSV = "SV002",
                    HoTen = "Trần Thị B",
                    Email = "tranthib@gmail.com",
                    NgaySinh = new DateTime(2004, 5, 20),
                    LopSinhHoat = "22DTH2"
                }
            });

            modelBuilder.Entity<LopHocPhanEntity>().HasData(new List<LopHocPhanEntity>() {
                new LopHocPhanEntity
                {
                    LopHocPhanID = 1,
                    MonHocID = 1,
                    MaLopHP = "LHP001",
                    HocKy = 1,
                    NamHoc = 2024,
                    SiSoToiDa = 40
                },
                new LopHocPhanEntity
                {
                    LopHocPhanID = 2,
                    MonHocID = 1,
                    MaLopHP = "LHP002",
                    HocKy = 1,
                    NamHoc = 2024,
                    SiSoToiDa = 45
                },
                new LopHocPhanEntity
                {
                    LopHocPhanID = 3,
                    MonHocID = 2,
                    MaLopHP = "LHP003",
                    HocKy = 2,
                    NamHoc = 2024,
                    SiSoToiDa = 40
                }
            });
        }
    }
}
