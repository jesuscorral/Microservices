using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JCP.Ordering.Infrastructure.Migrations
{
    public partial class Add_Order_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Ordering");

            migrationBuilder.AddColumn<Guid>(
                name: "OrderId",
                schema: "Catalog",
                table: "OrderItem",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Order",
                schema: "Ordering",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_OrderId",
                schema: "Catalog",
                table: "OrderItem",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_Order_OrderId",
                schema: "Catalog",
                table: "OrderItem",
                column: "OrderId",
                principalSchema: "Ordering",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_Order_OrderId",
                schema: "Catalog",
                table: "OrderItem");

            migrationBuilder.DropTable(
                name: "Order",
                schema: "Ordering");

            migrationBuilder.DropIndex(
                name: "IX_OrderItem_OrderId",
                schema: "Catalog",
                table: "OrderItem");

            migrationBuilder.DropColumn(
                name: "OrderId",
                schema: "Catalog",
                table: "OrderItem");
        }
    }
}
