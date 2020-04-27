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
    public class CatalogesAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ITokenBuilder _tokenBuilder;
        private bool isExpired;
        private readonly ICataloges _catalogesRepository;

        public CatalogesAPIController(ApplicationDbContext context, ITokenBuilder tokenBuilder, ICataloges catalogesRepository)
        {
            _context = context;
            _tokenBuilder = tokenBuilder;
            _catalogesRepository = catalogesRepository;
        }

        // GET: api/CatalogesAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cataloges>>> GetCataloge2()
        {
            isExpired = _tokenBuilder.isExpiredToken();
            if (isExpired == false)
            {
                var result = _catalogesRepository.GetAllCataloges();
                return Ok(result);
            }
            else
                return Unauthorized();
        }

        [HttpGet("shop/{ShopId}")]
        public IActionResult GetCatalogesByShop(int? ShopId)
        {
            isExpired = _tokenBuilder.isExpiredToken();
            if (isExpired == false)
            {
                var result = _catalogesRepository.GetAllCatalogesByShop(ShopId);
                return Ok(result);
            }
            else
                return Unauthorized();
        }

        // PUT: api/CatalogesAPI/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCataloges(int id, Cataloges cataloges)
        {
            isExpired = _tokenBuilder.isExpiredToken();
            if (isExpired == false)
            {
                if (id != cataloges.id)
                {
                    return BadRequest();
                }
                var user = HttpContext.Session.GetObjectFromJson<Users>("user");
                cataloges.updated_by = user.username;
                _catalogesRepository.UpdateCataloges(id, cataloges);
                return NoContent();
            }
            else
                return Unauthorized();
        }

        // POST: api/CatalogesAPI
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Cataloges>> PostCataloges(Cataloges cataloges)
        {
            isExpired = _tokenBuilder.isExpiredToken();
            if (isExpired == false)
            {
                var user = HttpContext.Session.GetObjectFromJson<Users>("user");
                cataloges.created_by = user.username;
                _catalogesRepository.CreateCataloges(cataloges);
                return NoContent();
            }
            else
                return Unauthorized();
        }

        // DELETE: api/CatalogesAPI/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Cataloges>> DeleteCataloges(int id)
        {
            isExpired = _tokenBuilder.isExpiredToken();
            if (isExpired == false)
            {
                var cataloges = await _context.Cataloges.FindAsync(id);
                if (cataloges == null)
                {
                    return NotFound();
                }
                var user = HttpContext.Session.GetObjectFromJson<Users>("user");
                _catalogesRepository.RemoveCataloges(id, user.username);
                return NoContent();
            }
            else
                return Unauthorized();
        }
    }
}
