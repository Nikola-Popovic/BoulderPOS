using Microsoft.EntityFrameworkCore.Migrations;

namespace BoulderPOS.API.Migrations
{
    public partial class AddQuantityToProductPayment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "duration",
                table: "products");

            migrationBuilder.AddColumn<int>(
                name: "durationInMonths",
                table: "products",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "quantity",
                table: "productPayments",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "durationInMonths",
                table: "products");

            migrationBuilder.DropColumn(
                name: "quantity",
                table: "productPayments");

            migrationBuilder.AddColumn<int>(
                name: "duration",
                table: "products",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
