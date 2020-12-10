using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JCP.Ordering.Infrastructure.Migrations
{
    public partial class addauditableEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                schema: "Ordering",
                table: "OrderOrderItem",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "Ordering",
                table: "OrderOrderItem",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                schema: "Ordering",
                table: "OrderOrderItem",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                schema: "Ordering",
                table: "OrderOrderItem",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                schema: "Catalog",
                table: "OrderItem",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "Catalog",
                table: "OrderItem",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                schema: "Catalog",
                table: "OrderItem",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                schema: "Catalog",
                table: "OrderItem",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                schema: "Ordering",
                table: "Order",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "Ordering",
                table: "Order",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                schema: "Ordering",
                table: "Order",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                schema: "Ordering",
                table: "Order",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created",
                schema: "Ordering",
                table: "OrderOrderItem");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "Ordering",
                table: "OrderOrderItem");

            migrationBuilder.DropColumn(
                name: "LastModified",
                schema: "Ordering",
                table: "OrderOrderItem");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                schema: "Ordering",
                table: "OrderOrderItem");

            migrationBuilder.DropColumn(
                name: "Created",
                schema: "Catalog",
                table: "OrderItem");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "Catalog",
                table: "OrderItem");

            migrationBuilder.DropColumn(
                name: "LastModified",
                schema: "Catalog",
                table: "OrderItem");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                schema: "Catalog",
                table: "OrderItem");

            migrationBuilder.DropColumn(
                name: "Created",
                schema: "Ordering",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "Ordering",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "LastModified",
                schema: "Ordering",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                schema: "Ordering",
                table: "Order");
        }
    }
}
