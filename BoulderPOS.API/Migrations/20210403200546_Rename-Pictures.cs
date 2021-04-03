using Microsoft.EntityFrameworkCore.Migrations;

namespace BoulderPOS.API.Migrations
{
    public partial class RenamePictures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "categoryIconPath",
                table: "productCategories",
                newName: "iconName");

            migrationBuilder.RenameColumn(
                name: "picturePath",
                table: "customers",
                newName: "picture");

            migrationBuilder.AlterColumn<string>(
                name: "iconName",
                table: "productCategories",
                type: "varchar(30)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "iconName",
                table: "productCategories",
                newName: "categoryIconPath");

            migrationBuilder.RenameColumn(
                name: "picture",
                table: "customers",
                newName: "picturePath");

            migrationBuilder.AlterColumn<string>(
                name: "categoryIconPath",
                table: "productCategories",
                type: "varchar",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldNullable: true);
        }
    }
}
