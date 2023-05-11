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


            var allPaths = RouteFinder.FindAllPaths(await routes.ToListAsync(), departureStarportId, arrivalStarportId, specifiedDate);

            var filteredPaths = new List<List<JourneySegment>>();

            foreach (var path in allPaths)
            {
                if (GetAvailableSeats(path).Count!=0)
                {
                    filteredPaths.Add(path);
                }
            }



            switch (sortType)
            {
                case SortType.Price:
                    return RouteFinder.SortPathsByPrice(filteredPaths);
                case SortType.Time:
                    return RouteFinder.SortPathsByTime(filteredPaths);
                case SortType.Optimal:
                    return RouteFinder.SortPathsByOptimal(filteredPaths);
                default:
                    return filteredPaths; // No sorting, return original paths
            }
        }

        private List<int> GetAvailableSeats(List<JourneySegment> path)
        {
            var commonSeats = new List<int>();

            foreach (var segment in path)
            {
                var seats = GetAvailableSeatsInSegment(segment.Id).GetAwaiter().GetResult();

                if (commonSeats.Count == 0)
                {
                    commonSeats.AddRange(seats);
                }
                else
                {
                    commonSeats = commonSeats.Intersect(seats).ToList();
                }

            }

            return commonSeats;
        }





        public async Task<IEnumerable<int>> GetAvailableSeatsInSegment(int segmentId)
        {
            var vacantSeats = await _dbContext.SeatAllocations
                .Where(sa => sa.SegmentId == segmentId && sa.isVacant)
                .Select(sa => sa.SeatNumber)
                .ToListAsync();

            return vacantSeats;
        }

        public async Task<IEnumerable<int>> GetAllSeatsInSegment(int segmentId)
        {
            var seats = await _dbContext.SeatAllocations
                .Where(sa => sa.SegmentId == segmentId)
                .Select(sa => sa.SeatNumber)
                .ToListAsync();

            return seats;
        }




    }
}
