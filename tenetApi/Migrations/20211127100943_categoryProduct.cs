using Microsoft.EntityFrameworkCore.Migrations;

namespace tenetApi.Migrations
{
    public partial class categoryProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SHopID",
                table: "promotion",
                newName: "ShopID");

            migrationBuilder.RenameColumn(
                name: "CategoryID",
                table: "products",
                newName: "ProductCategoryID");

            migrationBuilder.CreateTable(
                name: "productCategories",
                columns: table => new
                {
                    ProductCategoryID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductCategoryTitle = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ProductCategoryDescription = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productCategories", x => x.ProductCategoryID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_products_ProductCategoryID",
                table: "products",
                column: "ProductCategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_products_productCategories_ProductCategoryID",
                table: "products",
                column: "ProductCategoryID",
                principalTable: "productCategories",
                principalColumn: "ProductCategoryID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_productCategories_ProductCategoryID",
                table: "products");

            migrationBuilder.DropTable(
                name: "productCategories");

            migrationBuilder.DropIndex(
                name: "IX_products_ProductCategoryID",
                table: "products");

            migrationBuilder.RenameColumn(
                name: "ShopID",
                table: "promotion",
                newName: "SHopID");

            migrationBuilder.RenameColumn(
                name: "ProductCategoryID",
                table: "products",
                newName: "CategoryID");
        }
    }
}
