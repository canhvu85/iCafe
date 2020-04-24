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
    public class ShopsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ShopsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Shops
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Shops.Include(s => s.Cities);
            ViewData["CitiesId"] = new SelectList(_context.Cities, "id", "name");
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Shops/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shops = await _context.Shops
                .Include(s => s.Cities)
                .FirstOrDefaultAsync(m => m.id == id);
            if (shops == null)
            {
                return NotFound();
            }

            return View(shops);
        }

        // GET: Shops/Create
        public IActionResult Create()
        {
            ViewData["CitiesId"] = new SelectList(_context.Cities, "id", "id");
            return View();
        }

        // POST: Shops/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,name,info,avatar,thumb,time_open,time_close,status,permalink,CitiesId,isDeleted,deleted_at,deleted_by,created_at,created_by,updated_at,updated_by")] Shops shops)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shops);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CitiesId"] = new SelectList(_context.Cities, "id", "id", shops.CitiesId);
            return View(shops);
        }

        // GET: Shops/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shops = await _context.Shops.FindAsync(id);
            if (shops == null)
            {
                return NotFound();
            }
            ViewData["CitiesId"] = new SelectList(_context.Cities, "id", "id", shops.CitiesId);
            return View(shops);
        }

        // POST: Shops/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,name,info,avatar,thumb,time_open,time_close,status,permalink,CitiesId,isDeleted,deleted_at,deleted_by,created_at,created_by,updated_at,updated_by")] Shops shops)
        {
            if (id != shops.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shops);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShopExists(shops.id))
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
            ViewData["CitiesId"] = new SelectList(_context.Cities, "id", "id", shops.CitiesId);
            return View(shops);
        }

        // GET: Shops/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shops = await _context.Shops
                .Include(s => s.Cities)
                .FirstOrDefaultAsync(m => m.id == id);
            if (shops == null)
            {
                return NotFound();
            }

            return View(shops);
        }

        // POST: Shops/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shops = await _context.Shops.FindAsync(id);
            _context.Shops.Remove(shops);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShopExists(int id)
        {
            return _context.Shops.Any(e => e.id == id);
        }
    }
}
