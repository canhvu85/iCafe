using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Test_coffe.Models;

namespace Test_coffe.Controllers
{
    public class CatalogesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CatalogesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Cataloges
        //public async Task<IActionResult> Index()
        //{
        //    var applicationDbContext = _context.Cataloges.Include(c => c.Shops);
        //    return View(await applicationDbContext.ToListAsync());
        //}

        //public async Task<IActionResult> Index()
        //{
        //    var applicationDbContext = _context.Cataloges.Include(c => c.Shops);
        //    ViewData["ShopsId"] = new SelectList(_context.Shops, "id", "id");
        //    return View(await applicationDbContext.ToListAsync());
        //}

        public IActionResult Index()
        {
            var user = HttpContext.Session.GetObjectFromJson<Users>("user");
            if (user != null)
            {
                return View();
            }
            else
                return RedirectToAction("Index", "Login");

        }

        // GET: Cataloges/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cataloges = await _context.Cataloges
                .Include(c => c.Shops)
                .FirstOrDefaultAsync(m => m.id == id);
            if (cataloges == null)
            {
                return NotFound();
            }

            return View(cataloges);
        }

        // GET: Cataloges/Create
        public IActionResult Create()
        {
            ViewData["ShopsId"] = new SelectList(_context.Shops, "id", "id");
            return View();
        }

        // POST: Cataloges/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,name,image,permalink,ShopsId,isDeleted,deleted_at,deleted_by,created_at,created_by,updated_at,updated_by")] Cataloges cataloges)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cataloges);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ShopsId"] = new SelectList(_context.Shops, "id", "id", cataloges.ShopsId);
            return View(cataloges);
        }

        // GET: Cataloges/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cataloges = await _context.Cataloges.FindAsync(id);
            if (cataloges == null)
            {
                return NotFound();
            }
            ViewData["ShopsId"] = new SelectList(_context.Shops, "id", "id", cataloges.ShopsId);
            return View(cataloges);
        }

        // POST: Cataloges/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,name,image,permalink,ShopsId,isDeleted,deleted_at,deleted_by,created_at,created_by,updated_at,updated_by")] Cataloges cataloges)
        {
            if (id != cataloges.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cataloges);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CatalogeExists(cataloges.id))
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
            ViewData["ShopsId"] = new SelectList(_context.Shops, "id", "id", cataloges.ShopsId);
            return View(cataloges);
        }

        // GET: Cataloges/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cataloges = await _context.Cataloges
                .Include(c => c.Shops)
                .FirstOrDefaultAsync(m => m.id == id);
            if (cataloges == null)
            {
                return NotFound();
            }

            return View(cataloges);
        }

        // POST: Cataloges/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cataloges = await _context.Cataloges.FindAsync(id);
            _context.Cataloges.Remove(cataloges);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CatalogeExists(int id)
        {
            return _context.Cataloges.Any(e => e.id == id);
        }
    }
}
