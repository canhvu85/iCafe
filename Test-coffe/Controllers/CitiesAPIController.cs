using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test_coffe.Controllers.Services;
using Test_coffe.Models;

namespace Test_coffe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ITokenBuilder _tokenBuilder;
        private bool isExpired;

        public CitiesAPIController(ApplicationDbContext context, ITokenBuilder tokenBuilder)
        {
            _context = context;
            _tokenBuilder = tokenBuilder;
        }

        // GET: api/CitiesAPI
        [HttpGet]
        public IActionResult GetCity2()
        {
            var result = from c in _context.Cities
                         where c.isDeleted == false
                         orderby c.name
                         select new
                         {
                             c.id,
                             c.name
                         };
            return Ok(result);
        }

        // GET: api/CitiesAPI
        [HttpGet("withtoken")]
        public IActionResult GetCityToken()
        {
            isExpired = _tokenBuilder.isExpiredToken();
            if (isExpired == false)
            {
                var result = from c in _context.Cities
                             where c.isDeleted == false
                             orderby c.name
                             select new
                             {
                                 c.id,
                                 c.name
                             };
                return Ok(result);
            }
            else
                return Unauthorized();
        }

        [HttpGet("full")]
        public async Task<ActionResult<IEnumerable<Cities>>> GetCityFull()
        {
            isExpired = _tokenBuilder.isExpiredToken();
            if (isExpired == false)
            {
                return await _context.Cities.ToListAsync();
            }
            else
                return Unauthorized();

        }

        // GET: api/CitiesAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cities>> GetCities(int id)
        {
            var cities = await _context.Cities.FindAsync(id);

            if (cities == null)
            {
                return NotFound();
            }

            return cities;
        }

        // PUT: api/CitiesAPI/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCities(int id, Cities cities)
        {
            isExpired = _tokenBuilder.isExpiredToken();
            if (isExpired == false)
            {
                if (id != cities.id)
                {
                    return BadRequest();
                }
                var user = HttpContext.Session.GetObjectFromJson<Users>("user");
                var citiesOld = _context.Cities.Find(id);
                citiesOld.updated_at = DateTime.Now;
                citiesOld.updated_by = user.username;
                citiesOld.name = cities.name;
                _context.Entry(citiesOld).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CitiesExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return NoContent();
            }
            else
                return Unauthorized();
        }

        // POST: api/CitiesAPI
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Cities>> PostCities(Cities cities)
        {
            isExpired = _tokenBuilder.isExpiredToken();
            if (isExpired == false)
            {
                var user = HttpContext.Session.GetObjectFromJson<Users>("user");
                cities.created_by = user.username;
                _context.Cities.Add(cities);
                await _context.SaveChangesAsync();

                return Ok(cities.id);
            }
            else
                return Unauthorized();
        }

        // DELETE: api/CitiesAPI/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Cities>> DeleteCity(int id)
        {
            isExpired = _tokenBuilder.isExpiredToken();
            if (isExpired == false)
            {
                var cities = await _context.Cities.FindAsync(id);
                if (cities == null)
                {
                    return NotFound();
                }
                var user = HttpContext.Session.GetObjectFromJson<Users>("user");
                cities.deleted_by = user.username;
                cities.deleted_at = DateTime.Now;
                cities.isDeleted = true;
                _context.Entry(cities).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                return cities;
            }
            else
                return Unauthorized();
        }

        private bool CitiesExists(int id)
        {
            return _context.Cities.Any(e => e.id == id);
        }
    }
}
