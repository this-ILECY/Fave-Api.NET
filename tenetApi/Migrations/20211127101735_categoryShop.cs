using Microsoft.EntityFrameworkCore.Migrations;

namespace tenetApi.Migrations
{
    public partial class categoryShop : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_productCategories_ProductCategoryID",
                table: "products");

            migrationBuilder.AddColumn<long>(
                name: "ShopCategoryID",
                table: "shops",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ShopCategory",
                columns: table => new
                {
                    ShopCategoryID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShopCategoryTitle = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ShopCategoryDescription = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShopCategory", x => x.ShopCategoryID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_shops_ShopCategoryID",
                table: "shops",
                column: "ShopCategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_products_productCategories_ProductCategoryID",
                table: "products",
                column: "ProductCategoryID",
                principalTable: "productCategories",
                principalColumn: "ProductCategoryID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_shops_ShopCategory_ShopCategoryID",
                table: "shops",
                column: "ShopCategoryID",
                principalTable: "ShopCategory",
                principalColumn: "ShopCategoryID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_productCategories_ProductCategoryID",
                table: "products");

            migrationBuilder.DropForeignKey(
                name: "FK_shops_ShopCategory_ShopCategoryID",
                table: "shops");

            migrationBuilder.DropTable(
                name: "ShopCategory");

            migrationBuilder.DropIndex(
                name: "IX_shops_ShopCategoryID",
                table: "shops");

            migrationBuilder.DropColumn(
                name: "ShopCategoryID",
                table: "shops");

            migrationBuilder.AddForeignKey(
                name: "FK_products_productCategories_ProductCategoryID",
                table: "products",
                column: "ProductCategoryID",
                principalTable: "productCategories",
                principalColumn: "ProductCategoryID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
