using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookStore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UserSuperAdminSeeding3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { new Guid("0485df0e-8f06-4852-82e1-64ae9e7904f7"), null, "SuperAdmin", "SUPERADMIN" },
                    { new Guid("4d028a3f-2e32-43c4-a168-a3e90eb602f1"), null, "Admin", "ADMIN" },
                    { new Guid("cc1c267c-6b52-42e4-a1d2-f8f8e366f609"), null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PhotoPath", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("ca9d3855-2c7d-427a-9364-d37dac608b55"), 0, "fa95c310-1ae7-454d-a589-af70ed8c0bce", "adminaka0618@gmail.com", false, "Admin aka", false, null, "ADMINAKA@0618GMAIL.COM", "ADMINAKA", "AQAAAAIAAYagAAAAEL5dGfxjT0/cQfyvMMMQ+b+ancTXrKrIV/xliyQbGTpwoIO2zJNi/DYKFHKMs05POg==", "123456789", false, null, "EQL6PMQHTWTUEC7XXDY6ZS5M3YS6UAZJ", false, "adminaka" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("0485df0e-8f06-4852-82e1-64ae9e7904f7"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4d028a3f-2e32-43c4-a168-a3e90eb602f1"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("cc1c267c-6b52-42e4-a1d2-f8f8e366f609"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("ca9d3855-2c7d-427a-9364-d37dac608b55"));

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
    }
}
