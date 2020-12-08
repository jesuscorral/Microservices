using Microsoft.EntityFrameworkCore.Migrations;

namespace JCP.Ordering.Infrastructure.Migrations
{
    public partial class Changeschema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderOrderItems_Order_OrderId",
                table: "OrderOrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderOrderItems_OrderItem_OrderId",
                table: "OrderOrderItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderOrderItems",
                table: "OrderOrderItems");

            migrationBuilder.RenameTable(
                name: "OrderOrderItems",
                newName: "OrderOrderItem",
                newSchema: "Ordering");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderOrderItem",
                schema: "Ordering",
                table: "OrderOrderItem",
                columns: new[] { "OrderId", "OrderItemId" });

            migrationBuilder.AddForeignKey(
                name: "FK_OrderOrderItem_Order_OrderId",
                schema: "Ordering",
                table: "OrderOrderItem",
                column: "OrderId",
                principalSchema: "Ordering",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderOrderItem_OrderItem_OrderId",
                schema: "Ordering",
                table: "OrderOrderItem",
                column: "OrderId",
                principalSchema: "Catalog",
                principalTable: "OrderItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderOrderItem_Order_OrderId",
                schema: "Ordering",
                table: "OrderOrderItem");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderOrderItem_OrderItem_OrderId",
                schema: "Ordering",
                table: "OrderOrderItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderOrderItem",
                schema: "Ordering",
                table: "OrderOrderItem");

            migrationBuilder.RenameTable(
                name: "OrderOrderItem",
                schema: "Ordering",
                newName: "OrderOrderItems");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderOrderItems",
                table: "OrderOrderItems",
                columns: new[] { "OrderId", "OrderItemId" });

            migrationBuilder.AddForeignKey(
                name: "FK_OrderOrderItems_Order_OrderId",
                table: "OrderOrderItems",
                column: "OrderId",
                principalSchema: "Ordering",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderOrderItems_OrderItem_OrderId",
                table: "OrderOrderItems",
                column: "OrderId",
                principalSchema: "Catalog",
                principalTable: "OrderItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
