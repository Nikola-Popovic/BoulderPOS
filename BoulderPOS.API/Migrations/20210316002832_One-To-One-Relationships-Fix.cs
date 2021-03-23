using Microsoft.EntityFrameworkCore.Migrations;

namespace BoulderPOS.API.Migrations
{
    public partial class OneToOneRelationshipsFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fK_customerSubscriptions_customers_customerId",
                table: "customerSubscriptions");

            migrationBuilder.DropForeignKey(
                name: "fK_productInventory_products_productId",
                table: "productInventory");

            migrationBuilder.DropForeignKey(
                name: "fK_productPayments_customers_customerId",
                table: "productPayments");

            migrationBuilder.DropIndex(
                name: "iX_productInventory_productId",
                table: "productInventory");

            migrationBuilder.CreateIndex(
                name: "iX_productInventory_productId",
                table: "productInventory",
                column: "productId",
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

            migrationBuilder.AddForeignKey(
                name: "fK_productPayments_customers_customerId",
                table: "productPayments",
                column: "customerId",
                principalTable: "customers",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fK_customerSubscriptions_customers_customerId",
                table: "customerSubscriptions");

            migrationBuilder.DropForeignKey(
                name: "fK_productInventory_products_productId",
                table: "productInventory");

            migrationBuilder.DropForeignKey(
                name: "fK_productPayments_customers_customerId",
                table: "productPayments");

            migrationBuilder.DropIndex(
                name: "iX_productInventory_productId",
                table: "productInventory");

            migrationBuilder.CreateIndex(
                name: "iX_productInventory_productId",
                table: "productInventory",
                column: "productId");

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

            migrationBuilder.AddForeignKey(
                name: "fK_productPayments_customers_customerId",
                table: "productPayments",
                column: "customerId",
                principalTable: "customers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
