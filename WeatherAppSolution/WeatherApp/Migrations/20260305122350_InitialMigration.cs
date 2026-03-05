using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeatherApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WeatherHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Temp = table.Column<double>(type: "float", nullable: false),
                    TempMin = table.Column<double>(type: "float", nullable: false),
                    TempMax = table.Column<double>(type: "float", nullable: false),
                    Pressure = table.Column<double>(type: "float", nullable: false),
                    FeelsLike = table.Column<double>(type: "float", nullable: false),
                    GrndLevel = table.Column<double>(type: "float", nullable: false),
                    SeaLevel = table.Column<double>(type: "float", nullable: false),
                    Humidity = table.Column<double>(type: "float", nullable: false),
                    RequestedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WeatherHistories_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WeatherHistories_RequestedAt",
                table: "WeatherHistories",
                column: "RequestedAt");

            migrationBuilder.CreateIndex(
                name: "IX_WeatherHistories_UserId",
                table: "WeatherHistories",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WeatherHistories");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
