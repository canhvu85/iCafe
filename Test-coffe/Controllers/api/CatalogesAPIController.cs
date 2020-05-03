using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
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
    public class CatalogesAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ICataloges _catalogesRepository;

        public CatalogesAPIController(ApplicationDbContext context, ICataloges catalogesRepository)
        {
            _context = context;
            _catalogesRepository = catalogesRepository;
        }

        // GET: api/CatalogesAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cataloges>>> GetCataloge2()
        {
            var result = _catalogesRepository.GetAllCataloges();
            return Ok(result);
        }

        [HttpGet("shop/{ShopId}")]
        public IActionResult GetCatalogesByShop(int? ShopId)
        {
            var result = _catalogesRepository.GetAllCatalogesByShop(ShopId);
            return Ok(result);
        }

        // PUT: api/CatalogesAPI/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCataloges(int id, Cataloges cataloges)
        {
            if (id != cataloges.id)
            {
                return BadRequest();
            }
            var user = HttpContext.Session.GetObjectFromJson<Users>("user");
            _catalogesRepository.UpdateCataloges(id, cataloges);
            return NoContent();
        }

        // POST: api/CatalogesAPI
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Cataloges>> PostCataloges(Cataloges cataloges)
        {
            var user = HttpContext.Session.GetObjectFromJson<Users>("user");
            _catalogesRepository.CreateCataloges(cataloges);
            return NoContent();
        }

        // DELETE: api/CatalogesAPI/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Cataloges>> DeleteCataloges(int id)
        {
            var cataloges = await _context.Cataloges.FindAsync(id);
            if (cataloges == null)
            {
                return NotFound();
            }
            string remember_token = HttpContext.Request.Headers["Authorization"];
            var token = new JwtSecurityTokenHandler().ReadJwtToken(remember_token);
            var username = token.Claims.First(claim => claim.Type == "username").Value;
            _catalogesRepository.RemoveCataloges(id, username);
            return NoContent();
        }
    }
}
