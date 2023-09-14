using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_ELibrary.Migrations
{
    public partial class CreateDb_V3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "File_Path",
                table: "Dethi");

            migrationBuilder.AddColumn<int>(
                name: "FileId",
                table: "Dethi",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FileDethi",
                columns: table => new
                {
                    FileId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_Id = table.Column<int>(type: "int", nullable: true),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Size = table.Column<double>(type: "float", nullable: true),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileDethi", x => x.FileId);
                    table.ForeignKey(
                        name: "FK_File_User",
                        column: x => x.User_Id,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dethi_FileId",
                table: "Dethi",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_FileDethi_User_Id",
                table: "FileDethi",
                column: "User_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Dethi_File",
                table: "Dethi",
                column: "FileId",
                principalTable: "FileDethi",
                principalColumn: "FileId",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dethi_File",
                table: "Dethi");

            migrationBuilder.DropTable(
                name: "FileDethi");

            migrationBuilder.DropIndex(
                name: "IX_Dethi_FileId",
                table: "Dethi");

            migrationBuilder.DropColumn(
                name: "FileId",
                table: "Dethi");

            migrationBuilder.AddColumn<string>(
                name: "File_Path",
                table: "Dethi",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
