namespace NoGravity.Data.DataModel
{
    public class NoGravityDbContext : DbContext
    {
        public NoGravityDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Planet> Planets { get; set; }
        public DbSet<Carrier> Carriers { get; set; }
        public DbSet<Starcraft> Starcrafts { get; set; }
        public DbSet<Starport> Starports { get; set; }
        public DbSet<Journey> Journeys { get; set; }
        public DbSet<JourneySegment> JourneySegments { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<SeatAllocation> SeatAllocations { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            DataModelInitializer.ConfigureModel(modelBuilder);
            DataModelInitializer.SeedData(modelBuilder);
        }

    }
}
