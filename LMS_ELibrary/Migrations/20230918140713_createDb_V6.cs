using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_ELibrary.Migrations
{
    public partial class createDb_V6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Role_RoleDbRoleId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_RoleDbRoleId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "RoleDbRoleId",
                table: "User");

            migrationBuilder.CreateIndex(
                name: "IX_User_Role",
                table: "User",
                column: "Role");

            migrationBuilder.AddForeignKey(
                name: "FK_user_role",
                table: "User",
                column: "Role",
                principalTable: "Role",
                principalColumn: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_role",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_Role",
                table: "User");

            migrationBuilder.AddColumn<int>(
                name: "RoleDbRoleId",
                table: "User",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_RoleDbRoleId",
                table: "User",
                column: "RoleDbRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Role_RoleDbRoleId",
                table: "User",
                column: "RoleDbRoleId",
                principalTable: "Role",
                principalColumn: "RoleId");
        }
    }
}
