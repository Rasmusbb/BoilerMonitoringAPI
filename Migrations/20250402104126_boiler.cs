using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoilerMonitoringAPI.Migrations
{
    /// <inheritdoc />
    public partial class boiler : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Homes",
                columns: table => new
                {
                    HomeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HomeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Homes", x => x.HomeID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "Boilers",
                columns: table => new
                {
                    BoilerID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BoilerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BoilerType = table.Column<int>(type: "int", nullable: false),
                    BoilerStatus = table.Column<int>(type: "int", nullable: false),
                    FillLevel = table.Column<int>(type: "int", nullable: false),
                    HomeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boilers", x => x.BoilerID);
                    table.ForeignKey(
                        name: "FK_Boilers_Homes_HomeID",
                        column: x => x.HomeID,
                        principalTable: "Homes",
                        principalColumn: "HomeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserHomes",
                columns: table => new
                {
                    HomeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserHomes", x => new { x.HomeID, x.UserID });
                    table.ForeignKey(
                        name: "FK_UserHomes_Homes_HomeID",
                        column: x => x.HomeID,
                        principalTable: "Homes",
                        principalColumn: "HomeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserHomes_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Boilers_HomeID",
                table: "Boilers",
                column: "HomeID");

            migrationBuilder.CreateIndex(
                name: "IX_UserHomes_UserID",
                table: "UserHomes",
                column: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Boilers");

            migrationBuilder.DropTable(
                name: "UserHomes");

            migrationBuilder.DropTable(
                name: "Homes");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
