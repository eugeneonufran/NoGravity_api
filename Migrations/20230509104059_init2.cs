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
                keyValue: new Guid("6798d384-d463-4357-b4e3-d04fd5785da6"));

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: new Guid("ef25c76f-66e3-44aa-9251-25b0eb10ddc0"));

            migrationBuilder.UpdateData(
                table: "SeatAllocations",
                keyColumn: "Id",
                keyValue: 3,
                column: "SegmentId",
                value: 3);

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "BookingDateTime", "CIF", "EndStarportId", "JourneyId", "PassengerFirstName", "PassengerSecondName", "SeatNumber", "StartStarportId", "UserId" },
                values: new object[,]
                {
                    { new Guid("2328c3a9-2e08-479a-92ca-ff46dc3a06b2"), new DateTime(2023, 5, 9, 13, 40, 59, 98, DateTimeKind.Local).AddTicks(3775), "654321", 3, 1, "Jane", "Doe", 20, 2, 2 },
                    { new Guid("90bace80-cd60-4b0b-974f-15bff3ee76e4"), new DateTime(2023, 5, 9, 13, 40, 59, 98, DateTimeKind.Local).AddTicks(3734), "123456", 2, 1, "John", "Doe", 40, 1, 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                table: "SeatAllocations",
                keyColumn: "Id",
                keyValue: 3,
                column: "SegmentId",
                value: 1);

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "BookingDateTime", "CIF", "EndStarportId", "JourneyId", "PassengerFirstName", "PassengerSecondName", "SeatNumber", "StartStarportId", "UserId" },
                values: new object[,]
                {
                    { new Guid("6798d384-d463-4357-b4e3-d04fd5785da6"), new DateTime(2023, 5, 9, 13, 38, 50, 397, DateTimeKind.Local).AddTicks(8187), "123456", 2, 1, "John", "Doe", 40, 1, 1 },
                    { new Guid("ef25c76f-66e3-44aa-9251-25b0eb10ddc0"), new DateTime(2023, 5, 9, 13, 38, 50, 397, DateTimeKind.Local).AddTicks(8218), "654321", 3, 1, "Jane", "Doe", 20, 2, 2 }
                });
        }
    }
}
