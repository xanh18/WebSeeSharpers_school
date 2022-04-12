using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebSeeSharpers.Migrations
{
    public partial class updateIdentityAgain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
               
                 name: "isActive",
                 table: "AspNetUsers",
                 type: "nvarchar(max)",
                 nullable: true);
        }
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
            name: "IsActive",
            table: "AspNetUsers");

        }
    }
}
