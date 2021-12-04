using Microsoft.EntityFrameworkCore.Migrations;

namespace tenetApi.Migrations
{
    public partial class shopCatID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "shopCatID",
                table: "shops",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_promotion_ShopID",
                table: "promotion",
                column: "ShopID");

            migrationBuilder.AddForeignKey(
                name: "FK_promotion_shops_ShopID",
                table: "promotion",
                column: "ShopID",
                principalTable: "shops",
                principalColumn: "ShopID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_promotion_shops_ShopID",
                table: "promotion");

            migrationBuilder.DropIndex(
                name: "IX_promotion_ShopID",
                table: "promotion");

            migrationBuilder.DropColumn(
                name: "shopCatID",
                table: "shops");
        }
    }
}
