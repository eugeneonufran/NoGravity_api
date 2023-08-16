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
                .Include(t => t.Journey)
                .Include(t => t.StartStarport)
                .Include(t => t.EndStarport)
                .Include(t => t.User)
                .Select(t => new TicketDTO
                {
                    Id = t.Id,
                    JourneyId = t.JourneyId,
                    JourneyName = t.Journey.Name,
                    StartStarportId = t.StartStarportId,
                    StartStarportName = t.StartStarport.Name,
                    EndStarportId = t.EndStarportId,
                    EndStarportName = t.EndStarport.Name,
                    PassengerFirstName = t.PassengerFirstName,
                    PassengerSecondName = t.PassengerSecondName,
                    CIF = t.CIF,
                    UserId = t.UserId,
                    UserEmail = t.User.Email,
                    SeatNumber = t.SeatNumber,
                    File_Path = t.File_Path

                })
                .ToListAsync();
        }

        public async Task Create(Ticket ticket)
        {
            await _dbContext.Tickets.AddAsync(ticket);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Ticket> Update(Ticket ticket)
        {
            var existingTicket = await _dbContext.Tickets.FirstOrDefaultAsync(t => t.Id == ticket.Id);

            if (existingTicket == null)
            {
                return null; // Ticket not found, handle this in the calling code
            }

            // Update the properties of the existingTicket with the values from the passed ticket object
            existingTicket.JourneyId = ticket.JourneyId;
            existingTicket.StartStarportId = ticket.StartStarportId;
            existingTicket.EndStarportId = ticket.EndStarportId;
            existingTicket.PassengerFirstName = ticket.PassengerFirstName;
            existingTicket.PassengerSecondName = ticket.PassengerSecondName;
            existingTicket.CIF = ticket.CIF;
            existingTicket.File_Path = ticket.File_Path;
            // Update other properties as needed

            await _dbContext.SaveChangesAsync();

            return existingTicket;

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
                UserId = userId,
                SeatNumber = seatNumber,
                BookingDateTime = DateTime.Now,

            };


            await _dbContext.Tickets.AddAsync(ticket);
            await _dbContext.SaveChangesAsync();

            return ticket;
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
