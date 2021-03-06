﻿using System;
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
    [Route("cp/[controller]")]
    public class CatalogesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CatalogesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var user = HttpContext.Session.GetObjectFromJson<Users>("user");
            if (user != null)
            {
                return View("~/Views/cp/Cataloges/Index.cshtml");
            }
            else
                return Redirect("/");

        }
    }
}
