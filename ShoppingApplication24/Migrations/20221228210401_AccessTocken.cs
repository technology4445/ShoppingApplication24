using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoppingApplication24.Migrations
{
    public partial class AccessTocken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AccessTocken",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccessTocken",
                table: "Users");
        }
    }
}
