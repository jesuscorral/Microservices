using Microsoft.EntityFrameworkCore.Migrations;

namespace JCP.Catalog.Infrastructure.Migrations
{
    public partial class changeentityname2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CatalogItem",
                schema: "Catalog",
                table: "CatalogItem");

            migrationBuilder.RenameTable(
                name: "CatalogItem",
                schema: "Catalog",
                newName: "Product",
                newSchema: "Catalog");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Product",
                schema: "Catalog",
                table: "Product",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Product",
                schema: "Catalog",
                table: "Product");

            migrationBuilder.RenameTable(
                name: "Product",
                schema: "Catalog",
                newName: "CatalogItem",
                newSchema: "Catalog");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CatalogItem",
                schema: "Catalog",
                table: "CatalogItem",
                column: "Id");
        }
    }
}
