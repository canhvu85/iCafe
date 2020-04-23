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
    public class BillDetailsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BillDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BillDetails
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.BillDetails.Include(b => b.Bills).Include(b => b.Products);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: BillDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var billDetails = await _context.BillDetails
                .Include(b => b.Bills)
                .Include(b => b.Products)
                .FirstOrDefaultAsync(m => m.id == id);
            if (billDetails == null)
            {
                return NotFound();
            }

            return View(billDetails);
        }

        // GET: BillDetails/Create
        public IActionResult Create()
        {
            ViewData["BillsId"] = new SelectList(_context.Bills, "id", "id");
            ViewData["ProductsId"] = new SelectList(_context.Products, "id", "id");
            return View();
        }

        // POST: BillDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,price,quantity,total,permalink,status,user_name,ProductsId,BillsId,isDeleted,deleted_at,deleted_by,created_at,created_by,updated_at,updated_by")] BillDetails billDetails)
        {
            if (ModelState.IsValid)
            {
                _context.Add(billDetails);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BillsId"] = new SelectList(_context.Bills, "id", "id", billDetails.BillsId);
            ViewData["ProductsId"] = new SelectList(_context.Products, "id", "id", billDetails.ProductsId);
            return View(billDetails);
        }

        // GET: BillDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var billDetails = await _context.BillDetails.FindAsync(id);
            if (billDetails == null)
            {
                return NotFound();
            }
            ViewData["BillsId"] = new SelectList(_context.Bills, "id", "id", billDetails.BillsId);
            ViewData["ProductsId"] = new SelectList(_context.Products, "id", "id", billDetails.ProductsId);
            return View(billDetails);
        }

        // POST: BillDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,price,quantity,total,permalink,status,user_name,ProductsId,BillsId,isDeleted,deleted_at,deleted_by,created_at,created_by,updated_at,updated_by")] BillDetails billDetails)
        {
            if (id != billDetails.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(billDetails);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BillDetailExists(billDetails.id))
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
            ViewData["BillsId"] = new SelectList(_context.Bills, "id", "id", billDetails.BillsId);
            ViewData["ProductsId"] = new SelectList(_context.Products, "id", "id", billDetails.ProductsId);
            return View(billDetails);
        }

        // GET: BillDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var billDetails = await _context.BillDetails
                .Include(b => b.Bills)
                .Include(b => b.Products)
                .FirstOrDefaultAsync(m => m.id == id);
            if (billDetails == null)
            {
                return NotFound();
            }

            return View(billDetails);
        }

        // POST: BillDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var billDetails = await _context.BillDetails.FindAsync(id);
            _context.BillDetails.Remove(billDetails);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BillDetailExists(int id)
        {
            return _context.BillDetails.Any(e => e.id == id);
        }
    }
}
