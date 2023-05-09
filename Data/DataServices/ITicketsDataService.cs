using NoGravity.Data.Tables;
using static NoGravity.Data.NoGravityEnums;

public interface ITicketsDataService
{
    Task<IEnumerable<IEnumerable<JourneySegment>>> GetRoutes(int departureStarportId, int arrivalStarportId, SortType sortType = SortType.Optimal);
}
