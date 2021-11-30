using Microsoft.EntityFrameworkCore.Migrations;

namespace tenetApi.Migrations
{
    public partial class initCustomerRelation2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_customers_AspNetUsers_UserID",
                table: "customers");

            migrationBuilder.AddForeignKey(
                name: "FK_customers_AspNetUsers_UserID",
                table: "customers",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_customers_AspNetUsers_UserID",
                table: "customers");

            migrationBuilder.AddForeignKey(
                name: "FK_customers_AspNetUsers_UserID",
                table: "customers",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
