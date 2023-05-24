using NoGravity.Data.Tables;
using NoGravity.Data.DTO;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NoGravity.Data.DTO.Journey;

namespace NoGravity.Data.Repositories
{
    public class JourneysRepository : IJourneysRepository
    {
        private readonly NoGravityDbContext _dbContext;

        public JourneysRepository(NoGravityDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<JourneyDTO>> GetAllJourneys()
        {
            var journeys = await _dbContext.Journeys
                .Select(j => new JourneyDTO
                {
                    Id = j.Id,
                    Number = j.Number,
                    Name = j.Name,
                    StarcraftId = j.StarcraftId
                })
                .ToListAsync();

            return journeys;
        }

        public async Task<JourneyDTO> GetJourneyById(int journeyId)
        {
            var journey = await _dbContext.Journeys
                .Where(j => j.Id == journeyId)
                .Select(j => new JourneyDTO
                {
                    Id = j.Id,
                    Number = j.Number,
                    Name = j.Name,
                    StarcraftId = j.StarcraftId
                })
                .FirstOrDefaultAsync();

            return journey;
        }

        public async Task<JourneyDTO> CreateJourney(JourneyDTO journeyDTO)
        {
            var journey = new Journey
            {
                Number = journeyDTO.Number,
                Name = journeyDTO.Name,
                StarcraftId = journeyDTO.StarcraftId
            };

            _dbContext.Journeys.Add(journey);
            await _dbContext.SaveChangesAsync();

            journeyDTO.Id = journey.Id;
            return journeyDTO;
        }

        public async Task<JourneyDTO> UpdateJourney(int journeyId, JourneyDTO journeyDTO)
        {
            var existingJourney = await _dbContext.Journeys.FirstOrDefaultAsync(j => j.Id == journeyId);

            if (existingJourney == null)
                return null;

            existingJourney.Number = journeyDTO.Number;
            existingJourney.Name = journeyDTO.Name;
            existingJourney.StarcraftId = journeyDTO.StarcraftId;

            await _dbContext.SaveChangesAsync();

            return journeyDTO;
        }

        public async Task<bool> DeleteJourney(int journeyId)
        {
            var journey = await _dbContext.Journeys.FirstOrDefaultAsync(j => j.Id == journeyId);

            if (journey == null)
                return false;

            _dbContext.Journeys.Remove(journey);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
