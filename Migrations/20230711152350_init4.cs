using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NoGravity.Migrations
{
    /// <inheritdoc />
    public partial class init4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SeatAllocations",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "SeatAllocations",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "SeatAllocations",
                keyColumn: "Id",
                keyValue: 9);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "SeatAllocations",
                columns: new[] { "Id", "SeatNumber", "SegmentId", "isVacant" },
                values: new object[,]
                {
                    { 7, 18, 5, true },
                    { 8, 17, 5, true },
                    { 9, 18, 6, true }
                });
        }
    }
}
