using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_ELibrary.Migrations
{
    public partial class createDb_V13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tongquan",
                columns: table => new
                {
                    TongquanID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Monhoc_Id = table.Column<int>(type: "int", nullable: true),
                    Tieude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Noidung = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tongquan", x => x.TongquanID);
                    table.ForeignKey(
                        name: "FK_tongquan_monhoc",
                        column: x => x.Monhoc_Id,
                        principalTable: "Monhoc",
                        principalColumn: "MonhocID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tongquan_Monhoc_Id",
                table: "Tongquan",
                column: "Monhoc_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tongquan");
        }
    }
}
