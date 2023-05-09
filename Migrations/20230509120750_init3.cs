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
                table: "Tickets",
                keyColumn: "Id",
                keyValue: new Guid("2328c3a9-2e08-479a-92ca-ff46dc3a06b2"));

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: new Guid("90bace80-cd60-4b0b-974f-15bff3ee76e4"));

            migrationBuilder.UpdateData(
                table: "JourneySegments",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ArrivalStarportId", "DepartureStarportId" },
                values: new object[] { 4, 1 });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "BookingDateTime", "CIF", "EndStarportId", "JourneyId", "PassengerFirstName", "PassengerSecondName", "SeatNumber", "StartStarportId", "UserId" },
                values: new object[,]
                {
                    { new Guid("774d885c-0b10-4754-a2f0-0066a9e777d7"), new DateTime(2023, 5, 9, 15, 7, 50, 597, DateTimeKind.Local).AddTicks(5738), "123456", 2, 1, "John", "Doe", 40, 1, 1 },
                    { new Guid("99b71caa-dd26-4b67-9af6-11b42e42f388"), new DateTime(2023, 5, 9, 15, 7, 50, 597, DateTimeKind.Local).AddTicks(5771), "654321", 3, 1, "Jane", "Doe", 20, 2, 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: new Guid("774d885c-0b10-4754-a2f0-0066a9e777d7"));

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: new Guid("99b71caa-dd26-4b67-9af6-11b42e42f388"));

            migrationBuilder.UpdateData(
                table: "JourneySegments",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ArrivalStarportId", "DepartureStarportId" },
                values: new object[] { 6, 2 });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "BookingDateTime", "CIF", "EndStarportId", "JourneyId", "PassengerFirstName", "PassengerSecondName", "SeatNumber", "StartStarportId", "UserId" },
                values: new object[,]
                {
                    { new Guid("2328c3a9-2e08-479a-92ca-ff46dc3a06b2"), new DateTime(2023, 5, 9, 13, 40, 59, 98, DateTimeKind.Local).AddTicks(3775), "654321", 3, 1, "Jane", "Doe", 20, 2, 2 },
                    { new Guid("90bace80-cd60-4b0b-974f-15bff3ee76e4"), new DateTime(2023, 5, 9, 13, 40, 59, 98, DateTimeKind.Local).AddTicks(3734), "123456", 2, 1, "John", "Doe", 40, 1, 1 }
                });
        }
    }
}
