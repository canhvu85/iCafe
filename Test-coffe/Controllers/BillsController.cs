using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Test_coffe.Models;

namespace Test_coffe.Controllers
{
    public class BillsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BillsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Bills
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Bills.Include(b => b.Tables);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Bills/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bills = await _context.Bills
                .Include(b => b.Tables)
                .FirstOrDefaultAsync(m => m.id == id);
            if (bills == null)
            {
                return NotFound();
            }

            return View(bills);
        }

        // GET: Bills/Create
        public IActionResult Create()
        {
            ViewData["TablesId"] = new SelectList(_context.Tables, "id", "id");
            return View();
        }

        // POST: Bills/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,time_enter,time_out,permalink,status,sub_total,fee_service,total_money,user_name,TablesId,isDeleted,deleted_at,deleted_by,created_at,created_by,updated_at,updated_by")] Bills bills)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bills);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TablesId"] = new SelectList(_context.Tables, "id", "id", bills.TablesId);
            return View(bills);
        }

        // GET: Bills/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bills = await _context.Bills.FindAsync(id);
            if (bills == null)
            {
                return NotFound();
            }
            ViewData["TablesId"] = new SelectList(_context.Tables, "id", "id", bills.TablesId);
            return View(bills);
        }

        // POST: Bills/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,time_enter,time_out,permalink,status,sub_total,fee_service,total_money,user_name,TablesId,isDeleted,deleted_at,deleted_by,created_at,created_by,updated_at,updated_by")] Bills bills)
        {
            if (id != bills.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bills);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BillExists(bills.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["TablesId"] = new SelectList(_context.Tables, "id", "id", bills.TablesId);
            return View(bills);
        }

        // GET: Bills/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bills = await _context.Bills
                .Include(b => b.Tables)
                .FirstOrDefaultAsync(m => m.id == id);
            if (bills == null)
            {
                return NotFound();
            }

            return View(bills);
        }

        // POST: Bills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bills = await _context.Bills.FindAsync(id);
            _context.Bills.Remove(bills);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BillExists(int id)
        {
            return _context.Bills.Any(e => e.id == id);
        }
    }
}
