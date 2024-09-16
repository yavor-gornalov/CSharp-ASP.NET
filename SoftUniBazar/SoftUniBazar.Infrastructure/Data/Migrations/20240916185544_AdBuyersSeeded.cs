using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SoftUniBazar.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AdBuyersSeeded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AdBuyers",
                columns: new[] { "AdId", "BuyerId" },
                values: new object[,]
                {
                    { 1, "7a02b826-8f95-44b9-baa6-a4b9804daa3c" },
                    { 2, "7a02b826-8f95-44b9-baa6-a4b9804daa3c" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0b5ca5a6-5732-4895-a96c-cce811834780",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "ddab6020-948d-4adb-8b8e-e0e949cf3619", "12a3f99d-439e-4b90-900b-eff58daa7bce" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "493ba206-c3d5-4f28-8372-7824f4bbcf9e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0cda6a1e-4a81-4c98-9d50-007f2ac4205e", "AQAAAAIAAYagAAAAELOwN5ql0C6CcMPZkF29euOng/OSmwxjL5zARdbqcDtiDNiuIH4lsU4VHLGfXY1k3A==", "726fb172-69e0-4b8c-9a8d-b8928ff942da" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7a02b826-8f95-44b9-baa6-a4b9804daa3c",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "02995dba-82d0-443c-ad8a-a4060ae7b7dc", "AQAAAAIAAYagAAAAEDxoASOeVEfuqI5wfnnvhSwiYK3lygBDKR/79MQQnpqdJEL04sc7Z/6maw0+r7Eq/Q==", "75ff625c-3ab9-4567-b7ab-9f1eaf3635f5" });
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
    }
}
