using Microsoft.EntityFrameworkCore.Migrations;

namespace BoulderPOS.API.Migrations
{
    public partial class InitialCategoriesDataSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "productCategories",
                columns: new[] { "id", "iconName", "isEntries", "isSubscription", "name", "order" },
                values: new object[,]
                {
                    { -1, "fas fa-ticket-alt", true, false, "Entries", 1 },
                    { -2, "fas fa-user-clock", false, true, "Subscription", 2 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "productCategories",
                keyColumn: "id",
                keyValue: -2);

            migrationBuilder.DeleteData(
                table: "productCategories",
                keyColumn: "id",
                keyValue: -1);
        }
    }
}
