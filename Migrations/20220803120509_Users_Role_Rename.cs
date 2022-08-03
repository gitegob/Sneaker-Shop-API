using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sneaker_Shop_API.Migrations
{
    public partial class Users_Role_Rename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "role",
                table: "users",
                newName: "Role");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Role",
                table: "users",
                newName: "role");
        }
    }
}
