using NoGravity.Data.DTO.SeatAllocations;

namespace NoGravity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly ITicketsDataService _ticketService;
        private readonly NoGravityDbContext _noGravityDbContext;
        private readonly ITicketRepository _ticketRepository;
        private readonly ISeatAllocationsRepository _seatAllocationRepository;

        public BookingController(ITicketRepository ticketRepository, ITicketsDataService ticketService, NoGravityDbContext noGravityDbContext, ISeatAllocationsRepository seatAllocationRepository)
        {
            _ticketService = ticketService;
            _noGravityDbContext = noGravityDbContext;
            _ticketRepository = ticketRepository;
            _seatAllocationRepository = seatAllocationRepository;
        }


        [HttpGet("findroutes")]
        public async Task<IActionResult> FindAvailableRoutes(int departureStarportId, int arrivalStarportId, DateTime date, SortType sortType = SortType.Optimal)
        {
            var routeDTOs = await _ticketService.GetPossibleRoutes(departureStarportId, arrivalStarportId, date, sortType);

            return Ok(routeDTOs);

        }

        [HttpPost("seats")]
        public async Task<IActionResult> GetSeatMapForRoute([FromBody] RouteDTO route)
        {
            var startSegment = route.RouteSegments.FirstOrDefault();

            List<SeatAllocationDTO> seats = new List<SeatAllocationDTO>();

            var allSeats = await _ticketService.GetAllSeatsInSegment(startSegment.SegmentId);
            var availableSeats = await _ticketService.GetAvailableSeatsInSegment(startSegment.SegmentId);

            foreach (var seat in allSeats)
            {
                var seatDto = new SeatAllocationDTO
                {
                    Id = seat.Id,
                    segmentId = seat.SegmentId,
                    seatNumber = seat.SeatNumber,
                    isVacant = availableSeats.Contains(seat) ? true : false,

                };

                seats.Add(seatDto);
            }


            return Ok(seats);

        }




        [HttpPost("order")]
        public async Task<IActionResult> OrderRoute([FromBody] RouteDTO routeDTO, int seatNumber, string firstName, string lastName, string cif, int userId, bool actuallyCreateTicket)
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

            var ticketsPaths = new List<string>();


            for (int i = 0; i < journeys.Count; i++)
            {
                var ticket = await _ticketRepository.CreateTicket(journeys[i], startStarport, endStarport, firstName, lastName, cif, userId, seatNumber);

                (string outputFolderAndFileName, string outputFilePath) = _ticketService.GeneratePdfwithAppSettings(ticket);

                ticketsPaths.Add(outputFilePath);

                tickets.Add(ticket);
            }

            if (!actuallyCreateTicket)
            {
                foreach (var segment in routeSegments)
                {
                    await _seatAllocationRepository.MakeSeatOccupied(segment.SegmentId, seatNumber);
                }
            }


                var combinedPdfBytes = _ticketService.CombineAndReturnPdf(ticketsPaths);

                return File(combinedPdfBytes, "application/pdf", "tickets.pdf");

        }


        [HttpPost("orderM")]
        public async Task<IActionResult> OrderRoute([FromBody] OrderRequestDTO request)
        {

            var tickets = new List<Ticket>();
            var routeSegments = request.Route.RouteSegments;
            var journeys = routeSegments.Select(segment => segment.JourneyId).Distinct().ToList();
            var startStarport = routeSegments.First().DepartureId;
            var endStarport = routeSegments.Last().ArrivalId;
            var ticketsPaths = new List<string>();

            foreach (var passenger in request.Passengers)
            {
                for (int i = 0; i < journeys.Count; i++)
                {
                    var ticket = await _ticketRepository.CreateTicket(journeys[i], startStarport, endStarport, passenger.passenger.firstName, passenger.passenger.lastName, passenger.passenger.cif, 1, passenger.seat.seatNumber);
                    (string outputFolderAndFileName, string outputFilePath) = _ticketService.GeneratePdfwithAppSettings(ticket);
                    ticketsPaths.Add(outputFilePath);
                    tickets.Add(ticket);
                }

                if (request.ActuallyCreateTicket)
                {
                    foreach (var segment in routeSegments)
                    {
                        await _seatAllocationRepository.MakeSeatOccupied(segment.SegmentId, passenger.seat.seatNumber);
                    }
                }
            }

            

            var combinedPdfBytes = _ticketService.CombineAndReturnPdf(ticketsPaths);
            return File(combinedPdfBytes, "application/pdf", "tickets.pdf");
        }


    }
}



