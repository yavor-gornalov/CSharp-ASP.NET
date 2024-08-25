using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HouseRentingSystem.Data.Migrations
{
    public partial class UserClaimsSeederFixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUserClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "UserId" },
                values: new object[,]
                {
                    { 1, "user:fullname", "Linda Michaels", "dea12856-c198-4129-b3f3-b893d8395082" },
                    { 2, "user:fullname", "Teodor Lesly", "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e" },
                    { 3, "user:fullname", "Great Admin", "f1b0b8e3-6b7d-4b3e-8b1b-3f3e1b7e6b1e" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "aa674b34-fb5b-4996-be43-1b25058e1c46", "AQAAAAEAACcQAAAAENKqzLLTEs96SeFXEUQwAbK5af+hkXg78LND+eOAqeVK3wfnR2y42RJjOv71BfUujg==", "4ac2cdd1-41a2-4f5d-96bd-4079963843b1" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "66a0ee86-4f3c-4e39-b6fa-726f18b29a37", "AQAAAAEAACcQAAAAEP/yXC9HChe6Uiq75tB3bAYX0JO6KALvYI/f+dDbLWjSLxyhTc/Y3Zf0vyB08+jqyg==", "1df77f63-514f-48c7-a410-5eb35d31b0d8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f1b0b8e3-6b7d-4b3e-8b1b-3f3e1b7e6b1e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0e79dbc1-84be-472f-b5d9-6b4d6f1a2d34", "AQAAAAEAACcQAAAAEOo+pyyDQTxBsbu6hX+qb3uoC6KUNH4sKh5a+s3rGfYEPbx10KRETlNhZTUpP8raWA==", "f1e4edf0-becd-49d2-93fc-9a3c976413bd" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 3);

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

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f1b0b8e3-6b7d-4b3e-8b1b-3f3e1b7e6b1e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "87405b7a-3e51-4dbd-8d1e-7c3857df92f0", "AQAAAAEAACcQAAAAELhM+bQZPIEciD6RXir5U/Fe6Cj6dIAopDvg/s/907Ph52OImNVhaSiecUHMpI0Tgw==", "1ec72ab7-c173-4874-a15d-da86414b95bd" });
        }
    }
}
