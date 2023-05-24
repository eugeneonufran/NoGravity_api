namespace NoGravity.Controllers
{
    [ApiController]
    [Route("api/starports")]
    public class StarportsController : ControllerBase
    {
        private readonly IStarportRepository _starportRepository;

        public StarportsController(IStarportRepository starportRepository)
        {
            _starportRepository = starportRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStarports()
        {
            var starports = await _starportRepository.GetAllStarports();
            return Ok(starports);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStarportById(int id)
        {
            var starport = await _starportRepository.GetStarportById(id);
            if (starport == null)
                return NotFound();

            return Ok(starport);
        }

        [HttpPost]
        public async Task<IActionResult> CreateStarport(StarportDTO starportDTO)
        {
            var createdStarport = await _starportRepository.CreateStarport(starportDTO);
            return CreatedAtAction(nameof(GetStarportById), new { id = createdStarport.Id }, createdStarport);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStarport(int id, StarportDTO starportDTO)
        {
            var updatedStarport = await _starportRepository.UpdateStarport(id, starportDTO);
            if (updatedStarport == null)
                return NotFound();

            return Ok(updatedStarport);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStarport(int id)
        {
            var result = await _starportRepository.DeleteStarport(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
