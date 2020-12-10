using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JCP.Ordering.Infrastructure.Migrations
{
    public partial class changeentityrelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderOrderItem",
                schema: "Ordering");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderItem",
                schema: "Catalog",
                table: "OrderItem");

            migrationBuilder.DropColumn(
                name: "Amount",
                schema: "Ordering",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "Date",
                schema: "Ordering",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "OrderName",
                schema: "Ordering",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "Catalog",
                table: "OrderItem");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "Catalog",
                table: "OrderItem");

            migrationBuilder.DropColumn(
                name: "Price",
                schema: "Catalog",
                table: "OrderItem");

            migrationBuilder.RenameTable(
                name: "OrderItem",
                schema: "Catalog",
                newName: "OrderItem",
                newSchema: "Ordering");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "Ordering",
                table: "OrderItem",
                newName: "ProductId");

            migrationBuilder.AddColumn<Guid>(
                name: "OrderId",
                schema: "Ordering",
                table: "OrderItem",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderItem",
                schema: "Ordering",
                table: "OrderItem",
                columns: new[] { "OrderId", "ProductId" });

            migrationBuilder.CreateTable(
                name: "Product",
                schema: "Catalog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(4,2)", precision: 4, scale: 2, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_ProductId",
                schema: "Ordering",
                table: "OrderItem",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_Order_OrderId",
                schema: "Ordering",
                table: "OrderItem",
                column: "OrderId",
                principalSchema: "Ordering",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_Product_ProductId",
                schema: "Ordering",
                table: "OrderItem",
                column: "ProductId",
                principalSchema: "Catalog",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_Order_OrderId",
                schema: "Ordering",
                table: "OrderItem");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_Product_ProductId",
                schema: "Ordering",
                table: "OrderItem");

            migrationBuilder.DropTable(
                name: "Product",
                schema: "Catalog");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderItem",
                schema: "Ordering",
                table: "OrderItem");

            migrationBuilder.DropIndex(
                name: "IX_OrderItem_ProductId",
                schema: "Ordering",
                table: "OrderItem");

            migrationBuilder.DropColumn(
                name: "OrderId",
                schema: "Ordering",
                table: "OrderItem");

            migrationBuilder.RenameTable(
                name: "OrderItem",
                schema: "Ordering",
                newName: "OrderItem",
                newSchema: "Catalog");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                schema: "Catalog",
                table: "OrderItem",
                newName: "Id");

            migrationBuilder.AddColumn<double>(
                name: "Amount",
                schema: "Ordering",
                table: "Order",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                schema: "Ordering",
                table: "Order",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "OrderName",
                schema: "Ordering",
                table: "Order",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "Catalog",
                table: "OrderItem",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "Catalog",
                table: "OrderItem",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                schema: "Catalog",
                table: "OrderItem",
                type: "decimal(4,2)",
                precision: 4,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderItem",
                schema: "Catalog",
                table: "OrderItem",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "OrderOrderItem",
                schema: "Ordering",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderOrderItem", x => new { x.OrderId, x.OrderItemId });
                    table.ForeignKey(
                        name: "FK_OrderOrderItem_Order_OrderId",
                        column: x => x.OrderId,
                        principalSchema: "Ordering",
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderOrderItem_OrderItem_OrderId",
                        column: x => x.OrderId,
                        principalSchema: "Catalog",
                        principalTable: "OrderItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }
    }
}
