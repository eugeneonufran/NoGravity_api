using NoGravity.Data.Tables;

public interface ITicketsDataService
{
    Task<IEnumerable<IEnumerable<JourneySegment>>> GetRoutes(int departureStarportId, int arrivalStarportId);
    Task<IEnumerable<IEnumerable<JourneySegment>>> GetRoutesSortedByPrice(int departureStarportId, int arrivalStarportId);
    Task<IEnumerable<IEnumerable<JourneySegment>>> GetRoutesSortedByTime(int departureStarportId, int arrivalStarportId);
    Task<IEnumerable<IEnumerable<JourneySegment>>> GetRoutesSortedByOptimal(int departureStarportId, int arrivalStarportId);
}
