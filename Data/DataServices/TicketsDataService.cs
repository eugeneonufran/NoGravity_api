using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NoGravity.Data.DataModel;
using NoGravity.Data.DTO;
using NoGravity.Data.DTO.Booking;
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

        public async Task<List<RouteDTO>> GetRoutes(int departureStarportId, int arrivalStarportId, DateTime specifiedDate, SortType sortType = SortType.Optimal)
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
                    filteredPaths = RouteFinder.SortPathsByPrice(filteredPaths);
                    break;
                case SortType.Time:
                    filteredPaths = RouteFinder.SortPathsByTime(filteredPaths);
                    break;
                case SortType.Optimal:
                    filteredPaths = RouteFinder.SortPathsByOptimal(filteredPaths);
                    break;
            }

            var validRouteDTOs = new List<RouteDTO>();

            foreach (var route in filteredPaths)
            {
                var routeSegments = new List<RouteSegmentDTO>();
                DateTime? previousArrivalTime = null;

                bool isRouteValid = true;

                foreach (var segment in route)
                {
                    var seats = await GetAvailableSeatsInSegment(segment.Id);

                    // Check if there are available seats for the segment
                    if (seats.Count() == 0)
                    {
                        isRouteValid = false;
                        break; // Skip the current route if no seats are available
                    }

                    TimeSpan? idleTime = previousArrivalTime.HasValue
                        ? segment.DepartureDateTime - previousArrivalTime.Value
                        : null;

                    var segmentInfo = new RouteSegmentDTO
                    {
                        SegmentId = segment.Id,
                        DepartureId = segment.DepartureStarportId,
                        ArrivalId = segment.ArrivalStarportId,
                        JourneyId = segment.JourneyId,
                        DepartureDateTime = segment.DepartureDateTime,
                        ArrivalDateTime = segment.ArrivalDateTime,
                        Order = segment.Order,
                        Price = segment.Price,
                        TravelTime = segment.ArrivalDateTime - segment.DepartureDateTime,
                        IdleTime = idleTime,
                        SeatsAvailable = seats,
                    };

                    routeSegments.Add(segmentInfo);
                    previousArrivalTime = segment.ArrivalDateTime;
                }

                if (!isRouteValid)
                {
                    continue; // Skip the current route if it is not valid (no available seats)
                }

                var totalPrice = route.Sum(segment => segment.Price);
                var totalTime = route.Last().ArrivalDateTime - route.First().DepartureDateTime;

                var routeDTO = new RouteDTO
                {

                    RouteSegments = routeSegments,
                    TotalPrice = totalPrice,
                    TotalTravelTime = totalTime
                };

                validRouteDTOs.Add(routeDTO);
            }


            return validRouteDTOs;

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
