using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NutriApp.Server.Migrations
{
    /// <inheritdoc />
    public partial class addedProductsAmount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GramsInPortion",
                table: "ProductApiUrls",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Amount",
                table: "DishProducts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Amount",
                table: "DishApiProducts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GramsInPortion",
                table: "ProductApiUrls");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "DishProducts");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "DishApiProducts");
        }
    }
}
