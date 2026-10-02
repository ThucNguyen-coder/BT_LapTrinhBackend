using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LHU_124000698_NguyenHuynhDuyThuc.Api.Migrations
{
    public partial class Thuc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "MonHoc",
                columns: new[] { "MonHocID", "MaMon", "SoTietLyThuyet", "SoTinChi", "TenMon" },
                values: new object[] { 1, "IT101", 30, 3, "Nhập môn lập trình" });

            migrationBuilder.InsertData(
                table: "MonHoc",
                columns: new[] { "MonHocID", "MaMon", "SoTietLyThuyet", "SoTinChi", "TenMon" },
                values: new object[] { 2, "IT102", 30, 3, "Kỹ thuật lập trình" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MonHoc",
                keyColumn: "MonHocID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "MonHoc",
                keyColumn: "MonHocID",
                keyValue: 2);
        }
    }
}
