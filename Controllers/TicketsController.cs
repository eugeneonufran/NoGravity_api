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
        public async Task<IActionResult> FindAllPaths(int departureStarportId, int arrivalStarportId, SortType sortType = SortType.Optimal)
        {

            var routes = await _ticketService.GetRoutes(departureStarportId, arrivalStarportId, sortType);

            var routeInfo = routes.Select(route =>
            {
                var routeSegments = new List<object>();
                DateTime? previousArrivalTime = null;

                for (int i = 0; i < route.Count(); i++)
                {
                    var currentRoute = route.ElementAt(i);

                    TimeSpan? idleTime = previousArrivalTime.HasValue
                        ? currentRoute.DepartureDateTime - previousArrivalTime.Value
                        : (TimeSpan?)null;

                    var segmentInfo = new
                    {
                        segmentID = currentRoute.Id,
                        journeyID = currentRoute.JourneyId,
                        departureStarportId = currentRoute.DepartureStarportId,
                        arrivalStarportId = currentRoute.ArrivalStarportId,
                        departureDateTime = currentRoute.DepartureDateTime,
                        arrivalDateTime = currentRoute.ArrivalDateTime,
                        order = currentRoute.Order,
                        price = currentRoute.Price,
                        travelTime = currentRoute.ArrivalDateTime - currentRoute.DepartureDateTime,
                        idleTime = idleTime
                    };

                    routeSegments.Add(segmentInfo);
                    previousArrivalTime = currentRoute.ArrivalDateTime;
                }

                return new
                {
                    routeSegments
                };
            });

            return Ok(new
            {
                Routes = routeInfo,
                OptionsCount = routeInfo.Count()
            });


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


