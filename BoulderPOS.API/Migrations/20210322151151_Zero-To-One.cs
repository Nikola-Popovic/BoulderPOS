using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace BoulderPOS.API.Migrations
{
    public partial class ZeroToOne : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fK_customerSubscriptions_customers_customerId",
                table: "customerSubscriptions");

            migrationBuilder.DropForeignKey(
                name: "fK_productInventory_products_productId",
                table: "productInventory");

            migrationBuilder.DropPrimaryKey(
                name: "pK_productInventory",
                table: "productInventory");

            migrationBuilder.DropIndex(
                name: "iX_productInventory_productId",
                table: "productInventory");

            migrationBuilder.DropPrimaryKey(
                name: "pK_customerSubscriptions",
                table: "customerSubscriptions");

            migrationBuilder.DropIndex(
                name: "iX_customerSubscriptions_customerId",
                table: "customerSubscriptions");

            migrationBuilder.DropColumn(
                name: "id",
                table: "productInventory");

            migrationBuilder.DropColumn(
                name: "id",
                table: "customerSubscriptions");

            migrationBuilder.AddPrimaryKey(
                name: "pK_productInventory",
                table: "productInventory",
                column: "productId");

            migrationBuilder.AddPrimaryKey(
                name: "pK_customerSubscriptions",
                table: "customerSubscriptions",
                column: "customerId");

            migrationBuilder.AddForeignKey(
                name: "fK_customerSubscriptions_customers_customerId",
                table: "customerSubscriptions",
                column: "customerId",
                principalTable: "customers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fK_productInventory_products_productId",
                table: "productInventory",
                column: "productId",
                principalTable: "products",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fK_customerSubscriptions_customers_customerId",
                table: "customerSubscriptions");

            migrationBuilder.DropForeignKey(
                name: "fK_productInventory_products_productId",
                table: "productInventory");

            migrationBuilder.DropPrimaryKey(
                name: "pK_productInventory",
                table: "productInventory");

            migrationBuilder.DropPrimaryKey(
                name: "pK_customerSubscriptions",
                table: "customerSubscriptions");

            migrationBuilder.AddColumn<int>(
                name: "id",
                table: "productInventory",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "id",
                table: "customerSubscriptions",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "pK_productInventory",
                table: "productInventory",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pK_customerSubscriptions",
                table: "customerSubscriptions",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "iX_productInventory_productId",
                table: "productInventory",
                column: "productId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "iX_customerSubscriptions_customerId",
                table: "customerSubscriptions",
                column: "customerId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "fK_customerSubscriptions_customers_customerId",
                table: "customerSubscriptions",
                column: "customerId",
                principalTable: "customers",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fK_productInventory_products_productId",
                table: "productInventory",
                column: "productId",
                principalTable: "products",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
