using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NoGravity.Data.DataModel;
using NoGravity.Data.DataServices;
using static NoGravity.Data.NoGravityEnums;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NoGravity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly ITicketsDataService _ticketService;
        private readonly NoGravityDbContext _noGravityDbContext;

        public TicketsController(ITicketsDataService ticketService, NoGravityDbContext noGravityDbContext)
        {
            _ticketService = ticketService;
            _noGravityDbContext = noGravityDbContext;
        }


        [HttpGet("booking")]
        public async Task<IActionResult> FindAvailableRoutes(int departureStarportId, int arrivalStarportId, SortType sortType = SortType.Optimal)
        {
            var routes = await _ticketService.GetRoutes(departureStarportId, arrivalStarportId, sortType);

            var routesInfo = routes.Select(route =>
            {
                var routeSegments = new List<object>();
                DateTime? previousArrivalTime = null;

                foreach (var segment in route)
                {
                    TimeSpan? idleTime = previousArrivalTime.HasValue
                        ? segment.DepartureDateTime - previousArrivalTime.Value
                        : (TimeSpan?)null;

                    var segmentInfo = new
                    {
                        segmentID = segment.Id,
                        journeyID = segment.JourneyId,
                        departureStarportId = segment.DepartureStarportId,
                        arrivalStarportId = segment.ArrivalStarportId,
                        departureDateTime = segment.DepartureDateTime,
                        arrivalDateTime = segment.ArrivalDateTime,
                        order = segment.Order,
                        price = segment.Price,
                        travelTime = segment.ArrivalDateTime - segment.DepartureDateTime,
                        idleTime = idleTime
                    };

                    routeSegments.Add(segmentInfo);
                    previousArrivalTime = segment.ArrivalDateTime;
                }

                var totalPrice = route.Sum(segment => segment.Price);
                var totalTime = route.Last().ArrivalDateTime - route.First().DepartureDateTime;


                var routeInfo = new
                {
                    RouteSegments = routeSegments,
                    TotalPrice = totalPrice,
                    TotalTravelTime = totalTime
                };

                return routeInfo;
            });

            return Ok(new
            {
                Routes = routesInfo,
                OptionsCount = routesInfo.Count()
            });

        }

        /*

        [HttpGet("booking/routes/{routeId}")]
        public async Task<IActionResult> GetRouteDetails(int routeId)
        {
            // Retrieve the detailed information of the selected route using the routeId
            var routeDetails = await _ticketService.GetRouteDetails(routeId);

            if (routeDetails == null)
            {
                return NotFound(); // Route not found
            }

            return Ok(routeDetails);
        }

        */

    }
}



