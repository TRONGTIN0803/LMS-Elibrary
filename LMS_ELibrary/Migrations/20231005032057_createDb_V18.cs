using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_ELibrary.Migrations
{
    public partial class createDb_V18 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "File_Baigiang");

            migrationBuilder.DropTable(
                name: "File_Tainguyen");

            migrationBuilder.AddColumn<int>(
                name: "File_Baigiang_Id",
                table: "Tailieu_Baigiang",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "File_Tailen",
                columns: table => new
                {
                    File_Tailen_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tenfile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Size = table.Column<double>(type: "float", nullable: true),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoitailen_Id = table.Column<int>(type: "int", nullable: true),
                    Ngaytailen = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: true),
                    Nguoiduyet_Id = table.Column<int>(type: "int", nullable: true),
                    Ngayduyet = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_File_Tailen", x => x.File_Tailen_Id);
                    table.ForeignKey(
                        name: "FK_filebaigiang_user",
                        column: x => x.Nguoitailen_Id,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tainguyen",
                columns: table => new
                {
                    Tainguyen_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nguoitao_Id = table.Column<int>(type: "int", nullable: true),
                    Ngaytao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true),
                    File_Tainguyen_Id = table.Column<int>(type: "int", nullable: true),
                    Baigiang_Id = table.Column<int>(type: "int", nullable: true),
                    Sualancuoi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Nguoiduyet_Id = table.Column<int>(type: "int", nullable: true),
                    Ngayduyet = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tainguyen", x => x.Tainguyen_Id);
                    table.ForeignKey(
                        name: "FK_tainguyen_baigiang",
                        column: x => x.Baigiang_Id,
                        principalTable: "Tailieu_Baigiang",
                        principalColumn: "DocId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_tainguyen_filetainguyen",
                        column: x => x.File_Tainguyen_Id,
                        principalTable: "File_Tailen",
                        principalColumn: "File_Tailen_Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_tainguyen_user",
                        column: x => x.Nguoitao_Id,
                        principalTable: "User",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tailieu_Baigiang_File_Baigiang_Id",
                table: "Tailieu_Baigiang",
                column: "File_Baigiang_Id");

            migrationBuilder.CreateIndex(
                name: "IX_File_Tailen_Nguoitailen_Id",
                table: "File_Tailen",
                column: "Nguoitailen_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Tainguyen_Baigiang_Id",
                table: "Tainguyen",
                column: "Baigiang_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Tainguyen_File_Tainguyen_Id",
                table: "Tainguyen",
                column: "File_Tainguyen_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Tainguyen_Nguoitao_Id",
                table: "Tainguyen",
                column: "Nguoitao_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_baigiang_file",
                table: "Tailieu_Baigiang",
                column: "File_Baigiang_Id",
                principalTable: "File_Tailen",
                principalColumn: "File_Tailen_Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_baigiang_file",
                table: "Tailieu_Baigiang");

            migrationBuilder.DropTable(
                name: "Tainguyen");

            migrationBuilder.DropTable(
                name: "File_Tailen");

            migrationBuilder.DropIndex(
                name: "IX_Tailieu_Baigiang_File_Baigiang_Id",
                table: "Tailieu_Baigiang");

            migrationBuilder.DropColumn(
                name: "File_Baigiang_Id",
                table: "Tailieu_Baigiang");

            migrationBuilder.CreateTable(
                name: "File_Baigiang",
                columns: table => new
                {
                    File_Baigiang_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Baigiang_id = table.Column<int>(type: "int", nullable: true),
                    Nguoitailen_Id = table.Column<int>(type: "int", nullable: true),
                    Ngayduyet = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Ngaytailen = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Nguoiduyet_Id = table.Column<int>(type: "int", nullable: true),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Size = table.Column<double>(type: "float", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true),
                    Tenfile = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    Baigiang_Id = table.Column<int>(type: "int", nullable: true),
                    Nguoitailen_Id = table.Column<int>(type: "int", nullable: true),
                    Ngayduyet = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Ngaytailen = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Nguoiduyet_Id = table.Column<int>(type: "int", nullable: true),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Size = table.Column<double>(type: "float", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true),
                    Tenfile = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
        }
    }
}
