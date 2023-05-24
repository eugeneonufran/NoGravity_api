using NoGravity.Data.DTO;
using NoGravity.Data.DTO.JourneySegments;
using NoGravity.Data.Tables;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NoGravity.Data.Repositories
{
    public class JourneySegmentsRepository : IJourneySegmentsRepository
    {
        private readonly NoGravityDbContext _dbContext;

        public JourneySegmentsRepository(NoGravityDbContext context)
        {
            _dbContext = context;
        }

        public async Task<IEnumerable<JourneySegmentDTO>> GetAllJourneySegments()
        {
            var journeySegments = await _dbContext.JourneySegments.ToListAsync();
            return journeySegments.Select(ConvertToDTO);
        }

        public async Task<JourneySegmentDTO> GetJourneySegmentById(int journeySegmentId)
        {
            var journeySegment = await _dbContext.JourneySegments.FindAsync(journeySegmentId);
            return ConvertToDTO(journeySegment);
        }

        public async Task<JourneySegmentDTO> CreateJourneySegment(JourneySegmentDTO journeySegmentDTO)
        {
            var journeySegment = ConvertToEntity(journeySegmentDTO);
            _dbContext.JourneySegments.Add(journeySegment);
            await _dbContext.SaveChangesAsync();
            return ConvertToDTO(journeySegment);
        }

        public async Task<JourneySegmentDTO> UpdateJourneySegment(int journeySegmentId, JourneySegmentDTO journeySegmentDTO)
        {
            var existingJourneySegment = await _dbContext.JourneySegments.FindAsync(journeySegmentId);
            if (existingJourneySegment == null)
                return null;

            // Update the properties of the existingJourneySegment entity
            existingJourneySegment.DepartureStarportId = journeySegmentDTO.DepartureStarportId;
            existingJourneySegment.ArrivalStarportId = journeySegmentDTO.ArrivalStarportId;
            existingJourneySegment.Order = journeySegmentDTO.Order;
            existingJourneySegment.DepartureDateTime = journeySegmentDTO.DepartureDateTime;
            existingJourneySegment.Price = journeySegmentDTO.Price;
            existingJourneySegment.ArrivalDateTime = journeySegmentDTO.ArrivalDateTime;

            await _dbContext.SaveChangesAsync();
            return ConvertToDTO(existingJourneySegment);
        }

        public async Task<bool> DeleteJourneySegment(int journeySegmentId)
        {
            var journeySegment = await _dbContext.JourneySegments.FindAsync(journeySegmentId);
            if (journeySegment == null)
                return false;

            _dbContext.JourneySegments.Remove(journeySegment);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        private JourneySegmentDTO ConvertToDTO(JourneySegment journeySegment)
        {
            return new JourneySegmentDTO
            {
                Id = journeySegment.Id,
                DepartureStarportId = journeySegment.DepartureStarportId,
                ArrivalStarportId = journeySegment.ArrivalStarportId,
                Order = journeySegment.Order,
                DepartureDateTime = journeySegment.DepartureDateTime,
                Price = journeySegment.Price,
                ArrivalDateTime = journeySegment.ArrivalDateTime
            };
        }

        private JourneySegment ConvertToEntity(JourneySegmentDTO journeySegmentDTO)
        {
            return new JourneySegment
            {
                Id = journeySegmentDTO.Id,
                DepartureStarportId = journeySegmentDTO.DepartureStarportId,
                ArrivalStarportId = journeySegmentDTO.ArrivalStarportId,
                Order = journeySegmentDTO.Order,
                DepartureDateTime = journeySegmentDTO.DepartureDateTime,
                Price = journeySegmentDTO.Price,
                ArrivalDateTime = journeySegmentDTO.ArrivalDateTime
            };
        }
    }
}
