using Microsoft.EntityFrameworkCore.Migrations;

namespace tenetApi.Migrations
{
    public partial class initShopRel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Shop",
                columns: table => new
                {
                    ShopID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<long>(type: "bigint", nullable: false),
                    ShopName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    ShopAddress = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    TelePhone = table.Column<int>(type: "int", nullable: false),
                    CellPhone = table.Column<int>(type: "int", nullable: false),
                    ShopLatitude = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ShopLongitude = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shop", x => x.ShopID);
                    table.ForeignKey(
                        name: "FK_Shop_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Shop_UserID",
                table: "Shop",
                column: "UserID",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Shop");
        }
    }
}
