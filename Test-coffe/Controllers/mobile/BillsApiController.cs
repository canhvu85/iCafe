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
    public class BillsApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
       
        public BillsApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/BillsApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bills>>> GetBill(int? table_id)
        {
            var result = await _context.Bills.Where(b => b.status == 0 && b.TablesId == table_id).ToListAsync();
            if (result.FirstOrDefault() == null)
            {
                return NotFound();
            }
            return result;
        }

        // GET: api/BillsApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Bills>> GetBill(int id)
        {
            var bill = await _context.Bills.FindAsync(id);

            if (bill == null)
            {
                return NotFound();
            }

            return bill;
        }

        // PUT: api/BillsApi/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBill(int id, Bills bill)
        {
            if (id != bill.id)
            {
                return BadRequest();
            }

            _context.Entry(bill).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BillExists(id))
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

        // POST: api/BillsApi
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Bills>> PostBill(Bills bill)
        {
            _context.Bills.Add(bill);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBill", new { id = bill.id }, bill);
        }

        // DELETE: api/BillsApi/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Bills>> DeleteBill(int id)
        {
            var bill = await _context.Bills.FindAsync(id);
            if (bill == null)
            {
                return NotFound();
            }

            _context.Bills.Remove(bill);
            await _context.SaveChangesAsync();

            return bill;
        }

        private bool BillExists(int id)
        {
            return _context.Bills.Any(e => e.id == id);
        }
    }
}
