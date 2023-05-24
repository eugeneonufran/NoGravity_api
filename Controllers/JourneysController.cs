namespace NoGravity.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JourneyController : ControllerBase
    {
        private readonly IJourneysRepository _journeysRepository;

        public JourneyController(IJourneysRepository journeyRepository)
        {
            _journeysRepository = journeyRepository;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAllJourneys()
        {
            var journeys = await _journeysRepository.GetAllJourneys();
            return Ok(journeys);
        }

        [HttpGet("get/{Id}")]
        public async Task<IActionResult> GetJourneyById(int journeyId)
        {
            var journey = await _journeysRepository.GetJourneyById(journeyId);
            if (journey == null)
            {
                return NotFound();
            }

            return Ok(journey);
        }

        [HttpPost]
        public async Task<IActionResult> CreateJourney(JourneyDTO journeyDTO)
        {
            var createdJourney = await _journeysRepository.CreateJourney(journeyDTO);
            return CreatedAtAction(nameof(GetJourneyById), new { journeyId = createdJourney.Id }, createdJourney);
        }

        [HttpPut("update/{Id}")]
        public async Task<IActionResult> UpdateJourney(int journeyId, JourneyDTO journeyDTO)
        {
            var updatedJourney = await _journeysRepository.UpdateJourney(journeyId, journeyDTO);
            if (updatedJourney == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("delete/{Id}")]
        public async Task<IActionResult> DeleteJourney(int journeyId)
        {
            var result = await _journeysRepository.DeleteJourney(journeyId);
            if (!result)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
