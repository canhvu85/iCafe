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
<<<<<<< HEAD
=======
        private readonly ITokenBuilder _tokenBuilder;
        private bool isExpired;
>>>>>>> 1e5fa3f4d55602f90e120414cf434886acc18128

        public TablesAPIController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/TablesAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tables>>> GetTable2(int? shop_id)
        {
<<<<<<< HEAD
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
=======
            isExpired = _tokenBuilder.isExpiredToken();
            if (isExpired == false)
                return await _context.Tables.Where(t => t.Floors.ShopsId == shop_id).ToListAsync();
            else
                return Unauthorized();
>>>>>>> 1e5fa3f4d55602f90e120414cf434886acc18128
        }

        // GET: api/TablesAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tables>> GetTable(int id)
        {
            isExpired = _tokenBuilder.isExpiredToken();
            if (isExpired == false)
            {
                var tables = await _context.Tables.FindAsync(id);

                if (tables == null)
                {
                    return NotFound();
                }

                return tables;
            }
            else
                return Unauthorized();
        }

        [HttpGet("status/{id}")]
        public IActionResult GetTableStatus(int id)
        {
<<<<<<< HEAD
            var result = (from t in _context.Tables
                         where t.isDeleted == false && t.id == id
                         select new
                         {
                             status = t.status
                         }).FirstOrDefault();

            if (result == null)
=======
            isExpired = _tokenBuilder.isExpiredToken();
            if (isExpired == false)
>>>>>>> 1e5fa3f4d55602f90e120414cf434886acc18128
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
            else
                return Unauthorized();
        }

        // PUT: api/TablesAPI/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTable(int id, Tables tables)
        {
            isExpired = _tokenBuilder.isExpiredToken();
            if (isExpired == false)
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
            else
                return Unauthorized();
        }

        // POST: api/TablesAPI
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Tables>> PostTable(Tables tables)
        {
            isExpired = _tokenBuilder.isExpiredToken();
            if (isExpired == false)
            {
                _context.Tables.Add(tables);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetTable", new { id = tables.id }, tables);
            }
            else
                return Unauthorized();
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
