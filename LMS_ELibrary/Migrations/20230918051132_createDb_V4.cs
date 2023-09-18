using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_ELibrary.Migrations
{
    public partial class createDb_V4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CauhoiVandap",
                columns: table => new
                {
                    CauhoiId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tieude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Noidung = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaytao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    TailieuId = table.Column<int>(type: "int", nullable: true),
                    LopgiangId = table.Column<int>(type: "int", nullable: true),
                    ChudeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CauhoiVandap", x => x.CauhoiId);
                    table.ForeignKey(
                        name: "FK_Cauhoicandap_user",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CauhoiVandap_Tailieu_Baigiang_TailieuId",
                        column: x => x.TailieuId,
                        principalTable: "Tailieu_Baigiang",
                        principalColumn: "DocId");
                });

            migrationBuilder.CreateTable(
                name: "MonhocYeuthich",
                columns: table => new
                {
                    MonhocYeuthichID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    MonhocId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonhocYeuthich", x => x.MonhocYeuthichID);
                    table.ForeignKey(
                        name: "FK_MonhocYeuthich_Monhoc_MonhocId",
                        column: x => x.MonhocId,
                        principalTable: "Monhoc",
                        principalColumn: "MonhocID");
                    table.ForeignKey(
                        name: "FK_MonYeuthich_User",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CauhoiYeuthich",
                columns: table => new
                {
                    CauhoiYeuthichID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    CauhoiId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CauhoiYeuthich", x => x.CauhoiYeuthichID);
                    table.ForeignKey(
                        name: "FK_cauhoiyeuthcih_user",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CauhoiYeuthich_CauhoiVandap_CauhoiId",
                        column: x => x.CauhoiId,
                        principalTable: "CauhoiVandap",
                        principalColumn: "CauhoiId");
                });

            migrationBuilder.CreateTable(
                name: "Cautrl",
                columns: table => new
                {
                    CautrlID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cautrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaytao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    CauhoiId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cautrl", x => x.CautrlID);
                    table.ForeignKey(
                        name: "FK_Cautrl_CauhoiVandap_CauhoiId",
                        column: x => x.CauhoiId,
                        principalTable: "CauhoiVandap",
                        principalColumn: "CauhoiId");
                    table.ForeignKey(
                        name: "FK_Cautrl_user",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CauhoiVandap_TailieuId",
                table: "CauhoiVandap",
                column: "TailieuId");

            migrationBuilder.CreateIndex(
                name: "IX_CauhoiVandap_UserId",
                table: "CauhoiVandap",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CauhoiYeuthich_CauhoiId",
                table: "CauhoiYeuthich",
                column: "CauhoiId");

            migrationBuilder.CreateIndex(
                name: "IX_CauhoiYeuthich_UserId",
                table: "CauhoiYeuthich",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Cautrl_CauhoiId",
                table: "Cautrl",
                column: "CauhoiId");

            migrationBuilder.CreateIndex(
                name: "IX_Cautrl_UserId",
                table: "Cautrl",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MonhocYeuthich_MonhocId",
                table: "MonhocYeuthich",
                column: "MonhocId");

            migrationBuilder.CreateIndex(
                name: "IX_MonhocYeuthich_UserId",
                table: "MonhocYeuthich",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CauhoiYeuthich");

            migrationBuilder.DropTable(
                name: "Cautrl");

            migrationBuilder.DropTable(
                name: "MonhocYeuthich");

            migrationBuilder.DropTable(
                name: "CauhoiVandap");
        }
    }
}
