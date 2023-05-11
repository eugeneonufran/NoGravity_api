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
                keyValue: new Guid("11f6bcf1-cb1b-4e64-ae5c-3ceceab5757b"));

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: new Guid("af33021a-90ec-455c-b660-36245c7cb047"));

            migrationBuilder.InsertData(
                table: "SeatAllocations",
                columns: new[] { "Id", "SeatNumber", "SegmentId", "isVacant" },
                values: new object[,]
                {
                    { 8, 20, 3, true },
                    { 9, 20, 2, true }
                });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "BookingDateTime", "CIF", "EndStarportId", "JourneyId", "PassengerFirstName", "PassengerSecondName", "SeatId", "StartStarportId", "UserId" },
                values: new object[,]
                {
                    { new Guid("62d0bc02-0a5c-44f4-b471-eb4ffc74f3a9"), new DateTime(2023, 5, 11, 19, 4, 28, 251, DateTimeKind.Local).AddTicks(1133), "123456", 2, 1, "John", "Doe", 1, 1, 1 },
                    { new Guid("7891d438-5136-40f3-9c48-073ebcb66bef"), new DateTime(2023, 5, 11, 19, 4, 28, 251, DateTimeKind.Local).AddTicks(1174), "654321", 3, 1, "Jane", "Doe", 2, 2, 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SeatAllocations",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "SeatAllocations",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: new Guid("62d0bc02-0a5c-44f4-b471-eb4ffc74f3a9"));

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: new Guid("7891d438-5136-40f3-9c48-073ebcb66bef"));

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "BookingDateTime", "CIF", "EndStarportId", "JourneyId", "PassengerFirstName", "PassengerSecondName", "SeatId", "StartStarportId", "UserId" },
                values: new object[,]
                {
                    { new Guid("11f6bcf1-cb1b-4e64-ae5c-3ceceab5757b"), new DateTime(2023, 5, 11, 19, 0, 51, 410, DateTimeKind.Local).AddTicks(6407), "123456", 2, 1, "John", "Doe", 1, 1, 1 },
                    { new Guid("af33021a-90ec-455c-b660-36245c7cb047"), new DateTime(2023, 5, 11, 19, 0, 51, 410, DateTimeKind.Local).AddTicks(6438), "654321", 3, 1, "Jane", "Doe", 2, 2, 2 }
                });
        }
    }
}
