using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NoGravity.Migrations
{
    /// <inheritdoc />
    public partial class init3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "JourneySegments",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "JourneySegments",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "JourneySegments",
                keyColumn: "Id",
                keyValue: 6);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "JourneySegments",
                columns: new[] { "Id", "ArrivalDateTime", "ArrivalStarportId", "DepartureDateTime", "DepartureStarportId", "JourneyId", "Order", "Price" },
                values: new object[,]
                {
                    { 4, new DateTime(2023, 5, 11, 18, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2023, 5, 10, 14, 0, 0, 0, DateTimeKind.Unspecified), 1, 3, 2, 1750m },
                    { 5, new DateTime(2023, 5, 15, 18, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2023, 5, 15, 14, 0, 0, 0, DateTimeKind.Unspecified), 2, 3, 2, 1750m },
                    { 6, new DateTime(2023, 5, 17, 18, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2023, 5, 16, 14, 0, 0, 0, DateTimeKind.Unspecified), 3, 3, 3, 1750m }
                });
        }
    }
}
