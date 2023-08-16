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

        public AccountController(IUsersRepository userRepository, ITicketRepository ticketRepository)
        {
            _userRepository = userRepository;
            _ticketRepository = ticketRepository;
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
            return Ok("success");
        }

        [HttpPost("getTicket{id}")]
        public async Task<IActionResult> GetUserTicket(string path)
        {
            
        }
    }
}
