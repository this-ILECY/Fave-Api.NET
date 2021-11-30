using Microsoft.EntityFrameworkCore.Migrations;

namespace tenetApi.Migrations
{
    public partial class porduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PorductID",
                table: "products",
                newName: "ProductID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductID",
                table: "products",
                newName: "PorductID");
        }
    }
}
