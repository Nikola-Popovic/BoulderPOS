using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace BoulderPOS.API.Migrations
{
    public partial class AddBillAndUpdatePayment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "productPayments");

            migrationBuilder.CreateTable(
                name: "bills",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    customerId = table.Column<int>(type: "integer", nullable: true),
                    subtotal = table.Column<decimal>(type: "decimal(8, 2)", nullable: false),
                    total = table.Column<decimal>(type: "decimal(8, 2)", nullable: false),
                    processedDateTime = table.Column<DateTime>(type: "timestamp", nullable: false),
                    updatedDateTime = table.Column<DateTime>(type: "timestamp", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pK_bills", x => x.id);
                    table.ForeignKey(
                        name: "fK_bills_customers_customerId",
                        column: x => x.customerId,
                        principalTable: "customers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "billProducts",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    customerId = table.Column<int>(type: "integer", nullable: true),
                    productId = table.Column<int>(type: "integer", nullable: false),
                    isRefunded = table.Column<bool>(type: "boolean", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    sellingPrice = table.Column<decimal>(type: "decimal(8, 2)", nullable: false),
                    processedDateTime = table.Column<DateTime>(type: "timestamp", nullable: false),
                    updatedDateTime = table.Column<DateTime>(type: "timestamp", nullable: false),
                    billId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pK_billProducts", x => x.id);
                    table.ForeignKey(
                        name: "fK_billProducts_bills_billId",
                        column: x => x.billId,
                        principalTable: "bills",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fK_billProducts_customers_customerId",
                        column: x => x.customerId,
                        principalTable: "customers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fK_billProducts_products_productId",
                        column: x => x.productId,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "iX_billProducts_billId",
                table: "billProducts",
                column: "billId");

            migrationBuilder.CreateIndex(
                name: "iX_billProducts_customerId",
                table: "billProducts",
                column: "customerId");

            migrationBuilder.CreateIndex(
                name: "iX_billProducts_productId",
                table: "billProducts",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "iX_bills_customerId",
                table: "bills",
                column: "customerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "billProducts");

            migrationBuilder.DropTable(
                name: "bills");

            migrationBuilder.CreateTable(
                name: "productPayments",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    customerId = table.Column<int>(type: "integer", nullable: true),
                    isRefunded = table.Column<bool>(type: "boolean", nullable: false),
                    processedDateTime = table.Column<DateTime>(type: "timestamp", nullable: false),
                    productId = table.Column<int>(type: "integer", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    sellingPrice = table.Column<decimal>(type: "decimal(8, 2)", nullable: false),
                    updatedDateTime = table.Column<DateTime>(type: "timestamp", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pK_productPayments", x => x.id);
                    table.ForeignKey(
                        name: "fK_productPayments_customers_customerId",
                        column: x => x.customerId,
                        principalTable: "customers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fK_productPayments_products_productId",
                        column: x => x.productId,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "iX_productPayments_customerId",
                table: "productPayments",
                column: "customerId");

            migrationBuilder.CreateIndex(
                name: "iX_productPayments_productId",
                table: "productPayments",
                column: "productId");
        }
    }
}
