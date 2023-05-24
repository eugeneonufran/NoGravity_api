namespace NoGravity.Controllers
{
    [Route("api/[controller]")]
    public class StarportsController : ControllerBase
    {
        private readonly IStarportsRepository _starportsRepository;

        public StarportsController(IStarportsRepository starportRepository)
        {
            _starportsRepository = starportRepository;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAllStarports()
        {
            var starports = await _starportsRepository.GetAllStarports();
            return Ok(starports);
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetStarportById(int id)
        {
            var starport = await _starportsRepository.GetStarportById(id);
            if (starport == null)
                return NotFound();

            return Ok(starport);
        }

        [HttpPost("create/{id}")]
        public async Task<IActionResult> CreateStarport(StarportDTO starportDTO)
        {
            var createdStarport = await _starportsRepository.CreateStarport(starportDTO);
            return CreatedAtAction(nameof(GetStarportById), new { id = createdStarport.Id }, createdStarport);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateStarport(int id, StarportDTO starportDTO)
        {
            var updatedStarport = await _starportsRepository.UpdateStarport(id, starportDTO);
            if (updatedStarport == null)
                return NotFound();

            return Ok(updatedStarport);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteStarport(int id)
        {
            var result = await _starportsRepository.DeleteStarport(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
