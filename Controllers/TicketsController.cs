using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NoGravity.Data.DataModel;
using NoGravity.Data.DataServices;
using NoGravity.Data.Tables;
using System.Diagnostics.Eventing.Reader;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NoGravity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly ITicketsDataService _ticketService;
        private readonly NoGravityDbContext _noGravityDbContext;

        public TicketsController(ITicketsDataService ticketService, NoGravityDbContext noGravityDbContext)
        {
            _ticketService = ticketService;
            _noGravityDbContext = noGravityDbContext;
        }


        [HttpGet("departure")]
        public async Task<IActionResult> GetTicketsByDeparture([FromQuery] int departureId)
        {
            var tickets = await _ticketService.GetTicketsByDeparture(departureId);

            if (tickets == null || tickets.Count == 0)
            {
                return NotFound();
            }

            return Ok(tickets);
        }

        [HttpGet("booking")]
        public async Task<IActionResult> FindRoute(int departureStarportId, int arrivalStarportId)
        {
            var journeys = await _noGravityDbContext.Journeys.ToListAsync();

            decimal totalPrice = 0;

            foreach (var journey in journeys)
            {

                var segments = await _noGravityDbContext.JourneySegments.Where(js=>js.JourneyId == journey.Id).ToListAsync();

                var routeSegments = new List<JourneySegment>();
                var visitedSegments = new HashSet<JourneySegment>();

                DFS(segments, departureStarportId, arrivalStarportId, visitedSegments, routeSegments);

                return Ok(routeSegments);
         
            }
            return BadRequest();

        }


        
    
    }








    /*

    // GET: api/<TicketsController>
    [HttpGet]
    public IEnumerable<string> Get()
    {
        return new string[] { "value1", "value2" };
    }

    // GET api/<TicketsController>/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
        return "value";
    }

    // POST api/<TicketsController>
    [HttpPost]
    public void Post([FromBody] string value)
    {
    }

    // PUT api/<TicketsController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<TicketsController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {

    }
    */
    
}
