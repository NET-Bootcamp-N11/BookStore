using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UserRoleTableSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("de78ed68-8bfa-4ece-8e58-39df3b821a7e"));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("814a9fe9-4f17-4fb0-a10f-0cdda6d837c1"),
                columns: new[] { "Name", "NormalizedName" },
                values: new object[] { "SuperAdmin", "SUPERADMIN" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("c2597d72-c975-48af-8c1e-a2fb033a22dd"),
                columns: new[] { "Name", "NormalizedName" },
                values: new object[] { "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PhotoPath", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("ca9d3855-2c7d-427a-9364-d37dac608b55"), 0, "fa95c310-1ae7-454d-a589-af70ed8c0bce", "adminaka0618@gmail.com", false, "Admin aka", false, null, "ADMINAKA0618@GMAIL.COM", "ADMINAKA", "AQAAAAIAAYagAAAAEL5dGfxjT0/cQfyvMMMQ+b+ancTXrKrIV/xliyQbGTpwoIO2zJNi/DYKFHKMs05POg==", "123456789", false, null, "EQL6PMQHTWTUEC7XXDY6ZS5M3YS6UAZJ", false, "adminaka" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("814a9fe9-4f17-4fb0-a10f-0cdda6d837c1"), new Guid("ca9d3855-2c7d-427a-9364-d37dac608b55") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("814a9fe9-4f17-4fb0-a10f-0cdda6d837c1"), new Guid("ca9d3855-2c7d-427a-9364-d37dac608b55") });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("ca9d3855-2c7d-427a-9364-d37dac608b55"));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("814a9fe9-4f17-4fb0-a10f-0cdda6d837c1"),
                columns: new[] { "Name", "NormalizedName" },
                values: new object[] { "User", "USER" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("c2597d72-c975-48af-8c1e-a2fb033a22dd"),
                columns: new[] { "Name", "NormalizedName" },
                values: new object[] { "SuperAdmin", "SUPERADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PhotoPath", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("de78ed68-8bfa-4ece-8e58-39df3b821a7e"), 0, "fa95c310-1ae7-454d-a589-af70ed8c0bce", "adminaka0618@gmail.com", false, "Admin aka", false, null, "ADMINAKA0618@GMAIL.COM", "ADMINAKA", "AQAAAAIAAYagAAAAEL5dGfxjT0/cQfyvMMMQ+b+ancTXrKrIV/xliyQbGTpwoIO2zJNi/DYKFHKMs05POg==", "123456789", false, null, "EQL6PMQHTWTUEC7XXDY6ZS5M3YS6UAZJ", false, "adminaka" });
        }
    }
}
