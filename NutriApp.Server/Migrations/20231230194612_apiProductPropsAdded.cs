using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NutriApp.Server.Migrations
{
    /// <inheritdoc />
    public partial class apiProductPropsAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Brand",
                table: "ProductApiUrls",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AddColumn<string>(
                name: "ApiId",
                table: "ProductApiUrls",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Calories",
                table: "ProductApiUrls",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Carbohydrates",
                table: "ProductApiUrls",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ProductApiUrls",
                type: "nvarchar(512)",
                maxLength: 512,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Fats",
                table: "ProductApiUrls",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Portion",
                table: "ProductApiUrls",
                type: "nvarchar(512)",
                maxLength: 512,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Proteins",
                table: "ProductApiUrls",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApiId",
                table: "ProductApiUrls");

            migrationBuilder.DropColumn(
                name: "Calories",
                table: "ProductApiUrls");

            migrationBuilder.DropColumn(
                name: "Carbohydrates",
                table: "ProductApiUrls");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "ProductApiUrls");

            migrationBuilder.DropColumn(
                name: "Fats",
                table: "ProductApiUrls");

            migrationBuilder.DropColumn(
                name: "Portion",
                table: "ProductApiUrls");

            migrationBuilder.DropColumn(
                name: "Proteins",
                table: "ProductApiUrls");

            migrationBuilder.AlterColumn<string>(
                name: "Brand",
                table: "ProductApiUrls",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);
        }
    }
}
