using Microsoft.EntityFrameworkCore.Migrations;

namespace tenetApi.Migrations
{
    public partial class productShopRel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    PorductID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShopID = table.Column<long>(type: "bigint", nullable: false),
                    CategoryID = table.Column<long>(type: "bigint", nullable: false),
                    ProductCode = table.Column<long>(type: "bigint", nullable: false),
                    ProductTitle = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.PorductID);
                    table.ForeignKey(
                        name: "FK_Product_Shop_ShopID",
                        column: x => x.ShopID,
                        principalTable: "Shop",
                        principalColumn: "ShopID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_ShopID",
                table: "Product",
                column: "ShopID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Product");
        }
    }
}
