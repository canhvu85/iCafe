//using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Test_coffe.Models;

namespace Test_coffe.Controllers
{
    public class TablesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TablesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Tables
        public async Task<IActionResult> Index()
        {
            // var applicationDbContext = _context.Tables.Include(t => t.Floors);
            //return View(await applicationDbContext.ToListAsync());

            var user = HttpContext.Session.GetObjectFromJson<Users>("user");
            if (user != null)
            {
                return View();
            }
            else
                return RedirectToAction("Index", "Login");
        }

        // GET: Tables/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tables = await _context.Tables
                .Include(t => t.Floors)
                .FirstOrDefaultAsync(m => m.id == id);
            if (tables == null)
            {
                return NotFound();
            }

            return View(tables);
        }

        // GET: Tables/Create
        public IActionResult Create()
        {
            ViewData["FloorsId"] = new SelectList(_context.Floors, "id", "id");
            return View();
        }

        // POST: Tables/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,name,status,permalink,FloorsId,isDeleted,deleted_at,deleted_by,created_at,created_by,updated_at,updated_by")] Tables tables)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tables);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FloorsId"] = new SelectList(_context.Floors, "id", "id", tables.FloorsId);
            return View(tables);
        }

        // GET: Tables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tables = await _context.Tables.FindAsync(id);
            if (tables == null)
            {
                return NotFound();
            }
            ViewData["FloorsId"] = new SelectList(_context.Floors, "id", "id", tables.FloorsId);
            return View(tables);
        }

        // POST: Tables/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,name,status,permalink,FloorsId,isDeleted,deleted_at,deleted_by,created_at,created_by,updated_at,updated_by")] Tables tables)
        {
            if (id != tables.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tables);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TableExists(tables.id))
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
            ViewData["FloorsId"] = new SelectList(_context.Floors, "id", "id", tables.FloorsId);
            return View(tables);
        }

        // GET: Tables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tables = await _context.Tables
                .Include(t => t.Floors)
                .FirstOrDefaultAsync(m => m.id == id);
            if (tables == null)
            {
                return NotFound();
            }

            return View(tables);
        }

        // POST: Tables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tables = await _context.Tables.FindAsync(id);
            _context.Tables.Remove(tables);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TableExists(int id)
        {
            return _context.Tables.Any(e => e.id == id);
        }
    }
}
