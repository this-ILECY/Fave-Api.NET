using Microsoft.EntityFrameworkCore.Migrations;

namespace tenetApi.Migrations
{
    public partial class prorel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_promotion_ProductID",
                table: "promotion",
                column: "ProductID");

            migrationBuilder.AddForeignKey(
                name: "FK_promotion_products_ProductID",
                table: "promotion",
                column: "ProductID",
                principalTable: "products",
                principalColumn: "PorductID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_promotion_products_ProductID",
                table: "promotion");

            migrationBuilder.DropIndex(
                name: "IX_promotion_ProductID",
                table: "promotion");
        }
    }
}
