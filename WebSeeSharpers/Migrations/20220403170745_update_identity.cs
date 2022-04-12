using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebSeeSharpers.Migrations
{
    public partial class update_identity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
      
            migrationBuilder.RenameColumn(
                name: "last_name",
                table: "AspNetUsers",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "first_name",
                table: "AspNetUsers",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "is_active",
                table: "AspNetUsers",
                 newName: "IsActive");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
           name: "LastName",
           table: "AspNetUsers");


            migrationBuilder.DropColumn(
           name: "IsActive",
           table: "AspNetUsers");



        }
    }
}
