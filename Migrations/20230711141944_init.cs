using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NoGravity.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Carriers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carriers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Planets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SecondName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Starcrafts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CarrierId = table.Column<int>(type: "int", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Starcrafts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Starcrafts_Carriers_CarrierId",
                        column: x => x.CarrierId,
                        principalTable: "Carriers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Starports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlanetId = table.Column<int>(type: "int", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Starports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Starports_Planets_PlanetId",
                        column: x => x.PlanetId,
                        principalTable: "Planets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Journeys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StarcraftId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Journeys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Journeys_Starcrafts_StarcraftId",
                        column: x => x.StarcraftId,
                        principalTable: "Starcrafts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JourneySegments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JourneyId = table.Column<int>(type: "int", nullable: false),
                    DepartureStarportId = table.Column<int>(type: "int", nullable: false),
                    ArrivalStarportId = table.Column<int>(type: "int", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    DepartureDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    ArrivalDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JourneySegments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JourneySegments_Journeys_JourneyId",
                        column: x => x.JourneyId,
                        principalTable: "Journeys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JourneySegments_Starports_ArrivalStarportId",
                        column: x => x.ArrivalStarportId,
                        principalTable: "Starports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JourneySegments_Starports_DepartureStarportId",
                        column: x => x.DepartureStarportId,
                        principalTable: "Starports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JourneyId = table.Column<int>(type: "int", nullable: false),
                    StartStarportId = table.Column<int>(type: "int", nullable: false),
                    EndStarportId = table.Column<int>(type: "int", nullable: false),
                    PassengerFirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PassengerSecondName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CIF = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    SeatNumber = table.Column<int>(type: "int", nullable: false),
                    BookingDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tickets_Journeys_JourneyId",
                        column: x => x.JourneyId,
                        principalTable: "Journeys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tickets_Starports_EndStarportId",
                        column: x => x.EndStarportId,
                        principalTable: "Starports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tickets_Starports_StartStarportId",
                        column: x => x.StartStarportId,
                        principalTable: "Starports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tickets_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SeatAllocations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SegmentId = table.Column<int>(type: "int", nullable: false),
                    SeatNumber = table.Column<int>(type: "int", nullable: false),
                    isVacant = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeatAllocations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SeatAllocations_JourneySegments_SegmentId",
                        column: x => x.SegmentId,
                        principalTable: "JourneySegments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Carriers",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "NASA", "NASA" },
                    { 2, "SpaceX", "SpaceX" },
                    { 3, "Blue Origin", "Blue Origin" }
                });

            migrationBuilder.InsertData(
                table: "Planets",
                columns: new[] { "Id", "Location", "Name" },
                values: new object[,]
                {
                    { 1, "Sol System", "Earth" },
                    { 2, "Sol System", "Mars" },
                    { 3, "Sol System", "Venus" },
                    { 4, "Alpha Centauri System", "Proxima Centauri b" },
                    { 5, "Cygnus Constellation", "Kepler-438b" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "Password", "SecondName" },
                values: new object[,]
                {
                    { 1, "johndoe@example.com", "John", "password123", "Doe" },
                    { 2, "janedoe@example.com", "Jane", "password456", "Doe" },
                    { 3, "bobsmith@example.com", "Bob", "password789", "Smith" },
                    { 4, "alicejohnson@example.com", "Alice", "password101", "Johnson" }
                });

            migrationBuilder.InsertData(
                table: "Starcrafts",
                columns: new[] { "Id", "Capacity", "CarrierId", "Description", "Name" },
                values: new object[,]
                {
                    { 1, 600, 1, "First manned mission to the Moon", "Apollo 11" },
                    { 2, 200, 2, "Reusable rocket designed to carry cargo and people into space", "Falcon 9" },
                    { 3, 250, 3, "Suborbital rocket designed for space tourism", "New Shepard" }
                });

            migrationBuilder.InsertData(
                table: "Starports",
                columns: new[] { "Id", "Location", "Name", "PlanetId" },
                values: new object[,]
                {
                    { 1, "Florida, USA", "Kennedy Space Center", 1 },
                    { 2, "Kazakhstan", "Baikonur Cosmodrome", 1 },
                    { 3, "New Mexico, USA", "Spaceport America", 1 },
                    { 4, "Mars", "Mars Base Alpha", 2 },
                    { 5, "Venus", "Venus Space Station", 3 },
                    { 6, "Mars", "Mars Orbital Gateway", 4 }
                });

            migrationBuilder.InsertData(
                table: "Journeys",
                columns: new[] { "Id", "Name", "Number", "StarcraftId" },
                values: new object[,]
                {
                    { 1, "Moon Landing", "JNY001", 1 },
                    { 2, "Mars Expedition", "JNY002", 2 },
                    { 3, "Venus Flyby", "JNY003", 3 }
                });

            migrationBuilder.InsertData(
                table: "JourneySegments",
                columns: new[] { "Id", "ArrivalDateTime", "ArrivalStarportId", "DepartureDateTime", "DepartureStarportId", "JourneyId", "Order", "Price" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 5, 10, 12, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2023, 5, 10, 8, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, 1, 2000m },
                    { 2, new DateTime(2023, 5, 10, 17, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2023, 5, 10, 13, 0, 0, 0, DateTimeKind.Unspecified), 2, 1, 2, 3500m },
                    { 3, new DateTime(2023, 5, 11, 13, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2023, 5, 11, 9, 0, 0, 0, DateTimeKind.Unspecified), 3, 2, 1, 1500m },
                    { 4, new DateTime(2023, 5, 11, 18, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2023, 5, 10, 14, 0, 0, 0, DateTimeKind.Unspecified), 1, 3, 2, 1750m },
                    { 5, new DateTime(2023, 5, 15, 18, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2023, 5, 15, 14, 0, 0, 0, DateTimeKind.Unspecified), 2, 3, 2, 1750m },
                    { 6, new DateTime(2023, 5, 17, 18, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2023, 5, 16, 14, 0, 0, 0, DateTimeKind.Unspecified), 3, 3, 3, 1750m }
                });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "BookingDateTime", "CIF", "EndStarportId", "JourneyId", "PassengerFirstName", "PassengerSecondName", "SeatNumber", "StartStarportId", "UserId" },
                values: new object[,]
                {
                    { new Guid("189d54c0-eddc-4e04-8a5d-312b91f6ca27"), new DateTime(2023, 7, 11, 17, 19, 44, 188, DateTimeKind.Local).AddTicks(7987), "654321", 3, 1, "Jane", "Doe", 2, 2, 2 },
                    { new Guid("ffd3d2af-1072-49c4-a70c-10f4db8b47b3"), new DateTime(2023, 7, 11, 17, 19, 44, 188, DateTimeKind.Local).AddTicks(7951), "123456", 2, 1, "John", "Doe", 1, 1, 1 }
                });

            migrationBuilder.InsertData(
                table: "SeatAllocations",
                columns: new[] { "Id", "SeatNumber", "SegmentId", "isVacant" },
                values: new object[,]
                {
                    { 1, 17, 1, true },
                    { 2, 20, 1, false },
                    { 3, 17, 2, true },
                    { 4, 4, 2, false },
                    { 5, 13, 3, true },
                    { 6, 18, 3, true },
                    { 7, 18, 5, true },
                    { 8, 17, 5, true },
                    { 9, 18, 6, true }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Journeys_StarcraftId",
                table: "Journeys",
                column: "StarcraftId");

            migrationBuilder.CreateIndex(
                name: "IX_JourneySegments_ArrivalStarportId",
                table: "JourneySegments",
                column: "ArrivalStarportId");

            migrationBuilder.CreateIndex(
                name: "IX_JourneySegments_DepartureStarportId",
                table: "JourneySegments",
                column: "DepartureStarportId");

            migrationBuilder.CreateIndex(
                name: "IX_JourneySegments_JourneyId",
                table: "JourneySegments",
                column: "JourneyId");

            migrationBuilder.CreateIndex(
                name: "IX_SeatAllocations_SegmentId",
                table: "SeatAllocations",
                column: "SegmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Starcrafts_CarrierId",
                table: "Starcrafts",
                column: "CarrierId");

            migrationBuilder.CreateIndex(
                name: "IX_Starports_PlanetId",
                table: "Starports",
                column: "PlanetId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_EndStarportId",
                table: "Tickets",
                column: "EndStarportId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_JourneyId",
                table: "Tickets",
                column: "JourneyId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_StartStarportId",
                table: "Tickets",
                column: "StartStarportId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_UserId",
                table: "Tickets",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SeatAllocations");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "JourneySegments");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Journeys");

            migrationBuilder.DropTable(
                name: "Starports");

            migrationBuilder.DropTable(
                name: "Starcrafts");

            migrationBuilder.DropTable(
                name: "Planets");

            migrationBuilder.DropTable(
                name: "Carriers");
        }
    }
}
