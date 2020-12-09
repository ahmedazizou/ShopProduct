using Microsoft.EntityFrameworkCore.Migrations;

namespace ProductShop.API.Migrations
{
    public partial class AddUserIsClient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsClient",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsClient",
                table: "Users");
        }
    }
}
