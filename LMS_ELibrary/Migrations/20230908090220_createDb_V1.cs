using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_ELibrary.Migrations
{
    public partial class createDb_V1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ex_QA_Dethi_DethiID",
                table: "Ex_QA");

            migrationBuilder.AddForeignKey(
                name: "FK_Ex_Qa_Dethi",
                table: "Ex_QA",
                column: "DethiID",
                principalTable: "Dethi",
                principalColumn: "DethiID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ex_Qa_Dethi",
                table: "Ex_QA");

            migrationBuilder.AddForeignKey(
                name: "FK_Ex_QA_Dethi_DethiID",
                table: "Ex_QA",
                column: "DethiID",
                principalTable: "Dethi",
                principalColumn: "DethiID");
        }
    }
}
