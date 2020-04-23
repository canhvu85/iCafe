using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Test_coffe.Models;

namespace Test_coffe.Controllers.mobile
{
    public class MobileController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MobileController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult LoginNew()
        {
          //  var applicationDbContext = _context.Shop.Include(s => s.City);
           // ViewData["CityId"] = new SelectList(_context.Cities, "id", "name");
           // ViewData["ShopId"] = new SelectList(_context.Shop.Include(s => s.City), "id", "name");
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult Account()
        {
            return View();
        }
        public IActionResult Cart()
        {
            return View();
        }
    }
}