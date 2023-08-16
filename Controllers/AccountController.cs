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
        public async Task<IActionResult> GetUserProfile(User user)
        {
            var foundUser = await _userRepository.GetUserById(user.Id);

            if (foundUser != null)
            {
                var tickets = await _ticketRepository.GetAll();
                var userTickets = tickets.Where(ticket => ticket.UserId == user.Id).ToList();

                return Ok(userTickets);
            }
            return Created("success", _userRepository.CreateUser(user));
        }
    }
}
