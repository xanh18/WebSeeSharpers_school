using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebSeeSharpers.Migrations
{
    public partial class Room : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Room",
                table: "Movie",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Room",
                table: "Movie");
        }
    }
}
