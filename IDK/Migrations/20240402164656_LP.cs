using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IDK.Migrations
{
    /// <inheritdoc />
    public partial class LP : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Address", "Email", "Name", "Password" },
                values: new object[] { 3, "Ruse, Riga 26, Petar", "viktor.vip@abv.bg", "viktor", "pikolov" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
