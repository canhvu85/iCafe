using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Test_coffe.Controllers.Services;
using Test_coffe.Models;

namespace Test_coffe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ICities _citiesRepository;

        public CitiesAPIController(ApplicationDbContext context, ICities citiesRepository)
        {
            _context = context;
            _citiesRepository = citiesRepository;
        }

        // GET: api/CitiesAPI
        //[EnableCors("Policy1")]
        [HttpGet]
        public IActionResult GetCity2()
        {
            var result = _citiesRepository.GetAllCities();
            //Console.WriteLine(result.id);

            return Ok(result);
        }

        // GET: api/CitiesAPI
        [HttpGet("withtoken")]
        public IActionResult GetCityToken()
        {
            var result = _citiesRepository.GetAllCities();
            return Ok(result);
        }

        // PUT: api/CitiesAPI/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCities(int id, Cities cities)
        {
            if (id != cities.id)
            {
                return BadRequest();
            }
            var user = HttpContext.Session.GetObjectFromJson<Users>("user");
            cities.updated_by = user.username;
            _citiesRepository.UpdateCities(id, cities);
            return NoContent();
        }

        // POST: api/CitiesAPI
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Cities>> PostCities(Cities cities)
        {
            var user = HttpContext.Session.GetObjectFromJson<Users>("user");
            cities.created_by = user.username;
            _citiesRepository.CreateCities(cities);
            return NoContent();
        }

        // DELETE: api/CitiesAPI/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Cities>> DeleteCity(int id)
        {
            var cities = await _context.Cities.FindAsync(id);
            if (cities == null)
            {
                return NotFound();
            }
            var user = HttpContext.Session.GetObjectFromJson<Users>("user");
            _citiesRepository.RemoveCities(id, user.username);
            return NoContent();
        }
    }
}
