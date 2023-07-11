﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NoGravity.Data.DataModel;

#nullable disable

namespace NoGravity.Migrations
{
    [DbContext(typeof(NoGravityDbContext))]
    [Migration("20230711141944_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("NoGravity.Data.Tables.Carrier", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Carriers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "NASA",
                            Name = "NASA"
                        },
                        new
                        {
                            Id = 2,
                            Description = "SpaceX",
                            Name = "SpaceX"
                        },
                        new
                        {
                            Id = 3,
                            Description = "Blue Origin",
                            Name = "Blue Origin"
                        });
                });

            modelBuilder.Entity("NoGravity.Data.Tables.Journey", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StarcraftId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StarcraftId");

                    b.ToTable("Journeys");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Moon Landing",
                            Number = "JNY001",
                            StarcraftId = 1
                        },
                        new
                        {
                            Id = 2,
                            Name = "Mars Expedition",
                            Number = "JNY002",
                            StarcraftId = 2
                        },
                        new
                        {
                            Id = 3,
                            Name = "Venus Flyby",
                            Number = "JNY003",
                            StarcraftId = 3
                        });
                });

            modelBuilder.Entity("NoGravity.Data.Tables.JourneySegment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("ArrivalDateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("ArrivalStarportId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DepartureDateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("DepartureStarportId")
                        .HasColumnType("int");

                    b.Property<int>("JourneyId")
                        .HasColumnType("int");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(10,2)");

                    b.HasKey("Id");

                    b.HasIndex("ArrivalStarportId");

                    b.HasIndex("DepartureStarportId");

                    b.HasIndex("JourneyId");

                    b.ToTable("JourneySegments");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ArrivalDateTime = new DateTime(2023, 5, 10, 12, 0, 0, 0, DateTimeKind.Unspecified),
                            ArrivalStarportId = 2,
                            DepartureDateTime = new DateTime(2023, 5, 10, 8, 0, 0, 0, DateTimeKind.Unspecified),
                            DepartureStarportId = 1,
                            JourneyId = 1,
                            Order = 1,
                            Price = 2000m
                        },
                        new
                        {
                            Id = 2,
                            ArrivalDateTime = new DateTime(2023, 5, 10, 17, 0, 0, 0, DateTimeKind.Unspecified),
                            ArrivalStarportId = 3,
                            DepartureDateTime = new DateTime(2023, 5, 10, 13, 0, 0, 0, DateTimeKind.Unspecified),
                            DepartureStarportId = 2,
                            JourneyId = 1,
                            Order = 2,
                            Price = 3500m
                        },
                        new
                        {
                            Id = 3,
                            ArrivalDateTime = new DateTime(2023, 5, 11, 13, 0, 0, 0, DateTimeKind.Unspecified),
                            ArrivalStarportId = 4,
                            DepartureDateTime = new DateTime(2023, 5, 11, 9, 0, 0, 0, DateTimeKind.Unspecified),
                            DepartureStarportId = 3,
                            JourneyId = 2,
                            Order = 1,
                            Price = 1500m
                        },
                        new
                        {
                            Id = 4,
                            ArrivalDateTime = new DateTime(2023, 5, 11, 18, 0, 0, 0, DateTimeKind.Unspecified),
                            ArrivalStarportId = 2,
                            DepartureDateTime = new DateTime(2023, 5, 10, 14, 0, 0, 0, DateTimeKind.Unspecified),
                            DepartureStarportId = 1,
                            JourneyId = 3,
                            Order = 2,
                            Price = 1750m
                        },
                        new
                        {
                            Id = 5,
                            ArrivalDateTime = new DateTime(2023, 5, 15, 18, 0, 0, 0, DateTimeKind.Unspecified),
                            ArrivalStarportId = 3,
                            DepartureDateTime = new DateTime(2023, 5, 15, 14, 0, 0, 0, DateTimeKind.Unspecified),
                            DepartureStarportId = 2,
                            JourneyId = 3,
                            Order = 2,
                            Price = 1750m
                        },
                        new
                        {
                            Id = 6,
                            ArrivalDateTime = new DateTime(2023, 5, 17, 18, 0, 0, 0, DateTimeKind.Unspecified),
                            ArrivalStarportId = 4,
                            DepartureDateTime = new DateTime(2023, 5, 16, 14, 0, 0, 0, DateTimeKind.Unspecified),
                            DepartureStarportId = 3,
                            JourneyId = 3,
                            Order = 3,
                            Price = 1750m
                        });
                });

            modelBuilder.Entity("NoGravity.Data.Tables.Planet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Planets");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Location = "Sol System",
                            Name = "Earth"
                        },
                        new
                        {
                            Id = 2,
                            Location = "Sol System",
                            Name = "Mars"
                        },
                        new
                        {
                            Id = 3,
                            Location = "Sol System",
                            Name = "Venus"
                        },
                        new
                        {
                            Id = 4,
                            Location = "Alpha Centauri System",
                            Name = "Proxima Centauri b"
                        },
                        new
                        {
                            Id = 5,
                            Location = "Cygnus Constellation",
                            Name = "Kepler-438b"
                        });
                });

            modelBuilder.Entity("NoGravity.Data.Tables.SeatAllocation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("SeatNumber")
                        .HasColumnType("int");

                    b.Property<int>("SegmentId")
                        .HasColumnType("int");

                    b.Property<bool>("isVacant")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("SegmentId");

                    b.ToTable("SeatAllocations");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            SeatNumber = 17,
                            SegmentId = 1,
                            isVacant = true
                        },
                        new
                        {
                            Id = 2,
                            SeatNumber = 20,
                            SegmentId = 1,
                            isVacant = false
                        },
                        new
                        {
                            Id = 3,
                            SeatNumber = 17,
                            SegmentId = 2,
                            isVacant = true
                        },
                        new
                        {
                            Id = 4,
                            SeatNumber = 4,
                            SegmentId = 2,
                            isVacant = false
                        },
                        new
                        {
                            Id = 5,
                            SeatNumber = 13,
                            SegmentId = 3,
                            isVacant = true
                        },
                        new
                        {
                            Id = 6,
                            SeatNumber = 18,
                            SegmentId = 3,
                            isVacant = true
                        },
                        new
                        {
                            Id = 7,
                            SeatNumber = 18,
                            SegmentId = 5,
                            isVacant = true
                        },
                        new
                        {
                            Id = 8,
                            SeatNumber = 17,
                            SegmentId = 5,
                            isVacant = true
                        },
                        new
                        {
                            Id = 9,
                            SeatNumber = 18,
                            SegmentId = 6,
                            isVacant = true
                        });
                });

            modelBuilder.Entity("NoGravity.Data.Tables.Starcraft", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<int>("CarrierId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CarrierId");

                    b.ToTable("Starcrafts");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Capacity = 600,
                            CarrierId = 1,
                            Description = "First manned mission to the Moon",
                            Name = "Apollo 11"
                        },
                        new
                        {
                            Id = 2,
                            Capacity = 200,
                            CarrierId = 2,
                            Description = "Reusable rocket designed to carry cargo and people into space",
                            Name = "Falcon 9"
                        },
                        new
                        {
                            Id = 3,
                            Capacity = 250,
                            CarrierId = 3,
                            Description = "Suborbital rocket designed for space tourism",
                            Name = "New Shepard"
                        });
                });

            modelBuilder.Entity("NoGravity.Data.Tables.Starport", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PlanetId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PlanetId");

                    b.ToTable("Starports");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Location = "Florida, USA",
                            Name = "Kennedy Space Center",
                            PlanetId = 1
                        },
                        new
                        {
                            Id = 2,
                            Location = "Kazakhstan",
                            Name = "Baikonur Cosmodrome",
                            PlanetId = 1
                        },
                        new
                        {
                            Id = 3,
                            Location = "New Mexico, USA",
                            Name = "Spaceport America",
                            PlanetId = 1
                        },
                        new
                        {
                            Id = 4,
                            Location = "Mars",
                            Name = "Mars Base Alpha",
                            PlanetId = 2
                        },
                        new
                        {
                            Id = 5,
                            Location = "Venus",
                            Name = "Venus Space Station",
                            PlanetId = 3
                        },
                        new
                        {
                            Id = 6,
                            Location = "Mars",
                            Name = "Mars Orbital Gateway",
                            PlanetId = 4
                        });
                });

            modelBuilder.Entity("NoGravity.Data.Tables.Ticket", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("BookingDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("CIF")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EndStarportId")
                        .HasColumnType("int");

                    b.Property<int>("JourneyId")
                        .HasColumnType("int");

                    b.Property<string>("PassengerFirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PassengerSecondName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SeatNumber")
                        .HasColumnType("int");

                    b.Property<int>("StartStarportId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EndStarportId");

                    b.HasIndex("JourneyId");

                    b.HasIndex("StartStarportId");

                    b.HasIndex("UserId");

                    b.ToTable("Tickets");

                    b.HasData(
                        new
                        {
                            Id = new Guid("ffd3d2af-1072-49c4-a70c-10f4db8b47b3"),
                            BookingDateTime = new DateTime(2023, 7, 11, 17, 19, 44, 188, DateTimeKind.Local).AddTicks(7951),
                            CIF = "123456",
                            EndStarportId = 2,
                            JourneyId = 1,
                            PassengerFirstName = "John",
                            PassengerSecondName = "Doe",
                            SeatNumber = 1,
                            StartStarportId = 1,
                            UserId = 1
                        },
                        new
                        {
                            Id = new Guid("189d54c0-eddc-4e04-8a5d-312b91f6ca27"),
                            BookingDateTime = new DateTime(2023, 7, 11, 17, 19, 44, 188, DateTimeKind.Local).AddTicks(7987),
                            CIF = "654321",
                            EndStarportId = 3,
                            JourneyId = 1,
                            PassengerFirstName = "Jane",
                            PassengerSecondName = "Doe",
                            SeatNumber = 2,
                            StartStarportId = 2,
                            UserId = 2
                        });
                });

            modelBuilder.Entity("NoGravity.Data.Tables.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("SecondName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "johndoe@example.com",
                            FirstName = "John",
                            Password = "password123",
                            SecondName = "Doe"
                        },
                        new
                        {
                            Id = 2,
                            Email = "janedoe@example.com",
                            FirstName = "Jane",
                            Password = "password456",
                            SecondName = "Doe"
                        },
                        new
                        {
                            Id = 3,
                            Email = "bobsmith@example.com",
                            FirstName = "Bob",
                            Password = "password789",
                            SecondName = "Smith"
                        },
                        new
                        {
                            Id = 4,
                            Email = "alicejohnson@example.com",
                            FirstName = "Alice",
                            Password = "password101",
                            SecondName = "Johnson"
                        });
                });

            modelBuilder.Entity("NoGravity.Data.Tables.Journey", b =>
                {
                    b.HasOne("NoGravity.Data.Tables.Starcraft", "Starcraft")
                        .WithMany()
                        .HasForeignKey("StarcraftId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Starcraft");
                });

            modelBuilder.Entity("NoGravity.Data.Tables.JourneySegment", b =>
                {
                    b.HasOne("NoGravity.Data.Tables.Starport", "ArrivalStarport")
                        .WithMany()
                        .HasForeignKey("ArrivalStarportId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NoGravity.Data.Tables.Starport", "DepartureStarport")
                        .WithMany()
                        .HasForeignKey("DepartureStarportId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("NoGravity.Data.Tables.Journey", "Journey")
                        .WithMany("JourneySegments")
                        .HasForeignKey("JourneyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ArrivalStarport");

                    b.Navigation("DepartureStarport");

                    b.Navigation("Journey");
                });

            modelBuilder.Entity("NoGravity.Data.Tables.SeatAllocation", b =>
                {
                    b.HasOne("NoGravity.Data.Tables.JourneySegment", "Segment")
                        .WithMany()
                        .HasForeignKey("SegmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Segment");
                });

            modelBuilder.Entity("NoGravity.Data.Tables.Starcraft", b =>
                {
                    b.HasOne("NoGravity.Data.Tables.Carrier", "Carrier")
                        .WithMany()
                        .HasForeignKey("CarrierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Carrier");
                });

            modelBuilder.Entity("NoGravity.Data.Tables.Starport", b =>
                {
                    b.HasOne("NoGravity.Data.Tables.Planet", "Planet")
                        .WithMany()
                        .HasForeignKey("PlanetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Planet");
                });

            modelBuilder.Entity("NoGravity.Data.Tables.Ticket", b =>
                {
                    b.HasOne("NoGravity.Data.Tables.Starport", "EndStarport")
                        .WithMany()
                        .HasForeignKey("EndStarportId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NoGravity.Data.Tables.Journey", "Journey")
                        .WithMany()
                        .HasForeignKey("JourneyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NoGravity.Data.Tables.Starport", "StartStarport")
                        .WithMany()
                        .HasForeignKey("StartStarportId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("NoGravity.Data.Tables.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EndStarport");

                    b.Navigation("Journey");

                    b.Navigation("StartStarport");

                    b.Navigation("User");
                });

            modelBuilder.Entity("NoGravity.Data.Tables.Journey", b =>
                {
                    b.Navigation("JourneySegments");
                });
#pragma warning restore 612, 618
        }
    }
}
