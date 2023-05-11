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

            var validRouteDTOs = new List<RouteDTO>();

            foreach (var route in routes)
            {
                var routeSegments = new List<RouteSegmentDTO>();
                DateTime? previousArrivalTime = null;

                bool isRouteValid = true;

                foreach (var segment in route)
                {
                    var seats = await _ticketService.GetAvailableSeatsInSegment(segment.Id);

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
                        SeatsAvailable = seats.Count(),
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

            return Ok(new
            {
                Routes = validRouteDTOs
            });
        }



        [HttpPost("booking/seats")]
        public async Task<IActionResult> GetSeatMapForRoute([FromBody] RouteDTO routeDTO)
        {
            // Validate the input routeDTO and perform any necessary checks

            var segmentSeatMaps = new List<SegmentSeatMapDTO>();

            // Iterate over each route segment
            foreach (var segment in routeDTO.RouteSegments)
            {
                // Retrieve the available seats for the segment
                var availableSeats = await _ticketService.GetAvailableSeatsInSegment(segment.SegmentId);

                var seatDTOList = new List<SeatDTO>();

                // Check if the current segment is the last segment in the route
                var isLastSegment = segment == routeDTO.RouteSegments.Last();

                foreach (var availableSeat in availableSeats)
                {
                    // Determine the seat color based on availability for all further segments
                    var seatColor = GetSeatColor(availableSeat, segment, routeDTO.RouteSegments, isLastSegment);
                    var seatDTO = new SeatDTO(availableSeat, seatColor);
                    seatDTOList.Add(seatDTO);
                }

                segmentSeatMaps.Add(new SegmentSeatMapDTO(segment.SegmentId, seatDTOList));
            }

            var routeSeatMapDTO = new RouteSeatMapDTO(segmentSeatMaps);

            return Ok(routeSeatMapDTO);
        }

        private SeatColor GetSeatColor(int seatNumber, RouteSegmentDTO segment, List<RouteSegmentDTO> routeSegments, bool isLastSegment)
        {
            if (isLastSegment)
            {
                // Last segment, all seats are available for the entire route
                return SeatColor.Green;
            }
            else
            {
                // Check if the seat is available in all further segments
                var isAvailableForAllSegments = true;
                var startIndex = routeSegments.IndexOf(segment) + 1;

                for (int i = startIndex; i < routeSegments.Count; i++)
                {
                    var nextSegment = routeSegments[i];
                    var availableSeats = _ticketService.GetAvailableSeatsInSegment(nextSegment.SegmentId).Result;

                    if (!availableSeats.Contains(seatNumber))
                    {
                        isAvailableForAllSegments = false;
                        break;
                    }
                }

                return isAvailableForAllSegments ? SeatColor.Green : SeatColor.Yellow;
            }
        }








        [HttpPost("booking/order")]
        public IActionResult OrderRoute([FromBody] RouteDTO routeDTO, string firstName)
        {
            var orderConfirmation = "Your route has been ordered successfully.";
            return Ok(orderConfirmation);
        }


    }
}



