using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace BoulderPOS.API.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "customers",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    created = table.Column<DateTime>(type: "timestamp", nullable: false),
                    updated = table.Column<DateTime>(type: "timestamp", nullable: false),
                    firstName = table.Column<string>(type: "varchar(50)", nullable: false),
                    lastName = table.Column<string>(type: "varchar(50)", nullable: false),
                    email = table.Column<string>(type: "varchar(100)", nullable: false),
                    phoneNumber = table.Column<string>(type: "varchar(16)", nullable: true),
                    birthDate = table.Column<DateTime>(type: "date", nullable: false),
                    picturePath = table.Column<string>(type: "varchar", nullable: true),
                    picturePreviewPath = table.Column<byte[]>(type: "bytea", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pK_customers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "productCategories",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "varchar(30)", nullable: false),
                    categoryIconPath = table.Column<string>(type: "varchar", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pK_productCategories", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "customerEntries",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    customerId = table.Column<int>(type: "integer", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    unlimitedEntries = table.Column<bool>(type: "boolean", nullable: false, defaultValue:false),
                    updated = table.Column<DateTime>(type: "timestamp", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pK_customerEntries", x => x.id);
                    table.ForeignKey(
                        name: "fK_customerEntries_customers_customerId",
                        column: x => x.customerId,
                        principalTable: "customers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "customerSubscriptions",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    customerId = table.Column<int>(type: "integer", nullable: false),
                    startDate = table.Column<DateTime>(type: "date", nullable: false),
                    endDate = table.Column<DateTime>(type: "date", nullable: false),
                    autoRenewal = table.Column<bool>(type: "boolean", nullable: false),
                    created = table.Column<DateTime>(type: "timestamp", nullable: false),
                    updated = table.Column<DateTime>(type: "timestamp", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pK_customerSubscriptions", x => x.id);
                    table.ForeignKey(
                        name: "fK_customerSubscriptions_customers_customerId",
                        column: x => x.customerId,
                        principalTable: "customers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "varchar(50)", nullable: false),
                    price = table.Column<decimal>(type: "decimal(8, 2)", nullable: false),
                    categoryId = table.Column<int>(type: "integer", nullable: false),
                    created = table.Column<DateTime>(type: "timestamp", nullable: false),
                    updated = table.Column<DateTime>(type: "timestamp", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pK_products", x => x.id);
                    table.ForeignKey(
                        name: "fK_products_productCategories_categoryId",
                        column: x => x.categoryId,
                        principalTable: "productCategories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "productInventory",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    productId = table.Column<int>(type: "integer", nullable: false),
                    inStoreQuantity = table.Column<int>(type: "integer", nullable: false),
                    orderedQuantity = table.Column<int>(type: "integer", nullable: false),
                    suretyQuantity = table.Column<int>(type: "integer", nullable: false),
                    updated = table.Column<DateTime>(type: "timestamp", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pK_productInventory", x => x.id);
                    table.ForeignKey(
                        name: "fK_productInventory_products_productId",
                        column: x => x.productId,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "productPayments",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    customerId = table.Column<int>(type: "integer", nullable: false),
                    productId = table.Column<int>(type: "integer", nullable: false),
                    isRefunded = table.Column<bool>(type: "boolean", nullable: false, defaultValue:false),
                    sellingPrice = table.Column<decimal>(type: "decimal(8, 2)", nullable: false),
                    created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pK_productPayments", x => x.id);
                    table.ForeignKey(
                        name: "fK_productPayments_customers_customerId",
                        column: x => x.customerId,
                        principalTable: "customers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fK_productPayments_products_productId",
                        column: x => x.productId,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "iX_customerEntries_customerId",
                table: "customerEntries",
                column: "customerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "iX_customerSubscriptions_customerId",
                table: "customerSubscriptions",
                column: "customerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "iX_productInventory_productId",
                table: "productInventory",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "iX_productPayments_customerId",
                table: "productPayments",
                column: "customerId");

            migrationBuilder.CreateIndex(
                name: "iX_productPayments_productId",
                table: "productPayments",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "iX_products_categoryId",
                table: "products",
                column: "categoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "customerEntries");

            migrationBuilder.DropTable(
                name: "customerSubscriptions");

            migrationBuilder.DropTable(
                name: "productInventory");

            migrationBuilder.DropTable(
                name: "productPayments");

            migrationBuilder.DropTable(
                name: "customers");

            migrationBuilder.DropTable(
                name: "products");

            migrationBuilder.DropTable(
                name: "productCategories");
        }
    }
}
