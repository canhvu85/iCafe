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
    public class TypeMoneysController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TypeMoneysController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TypeMoneys
        public async Task<IActionResult> Index()
        {
            return View(await _context.TypeMoneys.ToListAsync());
        }

        // GET: TypeMoneys/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeMoneys = await _context.TypeMoneys
                .FirstOrDefaultAsync(m => m.id == id);
            if (typeMoneys == null)
            {
                return NotFound();
            }

            return View(typeMoneys);
        }

        // GET: TypeMoneys/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TypeMoneys/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,name,quantity,isDeleted,deleted_at,deleted_by,created_at,created_by,updated_at,updated_by")] TypeMoneys typeMoneys)
        {
            if (ModelState.IsValid)
            {
                _context.Add(typeMoneys);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(typeMoneys);
        }

        // GET: TypeMoneys/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeMoneys = await _context.TypeMoneys.FindAsync(id);
            if (typeMoneys == null)
            {
                return NotFound();
            }
            return View(typeMoneys);
        }

        // POST: TypeMoneys/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,name,quantity,isDeleted,deleted_at,deleted_by,created_at,created_by,updated_at,updated_by")] TypeMoneys typeMoneys)
        {
            if (id != typeMoneys.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(typeMoneys);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TypeMoneysExists(typeMoneys.id))
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
            return View(typeMoneys);
        }

        // GET: TypeMoneys/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeMoneys = await _context.TypeMoneys
                .FirstOrDefaultAsync(m => m.id == id);
            if (typeMoneys == null)
            {
                return NotFound();
            }

            return View(typeMoneys);
        }

        // POST: TypeMoneys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var typeMoneys = await _context.TypeMoneys.FindAsync(id);
            _context.TypeMoneys.Remove(typeMoneys);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TypeMoneysExists(int id)
        {
            return _context.TypeMoneys.Any(e => e.id == id);
        }
    }
}
