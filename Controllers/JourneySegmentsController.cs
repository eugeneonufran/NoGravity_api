using Microsoft.AspNetCore.Mvc;
using NoGravity.Data.DTO;
using NoGravity.Data.Repositories;
using NoGravity.Data.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NoGravity.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JourneySegmentsController : ControllerBase
    {
        private readonly IJourneySegmentsRepository _journeySegmentsRepository;

        public JourneySegmentsController(IJourneySegmentsRepository journeySegmentRepository)
        {
            _journeySegmentsRepository = journeySegmentRepository;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAllJourneySegments()
        {
            var journeySegments = await _journeySegmentsRepository.GetAllJourneySegments();
            return Ok(journeySegments);
        }

        [HttpGet("get/{Id}")]
        public async Task<IActionResult> GetJourneySegmentById(int journeySegmentId)
        {
            var journeySegment = await _journeySegmentsRepository.GetJourneySegmentById(journeySegmentId);
            if (journeySegment == null)
            {
                return NotFound();
            }

            return Ok(journeySegment);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateJourneySegment(JourneySegmentDTO journeySegmentDTO)
        {
            var createdJourneySegment = await _journeySegmentsRepository.CreateJourneySegment(journeySegmentDTO);
            return CreatedAtAction(nameof(GetJourneySegmentById), new { journeySegmentId = createdJourneySegment.Id}, createdJourneySegment);;
        }

        [HttpPut("update/{Id}")]
        public async Task<IActionResult> UpdateJourneySegment(int journeySegmentId, JourneySegmentDTO journeySegmentDTO)
        {
            var updatedJourneySegment = await _journeySegmentsRepository.UpdateJourneySegment(journeySegmentId, journeySegmentDTO);
            if (updatedJourneySegment == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("delete/{Id}")]
        public async Task<IActionResult> DeleteJourneySegment(int journeySegmentId)
        {
            var result = await _journeySegmentsRepository.DeleteJourneySegment(journeySegmentId);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
