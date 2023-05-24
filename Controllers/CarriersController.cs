namespace NoGravity.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarriersController : ControllerBase
    {
        private readonly ICarrierRepository _carrierRepository;

        public CarriersController(ICarrierRepository carrierRepository)
        {
            _carrierRepository = carrierRepository;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAllCarriers()
        {
            var carriers = await _carrierRepository.GetAllCarriers();
            return Ok(carriers);
        }

        [HttpGet("get/{Id}")]
        public async Task<IActionResult> GetCarrierById(int carrierId)
        {
            var carrier = await _carrierRepository.GetCarrierById(carrierId);
            if (carrier == null)
            {
                return NotFound();
            }

            return Ok(carrier);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateCarrier(CarrierDTO carrierDTO)
        {
            var createdCarrier = await _carrierRepository.CreateCarrier(carrierDTO);
            return CreatedAtAction(nameof(GetCarrierById), new { carrierId = createdCarrier.Id }, createdCarrier);
        }

        [HttpPut("update/{Id}")]
        public async Task<IActionResult> UpdateCarrier(int carrierId, CarrierDTO carrierDTO)
        {
            var updatedCarrier = await _carrierRepository.UpdateCarrier(carrierId, carrierDTO);
            if (updatedCarrier == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("delete/{Id}")]
        public async Task<IActionResult> DeleteCarrier(int carrierId)
        {
            var result = await _carrierRepository.DeleteCarrier(carrierId);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
