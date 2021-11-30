using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace tenetApi.Migrations
{
    public partial class dbcontextrepair : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerAddress_customers_CustomerID",
                table: "CustomerAddress");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Shop_ShopID",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Shop_AspNetUsers_UserID",
                table: "Shop");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Shop",
                table: "Shop");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Product",
                table: "Product");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerAddress",
                table: "CustomerAddress");

            migrationBuilder.RenameTable(
                name: "Shop",
                newName: "shops");

            migrationBuilder.RenameTable(
                name: "Product",
                newName: "products");

            migrationBuilder.RenameTable(
                name: "CustomerAddress",
                newName: "customerAddresses");

            migrationBuilder.RenameIndex(
                name: "IX_Shop_UserID",
                table: "shops",
                newName: "IX_shops_UserID");

            migrationBuilder.RenameIndex(
                name: "IX_Product_ShopID",
                table: "products",
                newName: "IX_products_ShopID");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerAddress_CustomerID",
                table: "customerAddresses",
                newName: "IX_customerAddresses_CustomerID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_shops",
                table: "shops",
                column: "ShopID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_products",
                table: "products",
                column: "PorductID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_customerAddresses",
                table: "customerAddresses",
                column: "CustomerAddressID");

            migrationBuilder.CreateTable(
                name: "promotion",
                columns: table => new
                {
                    PromotionID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductID = table.Column<long>(type: "bigint", nullable: false),
                    SHopID = table.Column<long>(type: "bigint", nullable: false),
                    BasePrice = table.Column<long>(type: "bigint", nullable: false),
                    DiscountPrice = table.Column<long>(type: "bigint", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    QualityGrade = table.Column<int>(type: "int", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_promotion", x => x.PromotionID);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_customerAddresses_customers_CustomerID",
                table: "customerAddresses",
                column: "CustomerID",
                principalTable: "customers",
                principalColumn: "CustomerID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_products_shops_ShopID",
                table: "products",
                column: "ShopID",
                principalTable: "shops",
                principalColumn: "ShopID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_shops_AspNetUsers_UserID",
                table: "shops",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_customerAddresses_customers_CustomerID",
                table: "customerAddresses");

            migrationBuilder.DropForeignKey(
                name: "FK_products_shops_ShopID",
                table: "products");

            migrationBuilder.DropForeignKey(
                name: "FK_shops_AspNetUsers_UserID",
                table: "shops");

            migrationBuilder.DropTable(
                name: "promotion");

            migrationBuilder.DropPrimaryKey(
                name: "PK_shops",
                table: "shops");

            migrationBuilder.DropPrimaryKey(
                name: "PK_products",
                table: "products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_customerAddresses",
                table: "customerAddresses");

            migrationBuilder.RenameTable(
                name: "shops",
                newName: "Shop");

            migrationBuilder.RenameTable(
                name: "products",
                newName: "Product");

            migrationBuilder.RenameTable(
                name: "customerAddresses",
                newName: "CustomerAddress");

            migrationBuilder.RenameIndex(
                name: "IX_shops_UserID",
                table: "Shop",
                newName: "IX_Shop_UserID");

            migrationBuilder.RenameIndex(
                name: "IX_products_ShopID",
                table: "Product",
                newName: "IX_Product_ShopID");

            migrationBuilder.RenameIndex(
                name: "IX_customerAddresses_CustomerID",
                table: "CustomerAddress",
                newName: "IX_CustomerAddress_CustomerID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Shop",
                table: "Shop",
                column: "ShopID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Product",
                table: "Product",
                column: "PorductID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerAddress",
                table: "CustomerAddress",
                column: "CustomerAddressID");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerAddress_customers_CustomerID",
                table: "CustomerAddress",
                column: "CustomerID",
                principalTable: "customers",
                principalColumn: "CustomerID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Shop_ShopID",
                table: "Product",
                column: "ShopID",
                principalTable: "Shop",
                principalColumn: "ShopID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Shop_AspNetUsers_UserID",
                table: "Shop",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
