using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LHU_124000698_NguyenHuynhDuyThuc.Api.Migrations
{
    public partial class BTVN : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "NguoiDung",
                keyColumn: "Id",
                keyValue: 1,
                column: "MatKhauHash",
                value: "$2a$11$G04WRNjSPCkzQpAaK3fBbe.K4Ajgav0nSA9Q8sl6SNs./FYML99X2");

            migrationBuilder.UpdateData(
                table: "NguoiDung",
                keyColumn: "Id",
                keyValue: 2,
                column: "MatKhauHash",
                value: "$2a$11$6ayGlEsf5GS0VuvERRNfKOkkSWjHzXJwv1wfmg4MIgJUwouVFTL4e");

            migrationBuilder.UpdateData(
                table: "NguoiDung",
                keyColumn: "Id",
                keyValue: 3,
                column: "MatKhauHash",
                value: "$2a$11$OpNjptEi0O9i87FM.g6ZuO.41fKtJptJDiSMjjXwKN3UeEgdtSQ4O");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
