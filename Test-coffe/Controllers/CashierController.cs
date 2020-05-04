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
        private readonly ApplicationDbContext _context;
        private string route;
        private string method;
        private Users user;

        public CashierController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            //var user = HttpContext.Session.GetObjectFromJson<Users>("user");
            //if (user != null)
            //    return View();
            //else
            //    return RedirectToAction("Index", "Login");
            route = Request.Path.Value;
            method = Request.Method;
            user = HttpContext.Session.GetObjectFromJson<Users>("user");
            if (user != null &&
                _context.PermissionDetails.Any(pd => pd.UsersId == user.id &&
                pd.permalink_permissions == route && pd.action == method))
                return View();
            else
            {
                HttpContext.Session.Clear();
                return Redirect("/");
            }
        }
    }
}