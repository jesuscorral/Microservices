using Microsoft.EntityFrameworkCore.Migrations;

namespace JCP.Catalog.Infrastructure.Migrations
{
    public partial class Change_tableName_and_Add_scheme : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Catalog",
                table: "Catalog");

            migrationBuilder.EnsureSchema(
                name: "Catalog");

            migrationBuilder.RenameTable(
                name: "Catalog",
                newName: "CatalogItem",
                newSchema: "Catalog");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CatalogItem",
                schema: "Catalog",
                table: "CatalogItem",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CatalogItem",
                schema: "Catalog",
                table: "CatalogItem");

            migrationBuilder.RenameTable(
                name: "CatalogItem",
                schema: "Catalog",
                newName: "Catalog");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Catalog",
                table: "Catalog",
                column: "Id");
        }
    }
}
