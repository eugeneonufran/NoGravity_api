
namespace NoGravity.Data.Repositories
{
    public class StarportRepository
    {
        private readonly NoGravityDbContext _dbContext;

        public StarportRepository(NoGravityDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<StarportDTO>> GetAllStarports()
        {
            var starports = await _dbContext.Starports
                .Include(s => s.Planet)
                .Select(s => new StarportDTO
                {
                    Id = s.Id,
                    Name = s.Name,
                    PlanetId = s.PlanetId,
                    PlanetName = s.Planet.Name,
                    Location = s.Location
                })
                .ToListAsync();

            return starports;
        }

        public async Task<StarportDTO> GetStarportById(int starportId)
        {
            var starport = await _dbContext.Starports
                .Include(s => s.Planet)
                .FirstOrDefaultAsync(s => s.Id == starportId);

            if (starport == null)
                return null;

            return new StarportDTO
            {
                Id = starport.Id,
                Name = starport.Name,
                PlanetId = starport.PlanetId,
                PlanetName = starport.Planet.Name,
                Location = starport.Location
            };
        }



        public async Task<StarportDTO> CreateStarport(StarportDTO starportDTO)
        {
            var starport = new Starport
            {
                Name = starportDTO.Name,
                PlanetId = starportDTO.PlanetId,
                Location = starportDTO.Location
            };

            _dbContext.Starports.Add(starport);
            await _dbContext.SaveChangesAsync();

            starportDTO.Id = starport.Id;
            return starportDTO;
        }

        public async Task<StarportDTO> UpdateStarport(int starportId, StarportDTO starportDTO)
        {
            var existingStarport = await _dbContext.Starports.FirstOrDefaultAsync(s => s.Id == starportId);

            if (existingStarport == null)
                return null;

            existingStarport.Name = starportDTO.Name;
            existingStarport.PlanetId = starportDTO.PlanetId;
            existingStarport.Location = starportDTO.Location;

            await _dbContext.SaveChangesAsync();

            return starportDTO;
        }

        public async Task<bool> DeleteStarport(int starportId)
        {
            var starport = await _dbContext.Starports.FirstOrDefaultAsync(s => s.Id == starportId);

            if (starport == null)
                return false;

            _dbContext.Starports.Remove(starport);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }

}
