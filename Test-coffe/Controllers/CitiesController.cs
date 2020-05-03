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
    public class CitiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CitiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Cities
        //public async Task<IActionResult> Index()
        //{
        //    Console.WriteLine("====        " + HttpContext.Session.GetString("email"));
        //    Console.WriteLine("====        " + HttpContext.Session.GetString("name"));
        //    //            select* From PermissionDetail p Join[User] u on p.UserId = u.id
        //    //Where p.PermissionId = 1 AND u.username = 'dan8@gmail.com'
        //    var username = HttpContext.Session.GetString("username");
        //    Console.WriteLine(username);
        //    if (username != null)
        //        return View(await _context.Cities.ToListAsync());
        //    else
        //        return RedirectToAction("Index", "Home");
        //}

        public async Task<IActionResult> Index()
        {
            var user = HttpContext.Session.GetObjectFromJson<Users>("user");
            if (user != null)
                return View(await _context.Cities.ToListAsync());
            else
                return RedirectToAction("Index", "Login");


            //return View(await _context.Cities.ToListAsync());
        }

        // GET: Cities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cities = await _context.Cities
                .FirstOrDefaultAsync(m => m.id == id);
            if (cities == null)
            {
                return NotFound();
            }

            return View(cities);
        }

        // GET: Cities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,name,permalink,isDeleted,deleted_at,deleted_by,created_at,created_by,updated_at,updated_by")] Cities cities)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cities);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cities);
        }

        // GET: Cities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cities = await _context.Cities.FindAsync(id);
            if (cities == null)
            {
                return NotFound();
            }
            return View(cities);
        }

        // POST: Cities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,name,permalink,isDeleted,deleted_at,deleted_by,created_at,created_by,updated_at,updated_by")] Cities cities)
        {
            if (id != cities.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cities);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CityExists(cities.id))
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
            return View(cities);
        }

        // GET: Cities/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var cities = await _context.Cities
        //        .FirstOrDefaultAsync(m => m.id == id);
        //    if (cities == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(cities);
        //}

        // POST: Cities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cities = await _context.Cities.FindAsync(id);
            _context.Cities.Remove(cities);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CityExists(int id)
        {
            return _context.Cities.Any(e => e.id == id);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            var cities = await _context.Cities.FindAsync(id);
            _context.Cities.Remove(cities);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
