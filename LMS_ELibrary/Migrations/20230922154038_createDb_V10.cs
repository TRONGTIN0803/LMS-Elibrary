using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_ELibrary.Migrations
{
    public partial class createDb_V10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ThongbaoLop_LopGiangday_LopgiangdayID",
                table: "ThongbaoLop");

            migrationBuilder.RenameColumn(
                name: "LopgiangdayID",
                table: "ThongbaoLop",
                newName: "User_Id");

            migrationBuilder.RenameIndex(
                name: "IX_ThongbaoLop_LopgiangdayID",
                table: "ThongbaoLop",
                newName: "IX_ThongbaoLop_User_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ThongbaoLop_Lopgiang_Id",
                table: "ThongbaoLop",
                column: "Lopgiang_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_thongbaolop_lop",
                table: "ThongbaoLop",
                column: "Lopgiang_Id",
                principalTable: "LopGiangday",
                principalColumn: "LopgiangdayID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_thongbaolop_user",
                table: "ThongbaoLop",
                column: "User_Id",
                principalTable: "User",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_thongbaolop_lop",
                table: "ThongbaoLop");

            migrationBuilder.DropForeignKey(
                name: "FK_thongbaolop_user",
                table: "ThongbaoLop");

            migrationBuilder.DropIndex(
                name: "IX_ThongbaoLop_Lopgiang_Id",
                table: "ThongbaoLop");

            migrationBuilder.RenameColumn(
                name: "User_Id",
                table: "ThongbaoLop",
                newName: "LopgiangdayID");

            migrationBuilder.RenameIndex(
                name: "IX_ThongbaoLop_User_Id",
                table: "ThongbaoLop",
                newName: "IX_ThongbaoLop_LopgiangdayID");

            migrationBuilder.AddForeignKey(
                name: "FK_ThongbaoLop_LopGiangday_LopgiangdayID",
                table: "ThongbaoLop",
                column: "LopgiangdayID",
                principalTable: "LopGiangday",
                principalColumn: "LopgiangdayID");
        }
    }
}
