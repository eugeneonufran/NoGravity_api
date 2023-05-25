namespace NoGravity.Data.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly NoGravityDbContext _dbContext;

        public TicketRepository(NoGravityDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Ticket> GetById(Guid id)
        {
            return await _dbContext.Tickets.FindAsync(id);
        }


        public async Task<IEnumerable<TicketDTO>> GetAll()
        {
            return await _dbContext.Tickets
                .Include(t=>t.Journey)
                .Include(t=>t.StartStarport)
                .Include(t => t.EndStarport)
                .Include(t=>t.User)
                .Select(t => new TicketDTO
                {
                    JourneyId = t.JourneyId,
                    JourneyName= t.Journey.Name,
                    StartStarportId = t.StartStarportId,
                    StartStarportName=t.StartStarport.Name,
                    EndStarportId = t.EndStarportId,
                    EndStarportName = t.EndStarport.Name,
                    PassengerFirstName = t.PassengerFirstName,
                    PassengerSecondName = t.PassengerSecondName,
                    CIF = t.CIF,
                    UserId = t.UserId,
                    UserEmail=t.User.Email,
                    SeatNumber = t.SeatNumber
                })
                .ToListAsync();
        }

        public async Task Create(Ticket ticket)
        {
            await _dbContext.Tickets.AddAsync(ticket);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Ticket> CreateTicket(int journeyId, int startStarportId, int endStarportId, string passengerFirstName, string passengerSecondName, string cif, int userId, int seatNumber)
        {
            var ticket = new Ticket
            {
                Id = new Guid(),
                JourneyId = journeyId,
                StartStarportId = startStarportId,
                EndStarportId = endStarportId,
                PassengerFirstName = passengerFirstName,
                PassengerSecondName = passengerSecondName,
                CIF = cif,
                UserId=userId,
                SeatNumber = seatNumber,
                BookingDateTime=DateTime.Now,

            };
            

            await _dbContext.Tickets.AddAsync(ticket);
            await _dbContext.SaveChangesAsync();

            return ticket;
        }

        public async Task Update(Ticket ticket)
        {
            _dbContext.Tickets.Update(ticket);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var ticket = await GetById(id);
            if (ticket != null)
            {
                _dbContext.Tickets.Remove(ticket);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
