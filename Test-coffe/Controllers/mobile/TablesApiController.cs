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
    public class TablesApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TablesApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/TablesApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tables>>> GetTable(int? shop_id)
        {
            return await _context.Tables.Where(t => t.Floors.ShopsId == shop_id && t.isDeleted == false).ToListAsync();
        }

        [HttpGet("floor")]
        public async Task<ActionResult<IEnumerable<Tables>>> GetTableByFloor(int? floor_id)
        {
            if (floor_id != null)
            {
                return await _context.Tables.Include(t=>t.Floors).Where(t => t.Floors.id == floor_id && t.isDeleted == false).ToListAsync();
            }
            else
                return NoContent();
        }

        // GET: api/TablesApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tables>> GetTable(int id)
        {
            var table = await _context.Tables.FindAsync(id);

            if (table == null)
            {
                return NotFound();
            }

            return table;
        }

        // PUT: api/TablesApi/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTable(int id, Tables table)
        {
            if (id != table.id)
            {
                return BadRequest();
            }

            var tableOld = _context.Tables.Where(t => t.permalink == table.permalink && t.FloorsId == table.FloorsId && t.isDeleted == false).ToList().FirstOrDefault();
            if (tableOld != null && tableOld.id != table.id)
            {
                return Content("Bàn này đã có, hãy nhập tên khác");
            }
            else
            {

                tableOld = _context.Tables.Find(id);

                tableOld.status = table.status;
                if (table.name != null)
                {
                    var tableOld1 = _context.Tables.Where(t => t.permalink == table.permalink
                                                            && t.id != table.id
                                                            && t.FloorsId == table.FloorsId
                                                            && t.isDeleted == false).ToList().FirstOrDefault();
                    if (tableOld1 != null)
                    {
                        tableOld.permalink = table.permalink + "_1";
                    }
                    else
                        tableOld.permalink = table.permalink;

                    tableOld.name = table.name;
                    tableOld.FloorsId = table.FloorsId;
                }
                // tableOld.updated_at = DateTime.Now;          
                _context.Entry(tableOld).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TableExists(id))
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
        }


        [HttpPut("del/{id}")]
        public IActionResult SoftDeleteTable(int id, String name)
        {
          
            var tableOld = _context.Tables.Find(id);
            tableOld.isDeleted = true;
            tableOld.deleted_at = DateTime.Now;
            tableOld.deleted_by = name;
           
            using (var db = _context)
            {
                //db.Users.Attach(user);
                db.Tables.Attach(tableOld);
                db.Entry(tableOld).Property(n => n.isDeleted).IsModified = true;
                db.Entry(tableOld).Property(i => i.deleted_at).IsModified = true;
                db.Entry(tableOld).Property(c => c.deleted_by).IsModified = true;
                db.SaveChanges();
            }

            return NoContent();
        }

        // POST: api/TablesApi
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Tables>> PostTable(Tables table)
        {
            var tableOld = _context.Tables.Where(t => t.permalink == table.permalink && t.FloorsId == table.FloorsId && t.isDeleted == false).ToList().FirstOrDefault();
            if (tableOld != null)
            {
                return Content("Bàn này đã có, hãy nhập tên khác");
            }
            else
            {
                var tableOld1 = _context.Tables.Where(t => t.permalink == table.permalink
                                                        && t.id != table.id
                                                        && t.FloorsId == table.FloorsId
                                                        && t.isDeleted == false).ToList().FirstOrDefault();
                if (tableOld1 != null)
                {
                    table.permalink += "_1";
                }
                _context.Tables.Add(table);
                await _context.SaveChangesAsync();


                return CreatedAtAction("GetTable", new { id = table.id }, table);
            }
        }

        // DELETE: api/TablesApi/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Tables>> DeleteTable(int id)
        {
            var table = await _context.Tables.FindAsync(id);
            if (table == null)
            {
                return NotFound();
            }

            _context.Tables.Remove(table);
            await _context.SaveChangesAsync();

            return table;
        }

        private bool TableExists(int id)
        {
            return _context.Tables.Any(e => e.id == id);
        }
    }
}
