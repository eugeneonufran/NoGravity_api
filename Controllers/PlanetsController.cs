namespace NoGravity.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlanetsController : ControllerBase
    {
        private readonly IPlanetsRepository _planetsRepository;

        public PlanetsController(IPlanetsRepository planetRepository)
        {
            _planetsRepository = planetRepository;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAllPlanets()
        {
            var planets = await _planetsRepository.GetAllPlanets();
            return Ok(planets);
        }

        [HttpGet("get/{Id}")]
        public async Task<IActionResult> GetPlanetById(int planetId)
        {
            var planet = await _planetsRepository.GetPlanetById(planetId);
            if (planet == null)
            {
                return NotFound();
            }

            return Ok(planet);
        }

        [HttpPost("create/{Id}")]
        public async Task<IActionResult> CreatePlanet(PlanetDTO planetDTO)
        {
            var createdPlanet = await _planetsRepository.CreatePlanet(planetDTO);
            return CreatedAtAction(nameof(GetPlanetById), new { planetId = createdPlanet.Id }, createdPlanet);
        }

        [HttpPut("update/{Id}")]
        public async Task<IActionResult> UpdatePlanet(int planetId, PlanetDTO planetDTO)
        {
            var updatedPlanet = await _planetsRepository.UpdatePlanet(planetId, planetDTO);
            if (updatedPlanet == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("delete/{Id}")]
        public async Task<IActionResult> DeletePlanet(int planetId)
        {
            var result = await _planetsRepository.DeletePlanet(planetId);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
