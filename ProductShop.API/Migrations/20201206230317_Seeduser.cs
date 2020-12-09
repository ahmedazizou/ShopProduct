using Microsoft.EntityFrameworkCore.Migrations;
using System;
namespace ProductShop.API.Migrations
{
    public partial class Seeduser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"INSERT INTO [dbo].[Users] ([Email],[FirstName],[LastName],[Password],[IsClient]) Values (N'admin@gmail.com' ,N'Admin', N'Admin',N'admin',0)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
