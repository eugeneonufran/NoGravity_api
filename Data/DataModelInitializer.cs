using Microsoft.EntityFrameworkCore;
using NoGravity.Data.Tables;

namespace NoGravity.Data
{
    public static class DataModelInitializer
    {
        public static void ConfigureModel(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<JourneySegment>()
                .HasOne(js => js.DepartureStarport)
                .WithMany()
                .HasForeignKey(js => js.DepartureStarportId)

                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<JourneySegment>()
                .HasOne(js => js.ArrivalStarport)
                .WithMany()
                .HasForeignKey(js => js.ArrivalStarportId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.StartStarport)
                .WithMany()
                .HasForeignKey(t => t.StartStarportId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<JourneySegment>()
                .Property(j => j.Price)
                .HasColumnType("decimal(10,2)");


        }
        public static void SeedData(ModelBuilder modelBuilder)
        {

            // Add some planets
            modelBuilder.Entity<Planet>().HasData(
            new Planet { Id = 1, Name = "Earth", Location = "Sol System" },
            new Planet { Id = 2, Name = "Mars", Location = "Sol System" },
            new Planet { Id = 3, Name = "Venus", Location = "Sol System" },
            new Planet { Id = 4, Name = "Proxima Centauri b", Location = "Alpha Centauri System" },
            new Planet { Id = 5, Name = "Kepler-438b", Location = "Cygnus Constellation" }
        );

            // Add some carriers
            modelBuilder.Entity<Carrier>().HasData(
                new Carrier { Id = 1, Name = "NASA", Description = "NASA" },
                new Carrier { Id = 2, Name = "SpaceX", Description = "SpaceX" },
                new Carrier { Id = 3, Name = "Blue Origin", Description = "Blue Origin" }
            );

            // Add some starcrafts
            modelBuilder.Entity<Starcraft>().HasData(
                new Starcraft { Id = 1, Name = "Apollo 11", CarrierId = 1, Capacity = 600, Description = "First manned mission to the Moon" },
                new Starcraft { Id = 2, Name = "Falcon 9", CarrierId = 2, Capacity = 200, Description = "Reusable rocket designed to carry cargo and people into space" },
                new Starcraft { Id = 3, Name = "New Shepard", CarrierId = 3, Capacity = 250, Description = "Suborbital rocket designed for space tourism" }
            );

            // Add some starports
            modelBuilder.Entity<Starport>().HasData(
                new Starport { Id = 1, Name = "Kennedy Space Center", PlanetId = 1, Location = "Florida, USA" },
                new Starport { Id = 2, Name = "Baikonur Cosmodrome", PlanetId = 1, Location = "Kazakhstan" },
                new Starport { Id = 3, Name = "Spaceport America", PlanetId = 1, Location = "New Mexico, USA" },
                new Starport { Id = 4, Name = "Mars Base Alpha", PlanetId = 2, Location = "Mars" },
                new Starport { Id = 5, Name = "Venus Space Station", PlanetId = 3, Location = "Venus" },
                new Starport { Id = 6, Name = "Mars Orbital Gateway", PlanetId = 4, Location = "Mars" }
            );

            // Add some journeys
            modelBuilder.Entity<Journey>().HasData(
                new Journey { Id = 1, Number = "JNY001", Name = "Moon Landing", StarcraftId = 1 },
                new Journey { Id = 2, Number = "JNY002", Name = "Mars Expedition", StarcraftId = 2 },
                new Journey { Id = 3, Number = "JNY003", Name = "Venus Flyby", StarcraftId = 3 }
            );

            modelBuilder.Entity<SeatAllocation>().HasData(
                new SeatAllocation { Id = 1, SegmentId = 1, SeatNumber = 17, isVacant = true },
                new SeatAllocation { Id = 2, SegmentId = 2, SeatNumber = 12, isVacant = true },
                new SeatAllocation { Id = 3, SegmentId = 3, SeatNumber = 13, isVacant = true }
            );

            // Add some journey segments
            modelBuilder.Entity<JourneySegment>().HasData(
                new JourneySegment
                {
                    Id = 1,
                    JourneyId = 1,
                    DepartureStarportId = 1,
                    ArrivalStarportId = 2,
                    Order = 1,
                    DepartureDateTime = DateTime.Parse("2023-05-10 08:00:00"),
                    ArrivalDateTime = DateTime.Parse("2023-05-10 12:00:00"),
                    Price = 2000
                },
                new JourneySegment
                {
                    Id = 2,
                    JourneyId = 1,
                    DepartureStarportId = 2,
                    ArrivalStarportId = 3,
                    Order = 2,
                    DepartureDateTime = DateTime.Parse("2023-05-10 13:00:00"),
                    ArrivalDateTime = DateTime.Parse("2023-05-10 17:00:00"),
                    Price = 3500
                },
                new JourneySegment
                {
                    Id = 3,
                    JourneyId = 2,
                    DepartureStarportId = 3,
                    ArrivalStarportId = 4,
                    Order = 1,
                    DepartureDateTime = DateTime.Parse("2023-05-11 09:00:00"),
                    ArrivalDateTime = DateTime.Parse("2023-05-11 13:00:00"),
                    Price = 1500
                },
                new JourneySegment
                {
                    Id = 4,
                    JourneyId = 2,
                    DepartureStarportId = 2,
                    ArrivalStarportId = 6,
                    Order = 2,
                    DepartureDateTime = DateTime.Parse("2023-05-11 14:00:00"),
                    ArrivalDateTime = DateTime.Parse("2023-05-11 18:00:00"),
                    Price = 1750
                }
            );
            modelBuilder.Entity<User>().HasData(
                    new User { Id = 1, FirstName = "John", SecondName = "Doe", Email = "johndoe@example.com", Password = "password123" },
                    new User { Id = 2, FirstName = "Jane", SecondName = "Doe", Email = "janedoe@example.com", Password = "password456" },
                    new User { Id = 3, FirstName = "Bob", SecondName = "Smith", Email = "bobsmith@example.com", Password = "password789" },
                    new User { Id = 4, FirstName = "Alice", SecondName = "Johnson", Email = "alicejohnson@example.com", Password = "password101" }
            );

            modelBuilder.Entity<Ticket>().HasData(
                    new Ticket
                    {
                        Id = Guid.NewGuid(),
                        JourneyId = 1,
                        StartStarportId = 1,
                        EndStarportId = 2,
                        PassengerFirstName = "John",
                        PassengerSecondName = "Doe",
                        CIF = "123456",
                        UserId = 1,
                        SeatNumber = 40,
                        BookingDateTime = DateTime.Now
                    },
                    new Ticket
                    {
                        Id = Guid.NewGuid(),
                        JourneyId = 1,
                        StartStarportId = 2,
                        EndStarportId = 3,
                        PassengerFirstName = "Jane",
                        PassengerSecondName = "Doe",
                        CIF = "654321",
                        UserId = 2,
                        SeatNumber = 20,
                        BookingDateTime = DateTime.Now
                    }
             );

        }
    }
}
