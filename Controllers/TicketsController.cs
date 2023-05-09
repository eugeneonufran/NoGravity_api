﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NoGravity.Data.DataModel;
using NoGravity.Data.DTO.Tickets;
using NoGravity.Data.Repositories.Interfaces;
using NoGravity.Data.Tables;

namespace NoGravity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly ITicketsDataService _ticketService;
        private readonly NoGravityDbContext _noGravityDbContext;
        private readonly ITicketRepository _ticketRepository;

        public TicketsController(ITicketRepository ticketRepository, ITicketsDataService ticketService, NoGravityDbContext noGravityDbContext)
        {
            _ticketService = ticketService;
            _noGravityDbContext = noGravityDbContext;
            _ticketRepository = ticketRepository;
        }


        [HttpGet("tickets/{id}")]
        public async Task<IActionResult> GetTicketById(Guid id)
        {
            try
            {
                var ticket = await _ticketRepository.GetById(id);

                if (ticket == null)
                {
                    return NotFound();
                }

                // Optionally, you can map the ticket to a DTO or return it directly
                // without any additional processing

                return Ok(ticket);
            }
            catch (Exception ex)
            {
                // Handle any exceptions and return an appropriate error response
                return StatusCode(500, "An error occurred while retrieving the ticket.");
            }
        }

        [HttpDelete("tickets/{id}")]
        public async Task<IActionResult> DeleteTicket(Guid id)
        {
            try
            {
                var ticket = await _ticketRepository.GetById(id);

                if (ticket == null)
                {
                    return NotFound();
                }

                await _ticketRepository.Delete(ticket.Id);

                return NoContent();
            }
            catch (Exception ex)
            {
                // Handle any exceptions and return an appropriate error response
                return StatusCode(500, "An error occurred while deleting the ticket.");
            }
        }

        [HttpPost("tickets/create")]
        public async Task<IActionResult> CreateTicket([FromBody] TicketDTO ticketDTO)
        {
            try
            {
                // Map the TicketDTO to a Ticket entity
                var ticket = new Ticket
                {  
                    JourneyId = ticketDTO.JourneyId,
                    StartStarportId = ticketDTO.StartStarportId,
                    EndStarportId = ticketDTO.EndStarportId,
                    PassengerFirstName = ticketDTO.PassengerFirstName,
                    PassengerSecondName = ticketDTO.PassengerSecondName,
                    CIF = ticketDTO.CIF,
                    UserId = ticketDTO.UserId,
                    SeatNumber = ticketDTO.SeatNumber,
                    BookingDateTime = DateTime.Now // Assuming the current date and time should be used for booking
                };

                // Save the ticket in the repository
                await _ticketRepository.Create(ticket);

                return Ok(ticket);
            }
            catch (Exception ex)
            {
                // Handle any exceptions and return an appropriate error response
                return StatusCode(500, "An error occurred while creating the ticket.");
            }
        }

        [HttpGet("tickets/getall")]
        public async Task<IActionResult> GetAllTickets()
        {
            try
            {
                var tickets = await _ticketRepository.GetAll();

                // Optionally, you can map the tickets to DTOs or return them directly
                // without any additional processing

                return Ok(tickets);
            }
            catch (Exception ex)
            {
                // Handle any exceptions and return an appropriate error response
                return StatusCode(500, "An error occurred while retrieving the tickets.");
            }
        }



    }
}
