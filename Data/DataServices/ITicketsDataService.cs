using NoGravity.Data.DTO;
using NoGravity.Data.Tables;
using static NoGravity.Data.NoGravityEnums;

public interface ITicketsDataService
{
    Task<List<RouteDTO>> GetPossibleRoutes(int departureStarportId, int arrivalStarportId, DateTime specifiedDate, SortType sortType = SortType.Optimal);
    Task<IEnumerable<SeatAllocation>> GetAvailableSeatsInSegment(int segmentId);
    Task<IEnumerable<SeatAllocation>> GetAllSeatsInSegment(int segmentId);

}
