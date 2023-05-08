using NoGravity.Data.Tables;

namespace NoGravity.Data.DataServices
{
    public interface ITicketsDataService
    {
        Task<IEnumerable<JourneySegment>> FindRoute( int departureStarportId, int arrivalStarportId);
        Task<List<Ticket>> GetTicketsByDeparture(int departureId);
    }
}