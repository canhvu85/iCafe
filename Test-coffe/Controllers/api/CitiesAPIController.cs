using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
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
        private string route;
        private string method;
        private string remember_token;
        private string idUser;
        private JwtSecurityToken token;

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

            route = Request.Path.Value;
            //Console.WriteLine(route.);
            method = Request.Method;
            remember_token = HttpContext.Request.Headers["Authorization"];
            token = new JwtSecurityTokenHandler().ReadJwtToken(remember_token);
            idUser = token.Claims.First(claim => claim.Type == "id").Value;

            if (_context.PermissionDetails.Any(pd => pd.UsersId == int.Parse(idUser) &&
                route.StartsWith(pd.permalink_permissions) && pd.action == method))
            {
                _citiesRepository.UpdateCities(id, cities);
                return NoContent();
            }
            else
                return StatusCode(203);


            //var result = from p in _context.PermissionDetails
            //             where p.isDeleted == false && p.UsersId == int.Parse(idUser) &&
            //            route.StartsWith(p.permalink_permissions) && p.action == method
            //             select new
            //             {
            //                 p.id
            //             };
            ////Console.WriteLine()
            //if (result.Count() > 0)
            //{
            //    _citiesRepository.UpdateCities(id, cities);
            //    return NoContent();
            //}
            //else
            //    return StatusCode(203);


        }

        // POST: api/CitiesAPI
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Cities>> PostCities(Cities cities)
        {
            route = Request.Path.Value;
            method = Request.Method;
            remember_token = HttpContext.Request.Headers["Authorization"];
            token = new JwtSecurityTokenHandler().ReadJwtToken(remember_token);
            idUser = token.Claims.First(claim => claim.Type == "id").Value;

            if (_context.PermissionDetails.Any(pd => pd.UsersId == int.Parse(idUser) &&
                pd.permalink_permissions == route && pd.action == method))
            {
                _citiesRepository.CreateCities(cities);
                return NoContent();
            }
            else
                return StatusCode(203);
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
            route = Request.Path.Value;
            method = Request.Method;
            remember_token = HttpContext.Request.Headers["Authorization"];
            token = new JwtSecurityTokenHandler().ReadJwtToken(remember_token);
            idUser = token.Claims.First(claim => claim.Type == "id").Value;
            var username = token.Claims.First(claim => claim.Type == "username").Value;

            if (_context.PermissionDetails.Any(pd => pd.UsersId == int.Parse(idUser) &&
                route.StartsWith(pd.permalink_permissions) && pd.action == method))
            {
                _citiesRepository.RemoveCities(id, username);
                return NoContent();
            }
            else
                return StatusCode(203);
        }
    }
}
