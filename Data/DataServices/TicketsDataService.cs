using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NoGravity.Data.DataModel;
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

        public async Task<IEnumerable<IEnumerable<JourneySegment>>> GetRoutes(int departureStarportId, int arrivalStarportId, SortType sortType=SortType.Optimal)
        {
            var routes = await _dbContext.JourneySegments.ToListAsync();

            var paths = RouteFinder.FindAllPaths(routes, departureStarportId, arrivalStarportId);

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

    }
}
