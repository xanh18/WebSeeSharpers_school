using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebSeeSharpers.Migrations
{
    public partial class Extend_application_user : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "AspNetUsers",
                newName: "City");

            migrationBuilder.AddColumn<string>(
                name: "Age",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "AspNetUsers",
                newName: "IsActive");
        }
    }
}
