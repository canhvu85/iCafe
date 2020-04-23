using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Test_coffe.Models;

namespace Test_coffe.Controllers
{
    public class CashierController : Controller
    {
        public IActionResult Index()
        {
            var user = HttpContext.Session.GetObjectFromJson<Users>("user");
            if (user != null)
                return View();
            else
                return RedirectToAction("Index", "Login");
        }
    }
}