using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_ELibrary.Migrations
{
    public partial class createDb_V16 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Dokho",
                table: "Dethi",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Thoiluong",
                table: "Dethi",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Dokho",
                table: "Dethi");

            migrationBuilder.DropColumn(
                name: "Thoiluong",
                table: "Dethi");
        }
    }
}
