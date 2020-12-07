using Microsoft.EntityFrameworkCore.Migrations;

namespace JCP.Ordering.Infrastructure.Migrations
{
    public partial class Change_schema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Catalog");

            migrationBuilder.RenameTable(
                name: "OrderItem",
                schema: "Order",
                newName: "OrderItem",
                newSchema: "Catalog");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Order");

            migrationBuilder.RenameTable(
                name: "OrderItem",
                schema: "Catalog",
                newName: "OrderItem",
                newSchema: "Order");
        }
    }
}
