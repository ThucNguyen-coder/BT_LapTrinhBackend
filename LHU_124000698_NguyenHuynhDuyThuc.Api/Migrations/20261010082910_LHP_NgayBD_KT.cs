using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LHU_124000698_NguyenHuynhDuyThuc.Api.Migrations
{
    public partial class LHP_NgayBD_KT : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "NgayBatDau",
                table: "LopHocPhan",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayKetThuc",
                table: "LopHocPhan",
                type: "datetime2",
                nullable: true);

            migrationBuilder.InsertData(
                table: "LopHocPhan",
                columns: new[] { "LopHocPhanID", "HocKy", "MaLopHP", "MonHocID", "NamHoc", "NgayBatDau", "NgayKetThuc", "SiSoToiDa" },
                values: new object[] { 1, 1, "LHP001", 1, 2024, null, null, 40 });

            migrationBuilder.InsertData(
                table: "LopHocPhan",
                columns: new[] { "LopHocPhanID", "HocKy", "MaLopHP", "MonHocID", "NamHoc", "NgayBatDau", "NgayKetThuc", "SiSoToiDa" },
                values: new object[] { 2, 1, "LHP002", 1, 2024, null, null, 45 });

            migrationBuilder.InsertData(
                table: "LopHocPhan",
                columns: new[] { "LopHocPhanID", "HocKy", "MaLopHP", "MonHocID", "NamHoc", "NgayBatDau", "NgayKetThuc", "SiSoToiDa" },
                values: new object[] { 3, 2, "LHP003", 2, 2024, null, null, 40 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "LopHocPhan",
                keyColumn: "LopHocPhanID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "LopHocPhan",
                keyColumn: "LopHocPhanID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "LopHocPhan",
                keyColumn: "LopHocPhanID",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "NgayBatDau",
                table: "LopHocPhan");

            migrationBuilder.DropColumn(
                name: "NgayKetThuc",
                table: "LopHocPhan");
        }
    }
}
