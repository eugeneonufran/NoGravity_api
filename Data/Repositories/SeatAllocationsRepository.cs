using NoGravity.Data.DTO.Planets;
using NoGravity.Data.Tables;

namespace NoGravity.Data.Repositories
{
    public class SeatAllocationsRepository : ISeatAllocationsRepository
    {
        private readonly NoGravityDbContext _dbContext;

        public SeatAllocationsRepository(NoGravityDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task MakeSeatOccupied(int segmentId, int seatNumber)
        {
            var seat = await _dbContext.Set<SeatAllocation>().Where(s => s.SegmentId == segmentId && s.SeatNumber == seatNumber).FirstAsync();

            seat.isVacant = false;

            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<SeatAllocation>> GetAllSeatAllocations()
        {
            return await _dbContext.Set<SeatAllocation>().ToListAsync();
        }

        public async Task<SeatAllocation> GetSeatAllocationById(int id)
        {
            return await _dbContext.Set<SeatAllocation>().FindAsync(id);
        }

        public async Task<List<SeatAllocation>> GetSeatAllocationsBySegmentId(int segmentId)
        {
            return await _dbContext.Set<SeatAllocation>()
                .Where(sa => sa.SegmentId == segmentId)
                .ToListAsync();
        }

        public async Task CreateSeatAllocation(SeatAllocation seatAllocation)
        {
            _dbContext.Set<SeatAllocation>().Add(seatAllocation);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateSeatAllocation(SeatAllocation seatAllocation)
        {
            _dbContext.Set<SeatAllocation>().Update(seatAllocation);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<SeatAllocationDTO> UpdateSeatAllocation(SeatAllocationDTO seatAllocationDTO)
        {
            var existingSeat = await _dbContext.SeatAllocations.FindAsync(seatAllocationDTO.Id);

            if (existingSeat == null)
            {
                return null;
            }

            existingSeat.SeatNumber = seatAllocationDTO.SeatNumber;
            existingSeat.isVacant = seatAllocationDTO.IsVacant;
            existingSeat.SegmentId = seatAllocationDTO.SegmentId;

            await _dbContext.SaveChangesAsync();

            // Convert the updated entity back to DTO
            var updatedSeatAllocationDTO = new SeatAllocationDTO
            {
                Id = existingSeat.Id,
                SeatNumber = existingSeat.SeatNumber,
                IsVacant = existingSeat.isVacant,
                SegmentId = existingSeat.SegmentId
            };

            return updatedSeatAllocationDTO;
        }


        public async Task DeleteSeatAllocation(SeatAllocation seatAllocation)
        {
            _dbContext.Set<SeatAllocation>().Remove(seatAllocation);
            await _dbContext.SaveChangesAsync();
        }
    }
}
