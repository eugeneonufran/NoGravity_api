using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NoGravity.Data.DataModel;
using NoGravity.Data.DTO;
using NoGravity.Data.Tables;
using System.Collections;
using static NoGravity.Data.NoGravityEnums;

namespace NoGravity.Data.DataServices
{
    public class TicketsDataService : ITicketsDataService
    {
        private readonly NoGravityDbContext _dbContext;

        public TicketsDataService(NoGravityDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<IEnumerable<JourneySegment>>> GetRoutes(int departureStarportId, int arrivalStarportId, SortType sortType = SortType.Optimal, DateTime? specifiedDate = null)
        {
            IQueryable<JourneySegment> routes = _dbContext.JourneySegments;

            if (specifiedDate.HasValue)
            {
                routes = routes.Where(segment => segment.DepartureDateTime.Date == specifiedDate.Value.Date);
            }

            var paths = RouteFinder.FindAllPaths(await routes.ToListAsync(), departureStarportId, arrivalStarportId);

            switch (sortType)
            {
                case SortType.Price:
                    return RouteFinder.SortPathsByPrice(paths);
                case SortType.Time:
                    return RouteFinder.SortPathsByTime(paths);
                case SortType.Optimal:
                    return RouteFinder.SortPathsByOptimal(paths);
                default:
                    return paths; // No sorting, return original paths
            }
        }


        /*
        public async Task<Ticket> CreateTicket(RouteDTO route,string firstName, string lastName, string cif)
        {

        }
        */



    }
}
