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
    public class PermissionDetailsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PermissionDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PermissionDetails
        public async Task<IActionResult> Index()
        {
            return View(await _context.PermissionDetails.ToListAsync());
        }

        // GET: PermissionDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var permissionDetails = await _context.PermissionDetails
                .FirstOrDefaultAsync(m => m.id == id);
            if (permissionDetails == null)
            {
                return NotFound();
            }

            return View(permissionDetails);
        }

        // GET: PermissionDetails/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PermissionDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,PermissionsId,UsersId,isDeleted,deleted_at,deleted_by,created_at,created_by,updated_at,updated_by")] PermissionDetails permissionDetails)
        {
            if (ModelState.IsValid)
            {
                _context.Add(permissionDetails);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(permissionDetails);
        }

        // GET: PermissionDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var permissionDetails = await _context.PermissionDetails.FindAsync(id);
            if (permissionDetails == null)
            {
                return NotFound();
            }
            return View(permissionDetails);
        }

        // POST: PermissionDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,PermissionsId,UsersId,isDeleted,deleted_at,deleted_by,created_at,created_by,updated_at,updated_by")] PermissionDetails permissionDetails)
        {
            if (id != permissionDetails.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(permissionDetails);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PermissionDetailsExists(permissionDetails.id))
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
            return View(permissionDetails);
        }

        // GET: PermissionDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var permissionDetails = await _context.PermissionDetails
                .FirstOrDefaultAsync(m => m.id == id);
            if (permissionDetails == null)
            {
                return NotFound();
            }

            return View(permissionDetails);
        }

        // POST: PermissionDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var permissionDetails = await _context.PermissionDetails.FindAsync(id);
            _context.PermissionDetails.Remove(permissionDetails);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PermissionDetailsExists(int id)
        {
            return _context.PermissionDetails.Any(e => e.id == id);
        }
    }
}
