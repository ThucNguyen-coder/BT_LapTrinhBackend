using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LHU_124000698_NguyenHuynhDuyThuc.Api.Migrations
{
    public partial class ThucSeedData2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "NguoiDung",
                keyColumn: "Id",
                keyValue: 1,
                column: "MatKhauHash",
                value: "$2a$11$ekQ0roCSaKIeuI.dEvVdi.TVJsM3bSqV95go7S3dmW80BqbM.K7xW");

            migrationBuilder.UpdateData(
                table: "NguoiDung",
                keyColumn: "Id",
                keyValue: 2,
                column: "MatKhauHash",
                value: "$2a$11$z8/Lzh.7mjFx1OvCyEIyUeW1zQEURlcWuSPl51kNSUc2KGQc.yVdm");

            migrationBuilder.UpdateData(
                table: "NguoiDung",
                keyColumn: "Id",
                keyValue: 3,
                column: "MatKhauHash",
                value: "$2a$11$9cENMk9Zx3TtcyzVzXqpm.A4EsxkVQ0K4.KpBJQ50kdxJQihZu5ui");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "NguoiDung",
                keyColumn: "Id",
                keyValue: 1,
                column: "MatKhauHash",
                value: "$2a$11$ToDL5Apm.cdz0RUOZggDkenLORElj.PA6qx1gZ7G5Yj5XQdhNml46");

            migrationBuilder.UpdateData(
                table: "NguoiDung",
                keyColumn: "Id",
                keyValue: 2,
                column: "MatKhauHash",
                value: "$2a$11$ToDL5Apm.cdz0RUOZggDkenLORElj.PA6qx1gZ7G5Yj5XQdhNml46");

            migrationBuilder.UpdateData(
                table: "NguoiDung",
                keyColumn: "Id",
                keyValue: 3,
                column: "MatKhauHash",
                value: "$2a$11$ToDL5Apm.cdz0RUOZggDkenLORElj.PA6qx1gZ7G5Yj5XQdhNml46");
        }
    }
}
