using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Balder.FiapCloudGames.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DataSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "Password", "Role" },
                values: new object[,]
                {
                    { new Guid("a64eaba9-8753-466a-9500-7ac3ada50342"), "admin@admin.com", "Admin", "$2a$11$qMAkSQCPQ/AgL3JqFv/aI.TNxO2FRFs8rWjzx1c2Zm6PWwupVrHXi", "admin" },
                    { new Guid("a7155384-ed2e-47a8-b603-60a5aa9ba424"), "user@fiapgames.com", "User", "$2a$11$dACyarlQmDHImkKicCYvtem86VRXQfv9SgI7XMH/Ol.P0ducjIeB2", "user" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a64eaba9-8753-466a-9500-7ac3ada50342"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a7155384-ed2e-47a8-b603-60a5aa9ba424"));
        }
    }
}
