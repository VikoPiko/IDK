using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace IDK.Migrations
{
    /// <inheritdoc />
    public partial class CancerHardcodedOrders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Products",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,2)");

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "CustomerId", "IsComplete", "OrderFulfilled", "OrderPlaced", "TotalPrice" },
                values: new object[,]
                {
                    { 3, 2, true, new DateTime(2024, 3, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 99.99m },
                    { 4, 1, false, new DateTime(2024, 3, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 449.99m },
                    { 5, 2, true, new DateTime(2024, 3, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 199.99m },
                    { 6, 1, false, new DateTime(2024, 3, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 59.99m },
                    { 7, 1, true, new DateTime(2024, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 389.99m },
                    { 8, 2, false, new DateTime(2024, 3, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 79.99m },
                    { 9, 1, true, new DateTime(2024, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 279.99m },
                    { 10, 1, false, new DateTime(2024, 3, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 149.99m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Products",
                type: "decimal(6,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }
    }
}
