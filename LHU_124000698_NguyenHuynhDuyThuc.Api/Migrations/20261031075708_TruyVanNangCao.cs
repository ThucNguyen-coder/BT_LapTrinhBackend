using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LHU_124000698_NguyenHuynhDuyThuc.Api.Migrations
{
    public partial class TruyVanNangCao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "NguoiDung",
                keyColumn: "Id",
                keyValue: 1,
                column: "MatKhauHash",
                value: "$2a$11$Wks2.ziLNSAEJ5nNkwTUmOroBo5MyA.1zcayHnzfmEePQ7dqpR/DK");

            migrationBuilder.UpdateData(
                table: "NguoiDung",
                keyColumn: "Id",
                keyValue: 2,
                column: "MatKhauHash",
                value: "$2a$11$ArIMa3fke7QYUhKngx70FuDUDz9P4hK.IQVjyna1KuBA1h4o27IXW");

            migrationBuilder.UpdateData(
                table: "NguoiDung",
                keyColumn: "Id",
                keyValue: 3,
                column: "MatKhauHash",
                value: "$2a$11$oao1FwgoshNuGHdcUngl7OCGwtGh/pwGD5VZsSqGwZnuEJLAX0IS6");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
