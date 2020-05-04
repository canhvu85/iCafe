//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Test_coffe.Models;

//namespace Test_coffe.Controllers
//{
//    //public class RegisterController : Controller
//    //{
//    //    public IActionResult Index()
//    //    {
//    //        return View();
//    //    }
//    //}

//    public class RegisterController : Controller
//    {
//        private readonly ApplicationDbContext _context;

//        public RegisterController(ApplicationDbContext context)
//        {
//            _context = context;
//        }

//        public async Task<IActionResult> Index()
//        {
//            return View();
//        }

//        // POST: Cities/Create
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Register([Bind("username,password,ShopsId")] Users users)
//        {
//            if (ModelState.IsValid)
//            {
//                users.PositionsId = 1;
//                _context.Add(users);
//                await _context.SaveChangesAsync();
//                return RedirectToAction("Index", "Login");
//            }
//            return RedirectToAction(nameof(Index));
//        }
//    }
//}