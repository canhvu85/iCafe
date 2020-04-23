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
    public class StatisticalController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StatisticalController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var user = HttpContext.Session.GetObjectFromJson<Users>("user");
            if (user != null)
            {
                var result = _context.Bills.Include(t => t.Tables)
                    .Where(b => b.Tables.Floors.ShopsId == user.ShopsId)
                    .ToListAsync();
                return View(await result);
            }
            else
                return RedirectToAction("Index", "Login");
        }
    }
}