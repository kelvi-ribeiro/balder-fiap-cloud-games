using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Balder.FiapCloudGames.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Platform = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(8,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            //#region CustomInsert
            //migrationBuilder.InsertData(
            //    table: "Users",
            //    columns: new[] { "Id", "Name", "Email", "Password", "Role" },
            //    values: new object[,]
            //    {
            //        { Guid.NewGuid(), "Admin", "admin@admin.com", "$2a$11$qMAkSQCPQ/AgL3JqFv/aI.TNxO2FRFs8rWjzx1c2Zm6PWwupVrHXi", "admin" }, //Password - adminFG123!
            //        { Guid.NewGuid(), "User", "user@fiapgames.com", "$2a$11$dACyarlQmDHImkKicCYvtem86VRXQfv9SgI7XMH/Ol.P0ducjIeB2", "user" }  //Password - userFG123!
            //    });
            //#endregion
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
