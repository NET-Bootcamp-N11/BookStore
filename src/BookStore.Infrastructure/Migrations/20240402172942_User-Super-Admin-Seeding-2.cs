using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookStore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UserSuperAdminSeeding2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("5d8ad1b2-07bd-45f1-82e1-d6175d8193a7"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("c3f1dc31-2c0d-4888-bab3-36776cf0e8f3"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("ceb6191e-8375-455c-b051-a1e017c7e002"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("cc62e35b-f79d-46bb-8ca0-bf004731f755"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("2b9298c5-dcf8-412d-8f89-7da1136b4334"), null, "Admin", "ADMIN" },
                    { new Guid("76589353-dc21-41d3-9287-afd4e79372a3"), null, "SuperAdmin", "SUPERADMIN" },
                    { new Guid("f7da20f3-481c-4278-a704-88fa4e37c6d0"), null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PhotoPath", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("90fdaa8e-cce3-4af5-bf46-147dc0b516cc"), 0, "fa95c310-1ae7-454d-a589-af70ed8c0bce", "adminaka0618@gmail.com", false, "Admin aka", false, null, "ADMINAKA0618GMAIL.COM", "ADMINAKA", "AQAAAAIAAYagAAAAEL5dGfxjT0/cQfyvMMMQ+b+ancTXrKrIV/xliyQbGTpwoIO2zJNi/DYKFHKMs05POg==", "123456789", false, null, "EQL6PMQHTWTUEC7XXDY6ZS5M3YS6UAZJ", false, "adminaka" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("2b9298c5-dcf8-412d-8f89-7da1136b4334"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("76589353-dc21-41d3-9287-afd4e79372a3"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("f7da20f3-481c-4278-a704-88fa4e37c6d0"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("90fdaa8e-cce3-4af5-bf46-147dc0b516cc"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("5d8ad1b2-07bd-45f1-82e1-d6175d8193a7"), null, "SuperAdmin", "SUPERADMIN" },
                    { new Guid("c3f1dc31-2c0d-4888-bab3-36776cf0e8f3"), null, "User", "USER" },
                    { new Guid("ceb6191e-8375-455c-b051-a1e017c7e002"), null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PhotoPath", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("cc62e35b-f79d-46bb-8ca0-bf004731f755"), 0, "fa95c310-1ae7-454d-a589-af70ed8c0bce", "adminaka0618@gmail.com", false, "Admin aka", false, null, "ADMINAKA0618GMAIL.COM", null, "AQAAAAIAAYagAAAAEL5dGfxjT0/cQfyvMMMQ+b+ancTXrKrIV/xliyQbGTpwoIO2zJNi/DYKFHKMs05POg==", "123456789", false, null, "EQL6PMQHTWTUEC7XXDY6ZS5M3YS6UAZJ", false, null });
        }
    }
}
