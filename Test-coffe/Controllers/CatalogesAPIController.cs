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

        public CatalogesAPIController(ApplicationDbContext context, ITokenBuilder tokenBuilder)
        {
            _context = context;
            _tokenBuilder = tokenBuilder;
        }

        // GET: api/CatalogesAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cataloges>>> GetCataloge2()
        {
            isExpired = _tokenBuilder.isExpiredToken();
            if (isExpired == false)
            {
                var result = from c in _context.Cataloges
                             where c.isDeleted == false
                             orderby c.name
                             select new
                             {
                                 c.id,
                                 c.name,
                                 shopsId = c.Shops.id,
                                 shopsName = c.Shops.name
                             };
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
                var result = from c in _context.Cataloges
                             where c.isDeleted == false && c.ShopsId == ShopId
                             orderby c.name
                             select new
                             {
                                 c.id,
                                 c.name,
                                 shopsId = c.Shops.id,
                                 shopsName = c.Shops.name
                             };
                return Ok(result);
            }
            else
                return Unauthorized();
        }

        // GET: api/CatalogesAPI/5
        [HttpGet("{id}")]
        public IActionResult GetCatalogesById(int id)
        {
            isExpired = _tokenBuilder.isExpiredToken();
            if (isExpired == false)
            {
                var result = from c in _context.Cataloges
                             where c.id == id
                             select new
                             {
                                 c.id,
                                 c.name,
                                 shopsName = c.Shops.name
                             };
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

                var catalogesOld = _context.Cataloges.Find(id);
                catalogesOld.updated_at = DateTime.Now;
                var user = HttpContext.Session.GetObjectFromJson<Users>("user");
                catalogesOld.updated_by = user.username;
                catalogesOld.name = cataloges.name;
                _context.Entry(catalogesOld).State = EntityState.Modified;


                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CatalogesExists(id))
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
                _context.Cataloges.Add(cataloges);
                await _context.SaveChangesAsync();

                return Ok(cataloges.id);
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
                cataloges.deleted_by = user.username;
                cataloges.deleted_at = DateTime.Now;
                cataloges.isDeleted = true;
                _context.Entry(cataloges).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                return cataloges;
            }
            else
                return Unauthorized();
        }

        private bool CatalogesExists(int id)
        {
            return _context.Cataloges.Any(e => e.id == id);
        }
    }
}
