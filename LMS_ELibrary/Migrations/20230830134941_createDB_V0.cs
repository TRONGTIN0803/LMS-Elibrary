using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_ELibrary.Migrations
{
    public partial class createDB_V0 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Chude",
                columns: table => new
                {
                    ChudeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tenchude = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chude", x => x.ChudeID);
                });

            migrationBuilder.CreateTable(
                name: "Tobomon",
                columns: table => new
                {
                    TobomonId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TobomonName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tobomon", x => x.TobomonId);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    Avt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserFullname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gioitinh = table.Column<bool>(type: "bit", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sdt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Diachi = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "Monhoc",
                columns: table => new
                {
                    MonhocID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenMonhoc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MaMonhoc = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Mota = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tinhtrang = table.Column<int>(type: "int", nullable: false),
                    TobomonId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Monhoc", x => x.MonhocID);
                    table.ForeignKey(
                        name: "FK_Monhoc_Tobomon_TobomonId",
                        column: x => x.TobomonId,
                        principalTable: "Tobomon",
                        principalColumn: "TobomonId");
                });

            migrationBuilder.CreateTable(
                name: "Help",
                columns: table => new
                {
                    HelpID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tieude = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Noidung = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgayGui = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Help", x => x.HelpID);
                    table.ForeignKey(
                        name: "FK_Help_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "Thongbao",
                columns: table => new
                {
                    ThongbaoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tieude = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Noidung = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Thoigian = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Thongbao", x => x.ThongbaoID);
                    table.ForeignKey(
                        name: "FK_Thongbao_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "Dethi",
                columns: table => new
                {
                    DethiID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Madethi = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: true),
                    Ngaytao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MonhocID = table.Column<int>(type: "int", nullable: true),
                    File_Path = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dethi", x => x.DethiID);
                    table.ForeignKey(
                        name: "FK_Dethi_Monhoc_MonhocID",
                        column: x => x.MonhocID,
                        principalTable: "Monhoc",
                        principalColumn: "MonhocID");
                    table.ForeignKey(
                        name: "FK_Dethi_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "LopGiangday",
                columns: table => new
                {
                    LopgiangdayID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: true),
                    MonhocID = table.Column<int>(type: "int", nullable: true),
                    TenLop = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Thoigian = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Truycapgannhat = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LopGiangday", x => x.LopgiangdayID);
                    table.ForeignKey(
                        name: "FK_LopGiangday_Monhoc_MonhocID",
                        column: x => x.MonhocID,
                        principalTable: "Monhoc",
                        principalColumn: "MonhocID");
                    table.ForeignKey(
                        name: "FK_LopGiangday_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "QA",
                columns: table => new
                {
                    QAID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cauhoi = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Cautrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MonhocID = table.Column<int>(type: "int", nullable: true),
                    Lancuoisua = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QA", x => x.QAID);
                    table.ForeignKey(
                        name: "FK_QA_Monhoc_MonhocID",
                        column: x => x.MonhocID,
                        principalTable: "Monhoc",
                        principalColumn: "MonhocID");
                });

            migrationBuilder.CreateTable(
                name: "Tailieu_Baigiang",
                columns: table => new
                {
                    DocId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    TenDoc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MonhocID = table.Column<int>(type: "int", nullable: true),
                    ChudeID = table.Column<int>(type: "int", nullable: true),
                    Sualancuoi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Kichthuoc = table.Column<double>(type: "float", nullable: true),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tailieu_Baigiang", x => x.DocId);
                    table.ForeignKey(
                        name: "FK_Tailieu_Baigiang_Chude_ChudeID",
                        column: x => x.ChudeID,
                        principalTable: "Chude",
                        principalColumn: "ChudeID");
                    table.ForeignKey(
                        name: "FK_Tailieu_Baigiang_Monhoc_MonhocID",
                        column: x => x.MonhocID,
                        principalTable: "Monhoc",
                        principalColumn: "MonhocID");
                    table.ForeignKey(
                        name: "FK_Tailieu_Baigiang_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "Ex_QA",
                columns: table => new
                {
                    EXQAID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DethiID = table.Column<int>(type: "int", nullable: true),
                    QAID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ex_QA", x => x.EXQAID);
                    table.ForeignKey(
                        name: "FK_Ex_QA_Dethi_DethiID",
                        column: x => x.DethiID,
                        principalTable: "Dethi",
                        principalColumn: "DethiID");
                    table.ForeignKey(
                        name: "FK_Ex_QA_QA_QAID",
                        column: x => x.QAID,
                        principalTable: "QA",
                        principalColumn: "QAID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dethi_MonhocID",
                table: "Dethi",
                column: "MonhocID");

            migrationBuilder.CreateIndex(
                name: "IX_Dethi_UserID",
                table: "Dethi",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Ex_QA_DethiID",
                table: "Ex_QA",
                column: "DethiID");

            migrationBuilder.CreateIndex(
                name: "IX_Ex_QA_QAID",
                table: "Ex_QA",
                column: "QAID");

            migrationBuilder.CreateIndex(
                name: "IX_Help_UserID",
                table: "Help",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_LopGiangday_MonhocID",
                table: "LopGiangday",
                column: "MonhocID");

            migrationBuilder.CreateIndex(
                name: "IX_LopGiangday_UserID",
                table: "LopGiangday",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Monhoc_TobomonId",
                table: "Monhoc",
                column: "TobomonId");

            migrationBuilder.CreateIndex(
                name: "IX_QA_MonhocID",
                table: "QA",
                column: "MonhocID");

            migrationBuilder.CreateIndex(
                name: "IX_Tailieu_Baigiang_ChudeID",
                table: "Tailieu_Baigiang",
                column: "ChudeID");

            migrationBuilder.CreateIndex(
                name: "IX_Tailieu_Baigiang_MonhocID",
                table: "Tailieu_Baigiang",
                column: "MonhocID");

            migrationBuilder.CreateIndex(
                name: "IX_Tailieu_Baigiang_UserId",
                table: "Tailieu_Baigiang",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Thongbao_UserID",
                table: "Thongbao",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ex_QA");

            migrationBuilder.DropTable(
                name: "Help");

            migrationBuilder.DropTable(
                name: "LopGiangday");

            migrationBuilder.DropTable(
                name: "Tailieu_Baigiang");

            migrationBuilder.DropTable(
                name: "Thongbao");

            migrationBuilder.DropTable(
                name: "Dethi");

            migrationBuilder.DropTable(
                name: "QA");

            migrationBuilder.DropTable(
                name: "Chude");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Monhoc");

            migrationBuilder.DropTable(
                name: "Tobomon");
        }
    }
}
