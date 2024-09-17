using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SoftUniBazar.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedsAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "0b5ca5a6-5732-4895-a96c-cce811834780", 0, "8e79271c-e0ec-49d0-aada-02a31a07aef6", "seller@softuni.com", false, false, null, "SELLER@SOFTUNI.COM", "SELLER@SOFTUNI.COM", "AQAAAAIAAYagAAAAEB9wfmnYGuxuRBTq/zRTtSbM8nY8wBdIFxefhA+zyM0GdwGjdHqj/hUDEIvKxMr20w==", null, false, "aa00a083-2e30-4111-818a-2b8faa9f36d1", false, "seller@softuni.com" },
                    { "493ba206-c3d5-4f28-8372-7824f4bbcf9e", 0, "e7423796-f215-4331-bca3-599a91780e0e", "guest@softuni.com", false, false, null, "GUEST@SOFTUNI.COM", "GUEST@SOFTUNI.COM", "AQAAAAIAAYagAAAAEJY4c5Nof/fttgzbg1E6Z6n1hxsV4rH4o90cbH4GolMK/8hDeB1nSNvAl4fATFTcwg==", null, false, "2abcfaf0-7487-4405-a129-124fd1f3fd87", false, "guest@softuni.com" },
                    { "7a02b826-8f95-44b9-baa6-a4b9804daa3c", 0, "9d4f5add-2f66-4082-86d3-30f79bca1571", "buyer@softuni.com", false, false, null, "BUYER@SOFTUNI.COM", "BUYER@SOFTUNI.COM", "AQAAAAIAAYagAAAAEE3qz/ngUVnwWHU9RXG/+kurJBN0Zc31CCzxjZg+eO52P1gk+23cgtyn/Ug6KBGqlQ==", null, false, "d85c27ad-6af0-4443-9338-602798c647f3", false, "buyer@softuni.com" }
                });

            migrationBuilder.InsertData(
                table: "Ads",
                columns: new[] { "Id", "CategoryId", "CreatedOn", "Description", "ImageUrl", "Name", "OwnerId", "Price" },
                values: new object[,]
                {
                    { 1, 4, new DateTime(2024, 9, 15, 19, 15, 0, 0, DateTimeKind.Unspecified), "I have three flowers for selling. They love sunlight and need watering three times a week.", "https://hips.hearstapps.com/hmg-prod/images/spring-flowers-65de4a13478ee.jpg", "Flowers", "0b5ca5a6-5732-4895-a96c-cce811834780", 15m },
                    { 2, 2, new DateTime(2024, 9, 17, 14, 30, 0, 0, DateTimeKind.Unspecified), "I have a car for selling. It is in a good condition and has a new battery.", "https://listings-prod.tcimg.net/listings/146110/48/82/1G1FW6S05P4138248/BF5GFZDUSPJ4G6VPYYJCY4WJV4-og-860.jpg", "Car", "0b5ca5a6-5732-4895-a96c-cce811834780", 12000m }
                });

            migrationBuilder.InsertData(
                table: "AdBuyers",
                columns: new[] { "AdId", "BuyerId" },
                values: new object[,]
                {
                    { 1, "7a02b826-8f95-44b9-baa6-a4b9804daa3c" },
                    { 2, "7a02b826-8f95-44b9-baa6-a4b9804daa3c" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AdBuyers",
                keyColumns: new[] { "AdId", "BuyerId" },
                keyValues: new object[] { 1, "7a02b826-8f95-44b9-baa6-a4b9804daa3c" });

            migrationBuilder.DeleteData(
                table: "AdBuyers",
                keyColumns: new[] { "AdId", "BuyerId" },
                keyValues: new object[] { 2, "7a02b826-8f95-44b9-baa6-a4b9804daa3c" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "493ba206-c3d5-4f28-8372-7824f4bbcf9e");

            migrationBuilder.DeleteData(
                table: "Ads",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Ads",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7a02b826-8f95-44b9-baa6-a4b9804daa3c");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0b5ca5a6-5732-4895-a96c-cce811834780");
        }
    }
}
