
namespace NoGravity.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller

    {
        private readonly IUsersRepository _userRepository;
        private readonly IJWTService _jwtService;

        public AuthController(IUsersRepository userRepository)
        {
            _userRepository = userRepository;
            _jwtService = new JWTService();
        }

        [HttpPost("register")]
        public IActionResult Register(UserRegisterDTO dto)
        {
            var user = new User
            {
                FirstName = dto.FirstName,
                SecondName = dto.SecondName,
                Email = dto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            };


            return Created("success", _userRepository.CreateUser(user));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDTO dto)
        {
            var user = await _userRepository.GetUserByEmail(dto.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
            {

                return StatusCode(StatusCodes.Status401Unauthorized, "Invalid credentials");
            }

            var jwt = _jwtService.GenerateToken(user.Id);

            Response.Cookies.Append("jwt", jwt, new CookieOptions
            {
                HttpOnly = true
            });

            return Ok(new
            {
                message = "success"
            });
        }

        [HttpGet("user")]
        public async Task<IActionResult> GetUser()
        {
            try
            {
                var jwt = Request.Cookies["jwt"];
                if (jwt is null)
                {
                    return Unauthorized("No authorized user");
                }
                var token = _jwtService.Verify(jwt);

                int userId = int.Parse(token.Issuer);
                var user = await _userRepository.GetUserById(userId);

                return Ok(user);

            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }

        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            Response.Cookies.Delete("jwt");
            return Ok();

        }
    }
}
