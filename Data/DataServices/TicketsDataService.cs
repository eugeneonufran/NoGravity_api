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

        public async Task<IEnumerable<JourneySegment>> FindRoute(int journeyId,int departureStarportId, int arrivalStarportId)
        {
                var segments = await _dbContext.JourneySegments.Where(js => js.JourneyId == journeyId).ToListAsync();

                var routeSegments = new List<JourneySegment>();
                var visitedSegments = new HashSet<JourneySegment>();

                DFS(segments, departureStarportId, arrivalStarportId, visitedSegments, routeSegments);

                return routeSegments;

        }

        private static bool DFS(List<JourneySegment> segments, int currentStarportId, int arrivalStarportId, HashSet<JourneySegment> visitedSegments, List<JourneySegment> routeSegments)
        {
            if (currentStarportId == arrivalStarportId)
            {
                return true; // Return true if the arrival starport is reached
            }

            foreach (var segment in segments)
            {
                if (!visitedSegments.Contains(segment) && segment.DepartureStarportId == currentStarportId)
                {
                    visitedSegments.Add(segment);
                    routeSegments.Add(segment);

                    if (DFS(segments, segment.ArrivalStarportId, arrivalStarportId, visitedSegments, routeSegments))
                    {
                        return true; // Return true if the route is found
                    }

                    visitedSegments.Remove(segment);
                    routeSegments.Remove(segment);
                }
            }

            return false; // Return false if the route is not found
        }

    }
}
