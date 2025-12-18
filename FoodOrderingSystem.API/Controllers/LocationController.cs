using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FoodOrderingSystem.API.Data;

namespace FoodOrderingSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocationController : ControllerBase
    {
        private readonly FoodOrderingDbContext _context;

        public LocationController(FoodOrderingDbContext context)
        {
            _context = context;
        }

        [HttpGet("states")]
        public async Task<ActionResult> GetStates()
        {
            var states = await _context.States.Select(s => new { s.StateId, s.StateName }).ToListAsync();
            return Ok(states);
        }

        [HttpGet("cities/{stateId}")]
        public async Task<ActionResult> GetCitiesByState(int stateId)
        {
            var cities = await _context.Cities.Where(c => c.StateId == stateId).Select(c => new { c.CityId, c.CityName }).ToListAsync();
            return Ok(cities);
        }
    }
}
