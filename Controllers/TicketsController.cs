using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NoGravity.Data.DataModel;
using NoGravity.Data.DataServices;
using static NoGravity.Data.NoGravityEnums;

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


        [HttpGet("booking")]
        public async Task<IActionResult> FindAllPaths(int departureStarportId, int arrivalStarportId, SortType sortType=SortType.Optimal)
        {
            var journeys = await _noGravityDbContext.Journeys.ToListAsync();

            var routes = await _ticketService.GetRoutes(departureStarportId, arrivalStarportId);

            return Ok(new
            {
                Routes = routes,
                Optionscount = routes.Count(),
            });
        }

        [HttpGet("booking/fastest")]
        public async Task<IActionResult> FindFastestPaths(int departureStarportId, int arrivalStarportId)
        {

            var fastest = await _ticketService.GetRoutesSortedByTime(departureStarportId, arrivalStarportId);

            return Ok(fastest);
        }

        [HttpGet("booking/best_price")]
        public async Task<IActionResult> FindCheapestPaths(int departureStarportId, int arrivalStarportId)
        {

            var fastest = await _ticketService.GetRoutesSortedByPrice(departureStarportId, arrivalStarportId);

            return Ok(fastest);
        }

        [HttpGet("booking/optimal")]
        public async Task<IActionResult> FindOptimalPaths(int departureStarportId, int arrivalStarportId)
        {

            var fastest = await _ticketService.GetRoutesSortedByOptimal(departureStarportId, arrivalStarportId);

            return Ok(fastest);
        }
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


