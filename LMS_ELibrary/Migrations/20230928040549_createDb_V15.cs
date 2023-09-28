using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_ELibrary.Migrations
{
    public partial class createDb_V15 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Thongbao");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "ThongbaoLop",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "ThongbaoLop");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Thongbao",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
