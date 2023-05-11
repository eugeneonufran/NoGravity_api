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

        public async Task<IEnumerable<IEnumerable<JourneySegment>>> GetRoutes(int departureStarportId, int arrivalStarportId, DateTime specifiedDate, SortType sortType = SortType.Optimal)
        {
            IQueryable<JourneySegment> routes = _dbContext.JourneySegments;


            var paths = RouteFinder.FindAllPaths(await routes.ToListAsync(), departureStarportId, arrivalStarportId, specifiedDate);

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





        public async Task<IEnumerable<int>> GetAvailableSeatsInSegment(int segmentId)
        {
            var vacantSeats = await _dbContext.SeatAllocations
                .Where(sa => sa.SegmentId == segmentId && sa.isVacant)
                .Select(sa => sa.SeatNumber)
                .ToListAsync();

            return vacantSeats;
        }



    }
}
