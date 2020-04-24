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
    public class FloorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FloorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Floors
        public async Task<IActionResult> Index()
        {
            return View(await _context.Floors.ToListAsync());
        }

        // GET: Floors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var floors = await _context.Floors
                .FirstOrDefaultAsync(m => m.id == id);
            if (floors == null)
            {
                return NotFound();
            }

            return View(floors);
        }

        // GET: Floors/Create
        public IActionResult Create()
        {
            ViewData["ShopsId"] = new SelectList(_context.Shops, "id", "id");
            return View();
        }

        // POST: Floors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,name,permalink,ShopsId,isDeleted,deleted_at,deleted_by,created_at,created_by,updated_at,updated_by")] Floors floors)
        {
            if (ModelState.IsValid)
            {
                _context.Add(floors);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(floors);
        }

        // GET: Floors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var floors = await _context.Floors.FindAsync(id);
            if (floors == null)
            {
                return NotFound();
            }
            return View(floors);
        }

        // POST: Floors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,name,permalink,ShopsId,isDeleted,deleted_at,deleted_by,created_at,created_by,updated_at,updated_by")] Floors floors)
        {
            if (id != floors.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(floors);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FloorsExists(floors.id))
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
            return View(floors);
        }

        // GET: Floors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var floors = await _context.Floors
                .FirstOrDefaultAsync(m => m.id == id);
            if (floors == null)
            {
                return NotFound();
            }

            return View(floors);
        }

        // POST: Floors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var floors = await _context.Floors.FindAsync(id);
            _context.Floors.Remove(floors);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FloorsExists(int id)
        {
            return _context.Floors.Any(e => e.id == id);
        }
    }
}
