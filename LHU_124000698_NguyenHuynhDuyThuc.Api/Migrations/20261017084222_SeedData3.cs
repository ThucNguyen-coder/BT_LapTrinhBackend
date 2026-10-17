using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LHU_124000698_NguyenHuynhDuyThuc.Api.Migrations
{
    public partial class SeedData3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NguoiDung",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenDangNhap = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MatKhauHash = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    VaiTro = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    SinhVienId = table.Column<int>(type: "int", nullable: true),
                    GiangVienId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NguoiDung", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NguoiDung_SinhVien_SinhVienId",
                        column: x => x.SinhVienId,
                        principalTable: "SinhVien",
                        principalColumn: "SinhVienID");
                });

            migrationBuilder.InsertData(
                table: "LopHocPhan",
                columns: new[] { "LopHocPhanID", "HocKy", "MaLopHP", "MonHocID", "NamHoc", "NgayBatDau", "NgayKetThuc", "SiSoToiDa" },
                values: new object[,]
                {
                    { 5, 1, "IT101-01", 1, 2025, new DateTime(2025, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 50 },
                    { 6, 1, "IT102-01", 2, 2025, new DateTime(2025, 9, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 12, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 45 }
                });

            migrationBuilder.InsertData(
                table: "MonHoc",
                columns: new[] { "MonHocID", "MaMon", "SoTietLyThuyet", "SoTinChi", "TenMon" },
                values: new object[,]
                {
                    { 7, "IT101", 30, 3, "Nhập môn lập trình" },
                    { 8, "IT102", 30, 3, "Kỹ thuật lập trình" },
                    { 9, "IT201", 30, 3, "Cấu trúc dữ liệu" }
                });

            migrationBuilder.InsertData(
                table: "NguoiDung",
                columns: new[] { "Id", "GiangVienId", "MatKhauHash", "SinhVienId", "TenDangNhap", "VaiTro" },
                values: new object[,]
                {
                    { 1, null, "$2a$11$ToDL5Apm.cdz0RUOZggDkenLORElj.PA6qx1gZ7G5Yj5XQdhNml46", null, "daotao", "DaoTao" },
                    { 2, 1, "$2a$11$ToDL5Apm.cdz0RUOZggDkenLORElj.PA6qx1gZ7G5Yj5XQdhNml46", null, "giangvien", "GiangVien" },
                    { 3, null, "$2a$11$ToDL5Apm.cdz0RUOZggDkenLORElj.PA6qx1gZ7G5Yj5XQdhNml46", 1, "sinhvien", "SinhVien" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_NguoiDung_SinhVienId",
                table: "NguoiDung",
                column: "SinhVienId");

            migrationBuilder.CreateIndex(
                name: "IX_NguoiDung_TenDangNhap",
                table: "NguoiDung",
                column: "TenDangNhap",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NguoiDung");

            migrationBuilder.DeleteData(
                table: "LopHocPhan",
                keyColumn: "LopHocPhanID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "LopHocPhan",
                keyColumn: "LopHocPhanID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "MonHoc",
                keyColumn: "MonHocID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "MonHoc",
                keyColumn: "MonHocID",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "MonHoc",
                keyColumn: "MonHocID",
                keyValue: 9);
        }
    }
}
