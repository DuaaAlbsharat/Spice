using Microsoft.EntityFrameworkCore.Migrations;

namespace Spice.Data.Migrations
{
    public partial class updMenuItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Espicy",
                table: "MenuItems",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Espicy",
                table: "MenuItems");
        }
    }
}
