using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LHU_124000698_NguyenHuynhDuyThuc.Api.Migrations
{
    public partial class SeedSinhVienData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "NgaySinh",
                table: "SinhVien",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "SinhVien",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "SinhVien",
                columns: new[] { "SinhVienID", "Email", "HoTen", "LopSinhHoat", "MaSV", "NgaySinh" },
                values: new object[] { 1, "nguyenvana@gmail.com", "Nguyễn Văn A", "22DTH1", "SV001", new DateTime(2004, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "SinhVien",
                columns: new[] { "SinhVienID", "Email", "HoTen", "LopSinhHoat", "MaSV", "NgaySinh" },
                values: new object[] { 2, "tranthib@gmail.com", "Trần Thị B", "22DTH2", "SV002", new DateTime(2004, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SinhVien",
                keyColumn: "SinhVienID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SinhVien",
                keyColumn: "SinhVienID",
                keyValue: 2);

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgaySinh",
                table: "SinhVien",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "SinhVien",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);
        }
    }
}
