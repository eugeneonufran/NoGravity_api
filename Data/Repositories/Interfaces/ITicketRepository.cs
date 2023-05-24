namespace NoGravity.Data.Repositories.Interfaces
{
    public interface ITicketRepository
    {
        Task Create(Ticket ticket);
        Task Delete(Guid id);
        Task<IEnumerable<TicketDTO>> GetAll();
        Task<Ticket> GetById(Guid id);
        Task Update(Ticket ticket);

        Task<Ticket> CreateTicket(int journeyId, int startStarportId, int endStarportId, string passengerFirstName, string passengerSecondName, string cif, int userId, int seatId);
    }
}