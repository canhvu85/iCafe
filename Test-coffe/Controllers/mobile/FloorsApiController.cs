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
    [Route("api/mobile/[controller]")]
    [ApiController]
    public class FloorsApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FloorsApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/FloorsApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Floors>>> GetFloors(int? shop_id)
        {
            return await _context.Floors.Where(f=>f.ShopsId==shop_id && f.isDeleted == false).ToListAsync();
        }

        // GET: api/FloorsApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Floors>> GetFloors(int id)
        {
            var floors = await _context.Floors.FindAsync(id);

            if (floors == null)
            {
                return NotFound();
            }

            return floors;
        }

        // PUT: api/FloorsApi/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFloors(int id, Floors floors)
        {
            if (id != floors.id)
            {
                return BadRequest();
            }

            var floorOld = _context.Floors.Find(id);
            if (floors.name != null)
            {
                floorOld.name = floors.name;
                floorOld.permalink = floors.permalink;
                
            }
            _context.Entry(floorOld).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FloorsExists(id))
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

        // POST: api/FloorsApi
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Floors>> PostFloors(Floors floors)
        {
            _context.Floors.Add(floors);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFloors", new { id = floors.id }, floors);
        }

        [HttpPut("del/{id}")]
        public IActionResult SoftDeleteFloor(int id, String name)
        {
            var table = _context.Tables.Where(t => t.FloorsId == id && t.isDeleted == false).ToList().FirstOrDefault();
            if (table != null)
            {
                return Content("Tầng này đang có bàn, không thể xóa!");
            }
            else
            {
                var floorOld = _context.Floors.Find(id);
                floorOld.isDeleted = true;
                floorOld.deleted_at = DateTime.Now;
                floorOld.deleted_by = name;

                using (var db = _context)
                {
                    //db.Users.Attach(user);
                    db.Floors.Attach(floorOld);
                    db.Entry(floorOld).Property(n => n.isDeleted).IsModified = true;
                    db.Entry(floorOld).Property(i => i.deleted_at).IsModified = true;
                    db.Entry(floorOld).Property(c => c.deleted_by).IsModified = true;
                    db.SaveChanges();
                }

                return NoContent();
            }
        }

        // DELETE: api/FloorsApi/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Floors>> DeleteFloors(int id)
        {
            var floors = await _context.Floors.FindAsync(id);
            if (floors == null)
            {
                return NotFound();
            }

            _context.Floors.Remove(floors);
            await _context.SaveChangesAsync();

            return floors;
        }

        private bool FloorsExists(int id)
        {
            return _context.Floors.Any(e => e.id == id);
        }
    }
}
