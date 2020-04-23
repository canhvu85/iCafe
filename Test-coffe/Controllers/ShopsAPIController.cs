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
    public class ShopsAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ShopsAPIController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ShopsAPI
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Shops>>> GetShop()
        //{
        //    return await _context.Shops.OrderBy(s => s.name).ToListAsync();
        //}

        [HttpGet]
        public IActionResult GetShop2()
        {
            var result = from s in _context.Shops
                         where s.isDeleted == false
                         orderby s.name
                         select new
                         {
                             id = s.id,
                             name = s.name,
                             citiesId = s.CitiesId
                         };
            return Ok(result);
        }

        [HttpGet("cities/{citiesId}")]
        public IActionResult GetShopByCities(int? citiesId)
        {
            var result = from s in _context.Shops
                         where s.isDeleted == false && s.CitiesId == citiesId
                         orderby s.name
                         select new
                         {
                             s.id,
                             s.name,
                             s.CitiesId
                         };
            return Ok(result);
        }

        // GET: api/ShopsAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Shops>> GetShop(int id)
        {
            var shops = await _context.Shops.FindAsync(id);

            if (shops == null)
            {
                return NotFound();
            }

            return shops;
        }

        // PUT: api/ShopsAPI/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShop(int id, Shops shops)
        {
            if (id != shops.id)
            {
                return BadRequest();
            }

            _context.Entry(shops).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShopExists(id))
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

        // POST: api/ShopsAPI
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Shops>> PostShop(Shops shops)
        {
            _context.Shops.Add(shops);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetShop", new { id = shops.id }, shops);
        }

        // DELETE: api/ShopsAPI/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Shops>> DeleteShop(int id)
        {
            var shops = await _context.Shops.FindAsync(id);
            if (shops == null)
            {
                return NotFound();
            }

            _context.Shops.Remove(shops);
            await _context.SaveChangesAsync();

            return shops;
        }

        private bool ShopExists(int id)
        {
            return _context.Shops.Any(e => e.id == id);
        }
    }
}
