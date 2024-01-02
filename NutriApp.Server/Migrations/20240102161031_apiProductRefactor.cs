using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NutriApp.Server.Migrations
{
    /// <inheritdoc />
    public partial class apiProductRefactor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DishApiProducts_ProductApiUrls_ApiProductInfoId",
                table: "DishApiProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductApiUrls",
                table: "ProductApiUrls");

            migrationBuilder.RenameTable(
                name: "ProductApiUrls",
                newName: "ApiProductInfos");

            migrationBuilder.AlterColumn<string>(
                name: "Portion",
                table: "ApiProductInfos",
                type: "nvarchar(512)",
                maxLength: 512,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(512)",
                oldMaxLength: 512);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "ApiProductInfos",
                type: "nvarchar(512)",
                maxLength: 512,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(512)",
                oldMaxLength: 512);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApiProductInfos",
                table: "ApiProductInfos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DishApiProducts_ApiProductInfos_ApiProductInfoId",
                table: "DishApiProducts",
                column: "ApiProductInfoId",
                principalTable: "ApiProductInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DishApiProducts_ApiProductInfos_ApiProductInfoId",
                table: "DishApiProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApiProductInfos",
                table: "ApiProductInfos");

            migrationBuilder.RenameTable(
                name: "ApiProductInfos",
                newName: "ProductApiUrls");

            migrationBuilder.AlterColumn<string>(
                name: "Portion",
                table: "ProductApiUrls",
                type: "nvarchar(512)",
                maxLength: 512,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(512)",
                oldMaxLength: 512,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "ProductApiUrls",
                type: "nvarchar(512)",
                maxLength: 512,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(512)",
                oldMaxLength: 512,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductApiUrls",
                table: "ProductApiUrls",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DishApiProducts_ProductApiUrls_ApiProductInfoId",
                table: "DishApiProducts",
                column: "ApiProductInfoId",
                principalTable: "ProductApiUrls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
