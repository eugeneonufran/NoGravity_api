using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NoGravity.Data.DTO;
using NoGravity.Data.DTO.Planets;
using NoGravity.Data.Tables;

namespace NoGravity.Data.Repositories
{
    public class PlanetsRepository : IPlanetsRepository
    {
        private readonly NoGravityDbContext _dbContext;

        public PlanetsRepository(NoGravityDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<PlanetDTO>> GetAllPlanets()
        {
            var planets = await _dbContext.Planets
                .Select(p => new PlanetDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    Location = p.Location
                })
                .ToListAsync();

            return planets;
        }

        public async Task<PlanetDTO> GetPlanetById(int planetId)
        {
            var planet = await _dbContext.Planets
                .Where(p => p.Id == planetId)
                .Select(p => new PlanetDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    Location = p.Location
                })
                .FirstOrDefaultAsync();

            return planet;
        }

        public async Task<PlanetDTO> CreatePlanet(PlanetDTO planetDTO)
        {
            var planet = new Planet
            {
                Name = planetDTO.Name,
                Location = planetDTO.Location
            };

            _dbContext.Planets.Add(planet);
            await _dbContext.SaveChangesAsync();

            planetDTO.Id = planet.Id;
            return planetDTO;
        }

        public async Task<PlanetDTO> UpdatePlanet(int planetId, PlanetDTO planetDTO)
        {
            var existingPlanet = await _dbContext.Planets.FindAsync(planetId);

            if (existingPlanet == null)
                return null;

            existingPlanet.Name = planetDTO.Name;
            existingPlanet.Location = planetDTO.Location;

            await _dbContext.SaveChangesAsync();

            return planetDTO;
        }

        public async Task<bool> DeletePlanet(int planetId)
        {
            var planet = await _dbContext.Planets.FindAsync(planetId);

            if (planet == null)
                return false;

            _dbContext.Planets.Remove(planet);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
