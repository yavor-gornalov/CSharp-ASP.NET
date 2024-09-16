using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SoftUniBazar.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class UsersSeeded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "0b5ca5a6-5732-4895-a96c-cce811834780", 0, "a348010e-1658-4a94-8e88-6ac1590c6f86", "seller@softuni.com", false, false, null, "SELLER@SOFTUNI.COM", "seller", null, null, false, "72aedb05-74ce-4077-933b-a23c90a8c6e2", false, "seller" },
                    { "493ba206-c3d5-4f28-8372-7824f4bbcf9e", 0, "f26c9d10-767a-411a-8836-d7f9dabef9e3", "guest@softuni.com", false, false, null, "GUEST@SOFTUNI.COM", "guest", "AQAAAAIAAYagAAAAENCj7D4Ay6do6cJktuFnivYAAsJaraHpw0eBW/CjV4ppmA1z8MPfP7FbOqLv1eBc6A==", null, false, "c407488f-e0c8-4bfe-a628-a2051b9c526e", false, "guest" },
                    { "7a02b826-8f95-44b9-baa6-a4b9804daa3c", 0, "613d1469-7630-44cd-8a44-a33c174ec17d", "buyer@softuni.com", false, false, null, "BUYER@SOFTUNI.COM", "buyer", "AQAAAAIAAYagAAAAEPW5h8NxMb5Q96iXM9jtrufbozeQn11fd1vaols8iUtZIoNuRSak/FftJKlEWTq/uA==", null, false, "712d5f45-e57a-48e3-800d-2a7b7b4dfc84", false, "buyer" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0b5ca5a6-5732-4895-a96c-cce811834780");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "493ba206-c3d5-4f28-8372-7824f4bbcf9e");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7a02b826-8f95-44b9-baa6-a4b9804daa3c");
        }
    }
}
