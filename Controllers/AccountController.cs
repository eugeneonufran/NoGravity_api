using Microsoft.AspNetCore.Mvc;
using NoGravity.Data.Tables;

namespace NoGravity.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly IUsersRepository _userRepository;
        private readonly ITicketRepository _ticketRepository;
        private readonly IConfiguration _configuration;

        public AccountController(IUsersRepository userRepository, ITicketRepository ticketRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _ticketRepository = ticketRepository;
            _configuration = configuration;
        }

        [HttpPost("getUserProfile")]
        public async Task<IActionResult> GetUserProfile(int userId)
        {
            var foundUser = await _userRepository.GetUserById(userId);

            if (foundUser != null)
            {
                var tickets = await _ticketRepository.GetAll();
                var userTickets = tickets.Where(ticket => ticket.UserId == userId).ToList();

                return Ok(userTickets);
            }
            return NotFound("User not found");
        }

        [HttpPost("getTicket")]
        public async Task<IActionResult> GetUserTicket(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return BadRequest("Path is missing or empty.");
            }

            var fullPath = _configuration["TicketsPaths:GENERATED"] + path;

            if (!System.IO.File.Exists(fullPath))
            {
                return NotFound("File not found.");
            }

            try
            {
                var bytes = await System.IO.File.ReadAllBytesAsync(fullPath);
                return File(bytes, "application/pdf", Path.GetFileName(fullPath));
            }
            catch (Exception ex)
            {
                // Log the error and return an appropriate response
                return StatusCode(500, "An error occurred while fetching the file.");
            }
        }

    }
}
