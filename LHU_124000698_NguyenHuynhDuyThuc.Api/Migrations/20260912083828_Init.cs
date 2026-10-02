using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LHU_124000698_NguyenHuynhDuyThuc.Api.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MonHoc",
                columns: table => new
                {
                    MonHocID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaMon = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TenMon = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SoTinChi = table.Column<int>(type: "int", nullable: false),
                    SoTietLyThuyet = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonHoc", x => x.MonHocID);
                });

            migrationBuilder.CreateTable(
                name: "LopHocPhan",
                columns: table => new
                {
                    LopHocPhanID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MonHocID = table.Column<int>(type: "int", nullable: false),
                    MaLopHP = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    HocKy = table.Column<int>(type: "int", nullable: false),
                    NamHoc = table.Column<int>(type: "int", nullable: false),
                    SiSoToiDa = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LopHocPhan", x => x.LopHocPhanID);
                    table.ForeignKey(
                        name: "FK_LopHocPhan_MonHoc_MonHocID",
                        column: x => x.MonHocID,
                        principalTable: "MonHoc",
                        principalColumn: "MonHocID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LopHocPhan_MonHocID",
                table: "LopHocPhan",
                column: "MonHocID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LopHocPhan");

            migrationBuilder.DropTable(
                name: "MonHoc");
        }
    }
}
