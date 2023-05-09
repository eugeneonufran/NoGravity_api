using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NoGravity.Data.DataModel;
using NoGravity.Data.DataServices;
using NoGravity.Data.DTO;
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

            var routeDTOs = new List<RouteDTO>();

            foreach (var route in routes)
            {
                var routeSegments = new List<RouteSegmentDTO>();
                DateTime? previousArrivalTime = null;

                foreach (var segment in route)
                {
                    TimeSpan? idleTime = previousArrivalTime.HasValue
                        ? segment.DepartureDateTime - previousArrivalTime.Value
                        : (TimeSpan?)null;

                    var segmentInfo = new RouteSegmentDTO
                    {
                        SegmentId = segment.Id,
                        JourneyId = segment.JourneyId,
                        DepartureDateTime = segment.DepartureDateTime,
                        ArrivalDateTime = segment.ArrivalDateTime,
                        Order = segment.Order,
                        Price = segment.Price,
                        TravelTime = segment.ArrivalDateTime - segment.DepartureDateTime,
                        IdleTime = idleTime
                    };

                    routeSegments.Add(segmentInfo);
                    previousArrivalTime = segment.ArrivalDateTime;
                }

                var totalPrice = route.Sum(segment => segment.Price);
                var totalTime = route.Last().ArrivalDateTime - route.First().DepartureDateTime;

                var routeDTO = new RouteDTO
                {
                    RouteSegments = routeSegments,
                    TotalPrice = totalPrice,
                    TotalTravelTime = totalTime
                };

                routeDTOs.Add(routeDTO);
            }

            return Ok(new
            {
                Routes = routeDTOs,
            });
        }

        [HttpPost("booking/order")]
        public IActionResult OrderRoute([FromBody] RouteDTO routeDTO, string name)
        {
            // Validate the routeDTO and perform any necessary checks

            // Process the order and perform any required operations

            // Return the order confirmation or relevant response
            var orderConfirmation = "Your route has been ordered successfully.";
            return Ok(orderConfirmation);
        }

    }
}



