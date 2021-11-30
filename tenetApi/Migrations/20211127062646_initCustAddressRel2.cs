using Microsoft.EntityFrameworkCore.Migrations;

namespace tenetApi.Migrations
{
    public partial class initCustAddressRel2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerAddress_customers_Customer",
                table: "CustomerAddress");

            migrationBuilder.DropIndex(
                name: "IX_CustomerAddress_Customer",
                table: "CustomerAddress");

            migrationBuilder.DropColumn(
                name: "Customer",
                table: "CustomerAddress");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAddress_CustomerID",
                table: "CustomerAddress",
                column: "CustomerID");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerAddress_customers_CustomerID",
                table: "CustomerAddress",
                column: "CustomerID",
                principalTable: "customers",
                principalColumn: "CustomerID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerAddress_customers_CustomerID",
                table: "CustomerAddress");

            migrationBuilder.DropIndex(
                name: "IX_CustomerAddress_CustomerID",
                table: "CustomerAddress");

            migrationBuilder.AddColumn<long>(
                name: "Customer",
                table: "CustomerAddress",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAddress_Customer",
                table: "CustomerAddress",
                column: "Customer");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerAddress_customers_Customer",
                table: "CustomerAddress",
                column: "Customer",
                principalTable: "customers",
                principalColumn: "CustomerID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
