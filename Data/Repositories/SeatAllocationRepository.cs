using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NoGravity.Data.DataModel;
using NoGravity.Data.Repositories.Interfaces;
using NoGravity.Data.Tables;

namespace NoGravity.Data.Repositories
{
    public class SeatAllocationRepository : ISeatAllocationRepository
    {
        private readonly NoGravityDbContext _dbContext;

        public SeatAllocationRepository(NoGravityDbContext dbContext)
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

        public async Task DeleteSeatAllocation(SeatAllocation seatAllocation)
        {
            _dbContext.Set<SeatAllocation>().Remove(seatAllocation);
            await _dbContext.SaveChangesAsync();
        }
    }
}
