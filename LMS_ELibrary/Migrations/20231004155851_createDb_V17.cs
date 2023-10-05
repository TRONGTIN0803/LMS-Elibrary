using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_ELibrary.Migrations
{
    public partial class createDb_V17 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Kichthuoc",
                table: "Tailieu_Baigiang");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Tailieu_Baigiang");

            migrationBuilder.RenameColumn(
                name: "Path",
                table: "Tailieu_Baigiang",
                newName: "Mota");

            migrationBuilder.AddColumn<int>(
                name: "Dokho",
                table: "QA",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Macauhoi",
                table: "QA",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Ngaytao",
                table: "QA",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Nguoitao_Id",
                table: "QA",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Hinhthuc",
                table: "Dethi",
                type: "bit",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "File_Baigiang",
                columns: table => new
                {
                    File_Baigiang_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tenfile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Size = table.Column<double>(type: "float", nullable: true),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoitailen_Id = table.Column<int>(type: "int", nullable: true),
                    Ngaytailen = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true),
                    Baigiang_id = table.Column<int>(type: "int", nullable: true),
                    Nguoiduyet_Id = table.Column<int>(type: "int", nullable: true),
                    Ngayduyet = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_File_Baigiang", x => x.File_Baigiang_Id);
                    table.ForeignKey(
                        name: "FK_file_baigiang",
                        column: x => x.Baigiang_id,
                        principalTable: "Tailieu_Baigiang",
                        principalColumn: "DocId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_filebaigiang_user",
                        column: x => x.Nguoitailen_Id,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "File_Tainguyen",
                columns: table => new
                {
                    File_Tainguyen_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tenfile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Size = table.Column<double>(type: "float", nullable: true),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoitailen_Id = table.Column<int>(type: "int", nullable: true),
                    Ngaytailen = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true),
                    Baigiang_Id = table.Column<int>(type: "int", nullable: true),
                    Nguoiduyet_Id = table.Column<int>(type: "int", nullable: true),
                    Ngayduyet = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_File_Tainguyen", x => x.File_Tainguyen_Id);
                    table.ForeignKey(
                        name: "FK_file_tainguyen",
                        column: x => x.Baigiang_Id,
                        principalTable: "Tailieu_Baigiang",
                        principalColumn: "DocId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_filetainguyen_user",
                        column: x => x.Nguoitailen_Id,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QA_Nguoitao_Id",
                table: "QA",
                column: "Nguoitao_Id");

            migrationBuilder.CreateIndex(
                name: "IX_File_Baigiang_Baigiang_id",
                table: "File_Baigiang",
                column: "Baigiang_id");

            migrationBuilder.CreateIndex(
                name: "IX_File_Baigiang_Nguoitailen_Id",
                table: "File_Baigiang",
                column: "Nguoitailen_Id");

            migrationBuilder.CreateIndex(
                name: "IX_File_Tainguyen_Baigiang_Id",
                table: "File_Tainguyen",
                column: "Baigiang_Id");

            migrationBuilder.CreateIndex(
                name: "IX_File_Tainguyen_Nguoitailen_Id",
                table: "File_Tainguyen",
                column: "Nguoitailen_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_cauhoi_user",
                table: "QA",
                column: "Nguoitao_Id",
                principalTable: "User",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cauhoi_user",
                table: "QA");

            migrationBuilder.DropTable(
                name: "File_Baigiang");

            migrationBuilder.DropTable(
                name: "File_Tainguyen");

            migrationBuilder.DropIndex(
                name: "IX_QA_Nguoitao_Id",
                table: "QA");

            migrationBuilder.DropColumn(
                name: "Dokho",
                table: "QA");

            migrationBuilder.DropColumn(
                name: "Macauhoi",
                table: "QA");

            migrationBuilder.DropColumn(
                name: "Ngaytao",
                table: "QA");

            migrationBuilder.DropColumn(
                name: "Nguoitao_Id",
                table: "QA");

            migrationBuilder.DropColumn(
                name: "Hinhthuc",
                table: "Dethi");

            migrationBuilder.RenameColumn(
                name: "Mota",
                table: "Tailieu_Baigiang",
                newName: "Path");

            migrationBuilder.AddColumn<double>(
                name: "Kichthuoc",
                table: "Tailieu_Baigiang",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Tailieu_Baigiang",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
