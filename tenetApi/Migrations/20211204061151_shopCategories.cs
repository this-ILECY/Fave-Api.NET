using Microsoft.EntityFrameworkCore.Migrations;

namespace tenetApi.Migrations
{
    public partial class shopCategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_shops_ShopCategory_ShopCategoryID",
                table: "shops");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShopCategory",
                table: "ShopCategory");

            migrationBuilder.RenameTable(
                name: "ShopCategory",
                newName: "shopCategories");

            migrationBuilder.AddPrimaryKey(
                name: "PK_shopCategories",
                table: "shopCategories",
                column: "ShopCategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_shops_shopCategories_ShopCategoryID",
                table: "shops",
                column: "ShopCategoryID",
                principalTable: "shopCategories",
                principalColumn: "ShopCategoryID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_shops_shopCategories_ShopCategoryID",
                table: "shops");

            migrationBuilder.DropPrimaryKey(
                name: "PK_shopCategories",
                table: "shopCategories");

            migrationBuilder.RenameTable(
                name: "shopCategories",
                newName: "ShopCategory");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShopCategory",
                table: "ShopCategory",
                column: "ShopCategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_shops_ShopCategory_ShopCategoryID",
                table: "shops",
                column: "ShopCategoryID",
                principalTable: "ShopCategory",
                principalColumn: "ShopCategoryID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
