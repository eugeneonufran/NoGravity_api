namespace NoGravity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly ITicketsDataService _ticketService;
        private readonly NoGravityDbContext _noGravityDbContext;
        private readonly ITicketRepository _ticketRepository;
        private readonly ISeatAllocationRepository _seatAllocationRepository;

        public BookingController(ITicketRepository ticketRepository, ITicketsDataService ticketService, NoGravityDbContext noGravityDbContext, ISeatAllocationRepository seatAllocationRepository)
        {
            _ticketService = ticketService;
            _noGravityDbContext = noGravityDbContext;
            _ticketRepository = ticketRepository;
            _seatAllocationRepository = seatAllocationRepository;
        }


        [HttpGet("booking")]
        public async Task<IActionResult> FindAvailableRoutes(int departureStarportId, int arrivalStarportId, DateTime date, SortType sortType = SortType.Optimal)
        {
            var routeDTOs = await _ticketService.GetPossibleRoutes(departureStarportId, arrivalStarportId, date, sortType);

            return Ok(routeDTOs);

        }

        [HttpPost("booking/seats")]
        public async Task<IActionResult> GetSeatMapForRoute([FromBody] List<RouteDTO> routesDTO, int routeId)
        {
            var startSegment = routesDTO[routeId].RouteSegments.FirstOrDefault();

            List<SeatDTO> seats = new List<SeatDTO>();

            var allSeats = await _ticketService.GetAllSeatsInSegment(startSegment.SegmentId);
            var availableSeats = await _ticketService.GetAvailableSeatsInSegment(startSegment.SegmentId);

            foreach (var seat in allSeats)
            {
                var seatDto = new SeatDTO
                {
                    Id = seat.Id,
                    SegmentId = seat.SegmentId,
                    SeatNumber = seat.SeatNumber,
                    IsVacant = availableSeats.Contains(seat)?true:false,

                };

                seats.Add(seatDto);
            }


            return Ok(seats);

        }




        [HttpPost("booking/order")]
        public async Task<IActionResult> OrderRoute([FromBody] RouteDTO routeDTO, int seatNumber,string firstName, string lastName, string cif, int userId, bool actuallyCreateTicket)
        {
            if (!actuallyCreateTicket)
            {
                var orderConfirmation = "Your route has been ordered successfully.";
                return Ok(orderConfirmation);
            }

            var tickets = new List<Ticket>();

            var routeSegments = routeDTO.RouteSegments;

            var journeys = routeSegments.Select(segment => segment.JourneyId).Distinct().ToList();

            var startStarport = routeSegments.First().DepartureId;

            var endStarport = routeSegments.Last().ArrivalId;


            for (int i = 0; i < journeys.Count; i++)
            {
                var ticket = await _ticketRepository.CreateTicket(journeys[i],startStarport,endStarport, firstName, lastName, cif, userId, seatNumber);

                tickets.Add(ticket);
            }

            foreach (var segment in routeSegments) {
                await _seatAllocationRepository.MakeSeatOccupied(segment.SegmentId, seatNumber);
            }




            return Ok(tickets);
            
        }


    }
}



