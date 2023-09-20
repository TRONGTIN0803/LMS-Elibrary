using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_ELibrary.Migrations
{
    public partial class createDb_V8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LopGiangday_Monhoc_MonhocID",
                table: "LopGiangday");

            migrationBuilder.AddColumn<string>(
                name: "Nganh",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Ngaysuadoi",
                table: "User",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ghichu",
                table: "Tailieu_Baigiang",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayDuyet",
                table: "Tailieu_Baigiang",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Nguoiduyet",
                table: "Tailieu_Baigiang",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Monhoc",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Malop",
                table: "LopGiangday",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Ngayduyet",
                table: "Dethi",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Nguoiduyet",
                table: "Dethi",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Monhoc_Id",
                table: "Chude",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Monhoc_UserId",
                table: "Monhoc",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Chude_Monhoc_Id",
                table: "Chude",
                column: "Monhoc_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_chude_Monhoc",
                table: "Chude",
                column: "Monhoc_Id",
                principalTable: "Monhoc",
                principalColumn: "MonhocID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_lop_Monhoc",
                table: "LopGiangday",
                column: "MonhocID",
                principalTable: "Monhoc",
                principalColumn: "MonhocID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Monhoc_user",
                table: "Monhoc",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserID",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_chude_Monhoc",
                table: "Chude");

            migrationBuilder.DropForeignKey(
                name: "FK_lop_Monhoc",
                table: "LopGiangday");

            migrationBuilder.DropForeignKey(
                name: "FK_Monhoc_user",
                table: "Monhoc");

            migrationBuilder.DropIndex(
                name: "IX_Monhoc_UserId",
                table: "Monhoc");

            migrationBuilder.DropIndex(
                name: "IX_Chude_Monhoc_Id",
                table: "Chude");

            migrationBuilder.DropColumn(
                name: "Nganh",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Ngaysuadoi",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Ghichu",
                table: "Tailieu_Baigiang");

            migrationBuilder.DropColumn(
                name: "NgayDuyet",
                table: "Tailieu_Baigiang");

            migrationBuilder.DropColumn(
                name: "Nguoiduyet",
                table: "Tailieu_Baigiang");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Monhoc");

            migrationBuilder.DropColumn(
                name: "Malop",
                table: "LopGiangday");

            migrationBuilder.DropColumn(
                name: "Ngayduyet",
                table: "Dethi");

            migrationBuilder.DropColumn(
                name: "Nguoiduyet",
                table: "Dethi");

            migrationBuilder.DropColumn(
                name: "Monhoc_Id",
                table: "Chude");

            migrationBuilder.AddForeignKey(
                name: "FK_LopGiangday_Monhoc_MonhocID",
                table: "LopGiangday",
                column: "MonhocID",
                principalTable: "Monhoc",
                principalColumn: "MonhocID");
        }
    }
}
