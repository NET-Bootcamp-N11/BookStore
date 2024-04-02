using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookStore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UserSuperAdminSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("480cb02c-7a14-471b-a02e-0a5ecf6a9910"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4a74caf8-3995-4f1d-996e-808f1a659c3e"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("baf9e5fd-afe2-45d9-bbb2-f3bc6ffc8495"));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { new Guid("480cb02c-7a14-471b-a02e-0a5ecf6a9910"), null, "User", "USER" },
                    { new Guid("4a74caf8-3995-4f1d-996e-808f1a659c3e"), null, "Admin", "ADMIN" },
                    { new Guid("baf9e5fd-afe2-45d9-bbb2-f3bc6ffc8495"), null, "SuperAdmin", "SUPERADMIN" }
                });
        }
    }
}
