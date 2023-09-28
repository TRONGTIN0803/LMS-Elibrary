using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_ELibrary.Migrations
{
    public partial class createDb_V14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Ghichu",
                table: "Monhoc",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Ngayduyet",
                table: "Monhoc",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Nguoiduyet",
                table: "Monhoc",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ghichu",
                table: "Dethi",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ghichu",
                table: "Monhoc");

            migrationBuilder.DropColumn(
                name: "Ngayduyet",
                table: "Monhoc");

            migrationBuilder.DropColumn(
                name: "Nguoiduyet",
                table: "Monhoc");

            migrationBuilder.DropColumn(
                name: "Ghichu",
                table: "Dethi");
        }
    }
}
