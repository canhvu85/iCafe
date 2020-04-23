using LazZiya.ImageResize;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Test_coffe.Models;

namespace Test_coffe.Controllers
{
    [Route("api/mobile/[controller]")]
    [ApiController]
    public class CatalogesApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;

        public CatalogesApiController(ApplicationDbContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            this._hostingEnvironment = hostingEnvironment;
        }

        // GET: api/CatalogesAPI
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Cataloges>>> GetCataloge()
        //{
        //    return await _context.Cataloges.ToListAsync();
        //}

        [HttpGet]
        public IActionResult GetCataloge(int? shop_id)
        {
            var result = from c in _context.Cataloges
                         join s in _context.Shops on c.ShopsId equals s.id
                         where s.isDeleted == false && c.ShopsId == shop_id
                         select new
                         {
                             id = c.id,
                             name = c.name,
                             permalink = c.permalink,
                             shop = c.Shops
                         };
            return Ok(result);
        }

        // GET: api/CatalogesAPI/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Cataloges>> GetCataloge(int id)
        //{
        //    var cataloge = await _context.Cataloges.FindAsync(id);

        //    if (cataloge == null)
        //    {
        //        return NotFound();
        //    }

        //    return cataloge;
        //}
        [HttpGet("{id}")]
        public IActionResult GetCataloge(int id)
        {
            var result = from c in _context.Cataloges
                         where c.id == id
                         select new
                         {
                             id = c.id,
                             name = c.name,
                             permalink = c.permalink,
                             shop = c.Shops
                         };
            return Ok(result);
        }

        // PUT: api/CatalogesAPI/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutCataloge(int id, Cataloges cataloge)
        //{
        //    if (id != cataloge.id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(cataloge).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!CatalogeExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCataloge(int id)
        {
            var cataloge = _context.Cataloges.Find(id);
            //var city = new City();
            cataloge.id = id;
            cataloge.name = HttpContext.Request.Form["name"];
            cataloge.permalink = HttpContext.Request.Form["permalink"];
            cataloge.ShopsId = int.Parse(HttpContext.Request.Form["shopId"]);
        
            cataloge.updated_at = DateTime.Now;
            cataloge.updated_by = "Đan";
            _context.Entry(cataloge).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetCity", new { id = cataloge.id });
        }

        // POST: api/CatalogesAPI
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        //[HttpPost]
        //public async Task<ActionResult<Cataloges>> PostCataloge(Cataloges cataloge)
        //{
        //    _context.Cataloges.Add(cataloge);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetCataloge", new { id = cataloge.id }, cataloge);
        //}

        [HttpPost]
        public async Task<ActionResult<Cataloges>> PostCataloge()
        {
            var cataloge = new Cataloges();
            cataloge.name = HttpContext.Request.Form["name"];
            cataloge.permalink = HttpContext.Request.Form["permalink"];
            cataloge.ShopsId = int.Parse(HttpContext.Request.Form["shopId"]);
            _context.Cataloges.Add(cataloge);
            await _context.SaveChangesAsync();

            var catalogeImg = _context.Cataloges.Find(cataloge.id);
            catalogeImg.created_by = "Đan";
           
            _context.Entry(catalogeImg).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetCity", new { id = cataloge.id }, new { cataloge.id});
        }

        // DELETE: api/CatalogesAPI/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Cataloges>> DeleteCataloge(int id)
        {
            var cataloge = await _context.Cataloges.FindAsync(id);
            if (cataloge == null)
            {
                return NotFound();
            }

            cataloge.deleted_by = "Đan";
            cataloge.deleted_at = DateTime.Now;
            cataloge.isDeleted = true;
            _context.Entry(cataloge).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return cataloge;
        }

        private bool CatalogeExists(int id)
        {
            return _context.Cataloges.Any(e => e.id == id);
        }
    }
}
