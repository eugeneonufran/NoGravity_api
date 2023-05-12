﻿using Microsoft.AspNetCore.Mvc;
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
            var routeDTOs = await _ticketService.GetRoutes(departureStarportId, arrivalStarportId, date, sortType);

            return Ok(routeDTOs);

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
                var allSeats = await _ticketService.GetAllSeatsInSegment(segment.SegmentId);

                var seatDTOList = new List<SeatDTO>();

                // Check if the current segment is the last segment in the route
                var isLastSegment = segment == routeDTO.RouteSegments.Last();

                foreach (var seatNumber in allSeats)
                {
                    // Determine the seat color based on availability for all further segments
                    var seatColor = await GetSeatColor(seatNumber, segment, routeDTO.RouteSegments, isLastSegment, availableSeats.ToList());
                    var seatDTO = new SeatDTO(seatNumber, seatColor);
                    seatDTOList.Add(seatDTO);
                }

                segmentSeatMaps.Add(new SegmentSeatMapDTO(segment.SegmentId, seatDTOList));
            }

            var routeSeatMapDTO = new RouteSeatMapDTO(segmentSeatMaps);

            return Ok(routeSeatMapDTO);
        }

        private async Task<SeatColor> GetSeatColor(int seatNumber, RouteSegmentDTO segment, List<RouteSegmentDTO> routeSegments, bool isLastSegment, List<int> availableSeats)
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
                    var nextSegmentAvailableSeats = await _ticketService.GetAvailableSeatsInSegment(nextSegment.SegmentId);

                    if (!nextSegmentAvailableSeats.Contains(seatNumber))
                    {
                        isAvailableForAllSegments = false;
                        break;
                    }
                }

                if (isAvailableForAllSegments)
                {
                    // Seat is available in all further segments
                    return SeatColor.Green;
                }
                else
                {
                    // Seat is not available in at least one segment
                    return SeatColor.Grey;
                }
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



