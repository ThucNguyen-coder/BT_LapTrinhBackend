using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LHU_124000698_NguyenHuynhDuyThuc.Api.Migrations
{
    public partial class AddSinhVienAndDangKyTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SinhVien",
                columns: table => new
                {
                    SinhVienID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaSV = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    HoTen = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgaySinh = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LopSinhHoat = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SinhVien", x => x.SinhVienID);
                });

            migrationBuilder.CreateTable(
                name: "DangKy",
                columns: table => new
                {
                    DangKyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SinhVienId = table.Column<int>(type: "int", nullable: false),
                    LopHocPhanId = table.Column<int>(type: "int", nullable: false),
                    NgayDangKy = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DangKy", x => x.DangKyId);
                    table.ForeignKey(
                        name: "FK_DangKy_LopHocPhan_LopHocPhanId",
                        column: x => x.LopHocPhanId,
                        principalTable: "LopHocPhan",
                        principalColumn: "LopHocPhanID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DangKy_SinhVien_SinhVienId",
                        column: x => x.SinhVienId,
                        principalTable: "SinhVien",
                        principalColumn: "SinhVienID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DangKy_LopHocPhanId",
                table: "DangKy",
                column: "LopHocPhanId");

            migrationBuilder.CreateIndex(
                name: "IX_DangKy_SinhVienId",
                table: "DangKy",
                column: "SinhVienId");

            migrationBuilder.CreateIndex(
                name: "IX_SinhVien_MaSV",
                table: "SinhVien",
                column: "MaSV",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DangKy");

            migrationBuilder.DropTable(
                name: "SinhVien");
        }
    }
}
