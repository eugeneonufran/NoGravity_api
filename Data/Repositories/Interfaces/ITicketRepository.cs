using NoGravity.Data.Tables;

namespace NoGravity.Data.Repositories.Interfaces
{
    public interface ITicketRepository
    {
        Task Create(Ticket ticket);
        Task Delete(Guid id);
        Task<IEnumerable<Ticket>> GetAll();
        Task<Ticket> GetById(Guid id);
        Task Update(Ticket ticket);
    }
}