using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoilerMonitoringAPI.Migrations
{
    /// <inheritdoc />
    public partial class members : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    DeviceID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeviceName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    firstNoice = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastNoice = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.DeviceID);
                });

            migrationBuilder.CreateTable(
                name: "Homes",
                columns: table => new
                {
                    HomeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HomeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefeshTokenExpiryTime = table.Column<DateTime>(type: "datetime2", nullable: true)
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
                    BoilerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BoilerType = table.Column<int>(type: "int", nullable: false),
                    BoilerStatus = table.Column<int>(type: "int", nullable: false),
                    IsOpen = table.Column<bool>(type: "bit", nullable: false),
                    minFuelLevel = table.Column<double>(type: "float", nullable: false),
                    maxFuelLevel = table.Column<double>(type: "float", nullable: false),
                    currentFuelLevel = table.Column<double>(type: "float", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HomeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeviceID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boilers", x => x.BoilerID);
                    table.ForeignKey(
                        name: "FK_Boilers_Devices_DeviceID",
                        column: x => x.DeviceID,
                        principalTable: "Devices",
                        principalColumn: "DeviceID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Boilers_Homes_HomeID",
                        column: x => x.HomeID,
                        principalTable: "Homes",
                        principalColumn: "HomeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HomeMembers",
                columns: table => new
                {
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HomeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomeMembers", x => new { x.UserID, x.HomeID });
                    table.ForeignKey(
                        name: "FK_HomeMembers_Homes_HomeID",
                        column: x => x.HomeID,
                        principalTable: "Homes",
                        principalColumn: "HomeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HomeMembers_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Boilers_DeviceID",
                table: "Boilers",
                column: "DeviceID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Boilers_HomeID",
                table: "Boilers",
                column: "HomeID");

            migrationBuilder.CreateIndex(
                name: "IX_HomeMembers_HomeID",
                table: "HomeMembers",
                column: "HomeID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Boilers");

            migrationBuilder.DropTable(
                name: "HomeMembers");

            migrationBuilder.DropTable(
                name: "Devices");

            migrationBuilder.DropTable(
                name: "Homes");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
