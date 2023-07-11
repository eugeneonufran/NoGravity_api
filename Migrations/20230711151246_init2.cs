using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NoGravity.Migrations
{
    /// <inheritdoc />
    public partial class init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: new Guid("189d54c0-eddc-4e04-8a5d-312b91f6ca27"));

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: new Guid("ffd3d2af-1072-49c4-a70c-10f4db8b47b3"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "BookingDateTime", "CIF", "EndStarportId", "JourneyId", "PassengerFirstName", "PassengerSecondName", "SeatNumber", "StartStarportId", "UserId" },
                values: new object[,]
                {
                    { new Guid("189d54c0-eddc-4e04-8a5d-312b91f6ca27"), new DateTime(2023, 7, 11, 17, 19, 44, 188, DateTimeKind.Local).AddTicks(7987), "654321", 3, 1, "Jane", "Doe", 2, 2, 2 },
                    { new Guid("ffd3d2af-1072-49c4-a70c-10f4db8b47b3"), new DateTime(2023, 7, 11, 17, 19, 44, 188, DateTimeKind.Local).AddTicks(7951), "123456", 2, 1, "John", "Doe", 1, 1, 1 }
                });
        }
    }
}
