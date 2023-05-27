
namespace NoGravity.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SeatAllocationsController : ControllerBase
    {
        private readonly ISeatAllocationsRepository _seatAllocationsRepository;

        public SeatAllocationsController(ISeatAllocationsRepository seatAllocationsRepository)
        {
            _seatAllocationsRepository = seatAllocationsRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SeatAllocation>>> GetAllSeatAllocations()
        {
            var seatAllocations = await _seatAllocationsRepository.GetAllSeatAllocations();
            return Ok(seatAllocations);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SeatAllocation>> GetSeatAllocationById(int id)
        {
            var seatAllocation = await _seatAllocationsRepository.GetSeatAllocationById(id);
            if (seatAllocation == null)
            {
                return NotFound();
            }

            return Ok(seatAllocation);
        }

        [HttpPost]
        public async Task<ActionResult<SeatAllocation>> CreateSeatAllocation(SeatAllocation seatAllocation)
        {
            await _seatAllocationsRepository.CreateSeatAllocation(seatAllocation);
            return CreatedAtAction(nameof(GetSeatAllocationById), new { id = seatAllocation.Id }, seatAllocation);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSeatAllocation(int id, SeatAllocationDTO seatAllocation)
        {
            if (id != seatAllocation.Id)
            {
                return BadRequest();
            }

            var ex=await _seatAllocationsRepository.UpdateSeatAllocation(seatAllocation);
            return Ok(ex);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSeatAllocation(int id)
        {
            var seatAllocation = await _seatAllocationsRepository.GetSeatAllocationById(id);
            if (seatAllocation == null)
            {
                return NotFound();
            }

            await _seatAllocationsRepository.DeleteSeatAllocation(seatAllocation);
            return NoContent();
        }
    }
}
