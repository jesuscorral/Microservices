using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JCP.Ordering.Infrastructure.Migrations
{
    public partial class changerelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_Order_OrderId",
                schema: "Catalog",
                table: "OrderItem");

            migrationBuilder.DropIndex(
                name: "IX_OrderItem_OrderId",
                schema: "Catalog",
                table: "OrderItem");

            migrationBuilder.DropColumn(
                name: "OrderId",
                schema: "Catalog",
                table: "OrderItem");

            migrationBuilder.CreateTable(
                name: "OrderOrderItems",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderOrderItems", x => new { x.OrderId, x.OrderItemId });
                    table.ForeignKey(
                        name: "FK_OrderOrderItems_Order_OrderId",
                        column: x => x.OrderId,
                        principalSchema: "Ordering",
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderOrderItems_OrderItem_OrderId",
                        column: x => x.OrderId,
                        principalSchema: "Catalog",
                        principalTable: "OrderItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderOrderItems");

            migrationBuilder.AddColumn<Guid>(
                name: "OrderId",
                schema: "Catalog",
                table: "OrderItem",
                type: "uniqueidentifier",
                nullable: true);

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
    }
}
