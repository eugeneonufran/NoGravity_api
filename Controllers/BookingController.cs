using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NoGravity.Data.DataModel;
using NoGravity.Data.DataServices;
using NoGravity.Data.DTO;
using NoGravity.Data.DTO.Booking;
using NoGravity.Data.Repositories.Interfaces;
using static NoGravity.Data.NoGravityEnums;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NoGravity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly ITicketsDataService _ticketService;
        private readonly NoGravityDbContext _noGravityDbContext;
        private readonly ITicketRepository _ticketRepository;

        public BookingController(ITicketRepository ticketRepository, ITicketsDataService ticketService, NoGravityDbContext noGravityDbContext)
        {
            _ticketService = ticketService;
            _noGravityDbContext = noGravityDbContext;
            _ticketRepository = ticketRepository;
        }


        [HttpGet("booking")]
        public async Task<IActionResult> FindAvailableRoutes(int departureStarportId, int arrivalStarportId, DateTime date, SortType sortType = SortType.Optimal)
        {
            var routes = await _ticketService.GetRoutes(departureStarportId, arrivalStarportId, date, sortType);

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
        public IActionResult OrderRoute([FromBody] RouteDTO routeDTO, string firstName)
        {
            var orderConfirmation = "Your route has been ordered successfully.";
            return Ok(orderConfirmation);
        }

        


    }
}



