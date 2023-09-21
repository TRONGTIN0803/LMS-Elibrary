using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_ELibrary.Migrations
{
    public partial class createDb_V9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Hocvien_Lop",
                columns: table => new
                {
                    HvLopID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_Id = table.Column<int>(type: "int", nullable: true),
                    Lopgiang_Id = table.Column<int>(type: "int", nullable: true),
                    LopgiangdayID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hocvien_Lop", x => x.HvLopID);
                    table.ForeignKey(
                        name: "FK_Hocvien_Lop_LopGiangday_LopgiangdayID",
                        column: x => x.LopgiangdayID,
                        principalTable: "LopGiangday",
                        principalColumn: "LopgiangdayID");
                    table.ForeignKey(
                        name: "FK_hocvienlop_user",
                        column: x => x.User_Id,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ThongbaoLop",
                columns: table => new
                {
                    ThongbaoLopID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Thongbao_Id = table.Column<int>(type: "int", nullable: true),
                    Lopgiang_Id = table.Column<int>(type: "int", nullable: true),
                    LopgiangdayID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThongbaoLop", x => x.ThongbaoLopID);
                    table.ForeignKey(
                        name: "FK_ThongbaoLop_LopGiangday_LopgiangdayID",
                        column: x => x.LopgiangdayID,
                        principalTable: "LopGiangday",
                        principalColumn: "LopgiangdayID");
                    table.ForeignKey(
                        name: "FK_thongbaolop_thongbao",
                        column: x => x.Thongbao_Id,
                        principalTable: "Thongbao",
                        principalColumn: "ThongbaoID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Hocvien_Lop_LopgiangdayID",
                table: "Hocvien_Lop",
                column: "LopgiangdayID");

            migrationBuilder.CreateIndex(
                name: "IX_Hocvien_Lop_User_Id",
                table: "Hocvien_Lop",
                column: "User_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ThongbaoLop_LopgiangdayID",
                table: "ThongbaoLop",
                column: "LopgiangdayID");

            migrationBuilder.CreateIndex(
                name: "IX_ThongbaoLop_Thongbao_Id",
                table: "ThongbaoLop",
                column: "Thongbao_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Hocvien_Lop");

            migrationBuilder.DropTable(
                name: "ThongbaoLop");
        }
    }
}
