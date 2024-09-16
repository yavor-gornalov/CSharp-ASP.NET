using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SoftUniBazar.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AdsSeeded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Ads",
                columns: new[] { "Id", "CategoryId", "CreatedOn", "Description", "ImageUrl", "Name", "OwnerId", "Price" },
                values: new object[,]
                {
                    { 1, 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "I have three flowers for selling. They love sunlight and need watering three times a week.", "https://hips.hearstapps.com/hmg-prod/images/spring-flowers-65de4a13478ee.jpg", "Flowers", "493ba206-c3d5-4f28-8372-7824f4bbcf9e", 15m },
                    { 2, 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "I have a car for selling. It is in a good condition and has a new battery.", "https://listings-prod.tcimg.net/listings/146110/48/82/1G1FW6S05P4138248/BF5GFZDUSPJ4G6VPYYJCY4WJV4-og-860.jpg", "Car", "493ba206-c3d5-4f28-8372-7824f4bbcf9e", 15000m }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0b5ca5a6-5732-4895-a96c-cce811834780",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "570595ab-bed5-427f-aec1-f56b1c27839f", "1e512070-2626-46bf-b0ed-8a0da39b80bd" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "493ba206-c3d5-4f28-8372-7824f4bbcf9e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0428ab65-5e43-4085-8e9a-c8a195e9c3c6", "AQAAAAIAAYagAAAAEIHQ0fZlWYDqEtmk3z6uoK+J9DaTAmrsUVcKWuvXlEnSY4K94HD0on+pAZD66uMDrQ==", "8206d977-b391-4ded-9dff-1d231aebf4f2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7a02b826-8f95-44b9-baa6-a4b9804daa3c",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e4589f8b-4618-4f16-9700-919d4f699c6a", "AQAAAAIAAYagAAAAEH74y82QiFHC3EwLUrM+PcwwbMkXmHyVixx/KkCzTsj780i3Q9FQWz3NtX/sUEEFhA==", "a44ba5ce-6410-47cf-a7ec-e115fd57a119" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Ads",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Ads",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0b5ca5a6-5732-4895-a96c-cce811834780",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "a348010e-1658-4a94-8e88-6ac1590c6f86", "72aedb05-74ce-4077-933b-a23c90a8c6e2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "493ba206-c3d5-4f28-8372-7824f4bbcf9e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f26c9d10-767a-411a-8836-d7f9dabef9e3", "AQAAAAIAAYagAAAAENCj7D4Ay6do6cJktuFnivYAAsJaraHpw0eBW/CjV4ppmA1z8MPfP7FbOqLv1eBc6A==", "c407488f-e0c8-4bfe-a628-a2051b9c526e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7a02b826-8f95-44b9-baa6-a4b9804daa3c",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "613d1469-7630-44cd-8a44-a33c174ec17d", "AQAAAAIAAYagAAAAEPW5h8NxMb5Q96iXM9jtrufbozeQn11fd1vaols8iUtZIoNuRSak/FftJKlEWTq/uA==", "712d5f45-e57a-48e3-800d-2a7b7b4dfc84" });
        }
    }
}
