using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookStore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RoleSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("003bf50c-db7c-4e58-914d-7d8a49960f61"), null, "Admin", null },
                    { new Guid("048fea76-1b8d-4001-80c3-0845ead9b7ed"), null, "User", null },
                    { new Guid("1cdb36ff-8ac5-4ef8-9147-41ee3cb793ac"), null, "SuperAdmin", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("003bf50c-db7c-4e58-914d-7d8a49960f61"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("048fea76-1b8d-4001-80c3-0845ead9b7ed"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("1cdb36ff-8ac5-4ef8-9147-41ee3cb793ac"));
        }
    }
}
