using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace IDK.Migrations
{
    /// <inheritdoc />
    public partial class cartconfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Carts",
                columns: new[] { "Id", "Price", "ProductId", "ProductName", "Quantity", "userId" },
                values: new object[,]
                {
                    { 1, 299.99m, 1, "Air Jordan 4", 1, 2 },
                    { 2, 299.99m, 1, "Air Jordan 4", 2, 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
