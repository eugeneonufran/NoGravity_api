using NoGravity.Data.Tables;

namespace NoGravity.Data.DataServices
{
    public interface ITicketsDataService
    {
        Task<List<Ticket>> GetTicketsByDeparture(int departureId);
    }
}