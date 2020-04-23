using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test_coffe.Models;

namespace Test_coffe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogesAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CatalogesAPIController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/CatalogesAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cataloges>>> GetCataloge2()
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

        [HttpGet("shop/{ShopId}")]
        public IActionResult GetCatalogesByShop(int? ShopId)
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

        // GET: api/CatalogesAPI/5
        [HttpGet("{id}")]
        public IActionResult GetCatalogesById(int id)
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

        // POST: api/CatalogesAPI
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Cataloges>> PostCataloges(Cataloges cataloges)
        {
            var user = HttpContext.Session.GetObjectFromJson<Users>("user");
            cataloges.created_by = user.username;
            _context.Cataloges.Add(cataloges);
            await _context.SaveChangesAsync();

            return Ok(cataloges.id);
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
            var user = HttpContext.Session.GetObjectFromJson<Users>("user");
            cataloges.deleted_by = user.username;
            cataloges.deleted_at = DateTime.Now;
            cataloges.isDeleted = true;
            _context.Entry(cataloges).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return cataloges;
        }

        private bool CatalogesExists(int id)
        {
            return _context.Cataloges.Any(e => e.id == id);
        }
    }
}
