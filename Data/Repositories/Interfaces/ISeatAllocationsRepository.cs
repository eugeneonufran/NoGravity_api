﻿namespace NoGravity.Data.Repositories.Interfaces
{
    public interface ISeatAllocationsRepository
    {
        Task CreateSeatAllocation(SeatAllocation seatAllocation);
        Task DeleteSeatAllocation(SeatAllocation seatAllocation);
        Task<List<SeatAllocation>> GetAllSeatAllocations();
        Task<SeatAllocation> GetSeatAllocationById(int id);
        Task<List<SeatAllocation>> GetSeatAllocationsBySegmentId(int segmentId);
        Task MakeSeatOccupied(int segmentId, int seatNumber);
        Task UpdateSeatAllocation(SeatAllocation seatAllocation);
        Task<SeatAllocationDTO> UpdateSeatAllocation(SeatAllocationDTO seatAllocationDTO);
    }
}