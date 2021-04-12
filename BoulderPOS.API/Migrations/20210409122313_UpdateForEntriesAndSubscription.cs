using Microsoft.EntityFrameworkCore.Migrations;

namespace BoulderPOS.API.Migrations
{
    public partial class UpdateForEntriesAndSubscription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "duration",
                table: "products",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "quantity",
                table: "products",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "isEntries",
                table: "productCategories",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isSubscription",
                table: "productCategories",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "order",
                table: "productCategories",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "duration",
                table: "products");

            migrationBuilder.DropColumn(
                name: "quantity",
                table: "products");

            migrationBuilder.DropColumn(
                name: "isEntries",
                table: "productCategories");

            migrationBuilder.DropColumn(
                name: "isSubscription",
                table: "productCategories");

            migrationBuilder.DropColumn(
                name: "order",
                table: "productCategories");
        }
    }
}
