using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NoGravity.Data.DataModel;
using NoGravity.Data.Tables;
using System.Collections;

namespace NoGravity.Data.DataServices
{
    public class TicketsDataService : ITicketsDataService
    {
        private readonly NoGravityDbContext _dbContext;

        public TicketsDataService(NoGravityDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Ticket>> GetTicketsByDeparture(int departureId)
        {

            var tickets = await _dbContext.Tickets
                .Where(t => t.StartStarportId == departureId)
                .ToListAsync();

            return tickets;

        }

        public async Task<IEnumerable<JourneySegment>> FindRoute(int departureStarportId, int arrivalStarportId)
        {
                var segments = await _dbContext.JourneySegments.ToListAsync();

            

                return DijkstraService.FindCheapestRouteDijkstra(segments, departureStarportId, arrivalStarportId);

        }

    }
}
