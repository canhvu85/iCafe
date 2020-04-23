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
    public class ProductsAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductsAPIController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ProductsAPI
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Products>>> GetProduct()
        //{
        //    return await _context.Products.ToListAsync();
        //}

        [HttpGet("shop/{ShopId}")]
        public IActionResult GetProduct(int? ShopId)
        {
            var result = from p in _context.Products
                         join c in _context.Cataloges on p.CatalogesId equals c.id
                         where c.ShopsId == ShopId && p.isDeleted == false
                         select new
                         {
                             p.id,
                             p.name,
                             p.images,
                             p.price,
                             catalogesId = p.Cataloges.id
                         };
            return Ok(result);
        }

        // GET: api/ProductsAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Products>> GetProduct(int id)
        {
            var products = await _context.Products.FindAsync(id);

            if (products == null)
            {
                return NotFound();
            }

            return products;
        }

        // PUT: api/ProductsAPI/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Products products)
        {
            if (id != products.id)
            {
                return BadRequest();
            }

            _context.Entry(products).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
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

        // POST: api/ProductsAPI
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Products>> PostProduct(Products products)
        {
            _context.Products.Add(products);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = products.id }, products);
        }

        // DELETE: api/ProductsAPI/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Products>> DeleteProduct(int id)
        {
            var products = await _context.Products.FindAsync(id);
            if (products == null)
            {
                return NotFound();
            }

            _context.Products.Remove(products);
            await _context.SaveChangesAsync();

            return products;
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.id == id);
        }
    }
}
