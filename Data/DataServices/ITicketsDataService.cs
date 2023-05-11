using NoGravity.Data.Tables;
using static NoGravity.Data.NoGravityEnums;

public interface ITicketsDataService
{
    Task<IEnumerable<IEnumerable<JourneySegment>>> GetRoutes(int departureStarportId, int arrivalStarportId, DateTime specifiedDate, SortType sortType = SortType.Optimal);
    Task<IEnumerable<int>> GetAvailableSeatsInSegment(int segmentId);
    Task<IEnumerable<int>> GetAllSeatsInSegment(int segmentId);

}
