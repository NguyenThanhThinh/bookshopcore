using Microsoft.EntityFrameworkCore.Migrations;

namespace BookShop.Migrations
{
    public partial class editmodels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Alias",
                table: "Categories",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Alias",
                table: "Books",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Alias",
                table: "Authors",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Alias",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "Alias",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Alias",
                table: "Authors");
        }
    }
}
