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
    public class TablesAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TablesAPIController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/TablesAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tables>>> GetTable2(int? shop_id)
        {
            //var user = HttpContext.Session.GetObjectFromJson<Users>("user");
            //string token = HttpContext.Request.Headers["Authorization"];

            //if (_context.Users.Any(u => u.username == user.username && u.token == token))
            //{
            //    Console.WriteLine(token);
            //    return await _context.Tables.Where(t => t.Floors.ShopsId == shop_id).ToListAsync();
            //}
            //else
            //{
            //    Console.WriteLine(token);
            //    //return StatusCode(401);
            //    return Unauthorized();
            //}

            return await _context.Tables.Where(t => t.Floors.ShopsId == shop_id).ToListAsync();
        }

        // GET: api/TablesAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tables>> GetTable(int id)
        {
            var tables = await _context.Tables.FindAsync(id);

            if (tables == null)
            {
                return NotFound();
            }

            return tables;
        }

        [HttpGet("status/{id}")]
        public IActionResult GetTableStatus(int id)
        {
            var result = (from t in _context.Tables
                         where t.isDeleted == false && t.id == id
                         select new
                         {
                             status = t.status
                         }).FirstOrDefault();

            if (result == null)
            {
                return NotFound();

            }
            return Ok(result);
        }

        // PUT: api/TablesAPI/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTable(int id, Tables tables)
        {
            if (id != tables.id)
            {
                return BadRequest();
            }
            var tableOld = _context.Tables.Find(id);
            tableOld.status = tables.status;
            tableOld.updated_at = DateTime.Now;
            tableOld.updated_by = tables.updated_by;

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

        // POST: api/TablesAPI
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Tables>> PostTable(Tables tables)
        {
            _context.Tables.Add(tables);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTable", new { id = tables.id }, tables);
        }

        // DELETE: api/TablesAPI/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Tables>> DeleteTable(int id)
        {
            var tables = await _context.Tables.FindAsync(id);
            if (tables == null)
            {
                return NotFound();
            }

            _context.Tables.Remove(tables);
            await _context.SaveChangesAsync();

            return tables;
        }

        private bool TableExists(int id)
        {
            return _context.Tables.Any(e => e.id == id);
        }
    }
}
