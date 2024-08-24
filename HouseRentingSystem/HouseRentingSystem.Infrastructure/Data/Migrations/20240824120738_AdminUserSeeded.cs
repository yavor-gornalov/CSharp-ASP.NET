using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HouseRentingSystem.Data.Migrations
{
    public partial class AdminUserSeeded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "afc820e8-0714-4378-bec0-0c3c143e558b", "AQAAAAEAACcQAAAAEKo4VKbnbEtfrAKFdfOIAuHK5JIDdIFu/EpAHvQ7cqLXzvSnWhrfkI/S9K7J6xYO+A==", "5b245b7e-be0e-4419-9e76-0151eff039de" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1b5505c6-c88b-408e-80e8-e8f342f08ef8", "AQAAAAEAACcQAAAAEGSDFrmp1H7Z4uquP7xgmEJ3l7DYjqK35qFerLORwbHKJR8MnWCXJLhWoULSoQ8/Ig==", "12fd63e8-bba8-42c9-b18b-b9f9a5fd4257" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "f1b0b8e3-6b7d-4b3e-8b1b-3f3e1b7e6b1e", 0, "87405b7a-3e51-4dbd-8d1e-7c3857df92f0", "ApplicationUser", "admin@mail.com", false, "Great", "Admin", false, null, "ADMIN@MAIL.COM", "ADMIN@MAIL.COM", "AQAAAAEAACcQAAAAELhM+bQZPIEciD6RXir5U/Fe6Cj6dIAopDvg/s/907Ph52OImNVhaSiecUHMpI0Tgw==", null, false, "1ec72ab7-c173-4874-a15d-da86414b95bd", false, "admin@mail.com" });

            migrationBuilder.UpdateData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: "https://www.shutterstock.com/shutterstock/photos/338950502/display_1500/stock-photo-big-luxury-modern-house-at-dusk-night-time-in-suburbs-of-vancouver-canada-338950502.jpg");

            migrationBuilder.InsertData(
                table: "Agents",
                columns: new[] { "Id", "PhoneNumber", "UserId" },
                values: new object[] { 3, "+359888111111", "f1b0b8e3-6b7d-4b3e-8b1b-3f3e1b7e6b1e" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Agents",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f1b0b8e3-6b7d-4b3e-8b1b-3f3e1b7e6b1e");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "36acc937-8d4f-487b-a6a7-9c5f5b53b34c", "AQAAAAEAACcQAAAAEKQeOE4/kVZISTM0TTaXX2CEIUshcd99TGkfLPnYsKwEfF9TmMoL2m5DWDWAh6O9kQ==", "10428f80-a2a1-4ec8-b24a-ca7b466e30e2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4d5c1c2c-e5f8-44a7-b06a-1da0098b6d21", "AQAAAAEAACcQAAAAEAFrcJxikf4qmEWSaMWTzdiADQDjo6ICMkbcC/b1Yj+PBT73+a85NZPgA2T/NJprXQ==", "d2b99588-b40d-43a9-b6de-63c35baca936" });

            migrationBuilder.UpdateData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: "https://www.luxury-architecture.net/wp-content/uploads/2017/12/1513217889-7597-FAIRWAYS-010.jpg");
        }
    }
}
