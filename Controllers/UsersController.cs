namespace NoGravity.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersRepository _userRepository;

        public UsersController(IUsersRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userRepository.GetAllUsers();
            return Ok(users);
        }

        [HttpGet("get/{Id}")]
        public async Task<IActionResult> GetUserById(int userId)
        {
            var user = await _userRepository.GetUserById(userId);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        //[HttpPost("create")]
        //public async Task<IActionResult> CreateUser(UserDTO userDTO)
        //{
        //    var createdUser = await _userRepository.CreateUser(userDTO);
        //   // return CreatedAtAction(nameof(GetUserById), new { userId = createdUser.Id }, createdUser);
        //}

        [HttpPut("update/{Id}")]
        public async Task<IActionResult> UpdateUser(int userId, UserDTO userDTO)
        {
            var updatedUser = await _userRepository.UpdateUser(userId, userDTO);
            if (updatedUser == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("delete/{Id}")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            var result = await _userRepository.DeleteUser(userId);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
