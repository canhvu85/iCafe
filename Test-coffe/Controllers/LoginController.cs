using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcHaack.Ajax;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Test_coffe.Controllers.Services;
using Test_coffe.Models;

namespace Test_coffe.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ITokenBuilder _tokenBuilder;

        public LoginController(ApplicationDbContext context, ITokenBuilder tokenBuilder)
        {
            _context = context;
            _tokenBuilder = tokenBuilder;
        }

        public async Task<IActionResult> Index()
        {
            //Console.WriteLine("====        " + HttpContext.Session.GetString("emailFB"));
            //Console.WriteLine("====        " + HttpContext.Session.GetString("nameFB"));
            //Console.WriteLine("====        " + HttpContext.Session.GetString("emailGoogle"));
            //Console.WriteLine("====        " + HttpContext.Session.GetString("nameGoogle"));
            //Console.WriteLine("====        " + HttpContext.Session.GetString("username"));
            //Console.WriteLine("====        " + HttpContext.Session.GetString("ShopId"));
            return View();
        }

        // POST: Cities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("username,password")] Users users)
        {

            if (ModelState.IsValid)
            {
                if (UserExists2(users.username, users.password))
                {
                    HttpContext.Session.SetString("username", users.username);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Đăng Nhập Không Đúng");
                    TempData["Message"] = "Invalid";
                    return RedirectToAction(nameof(Index));
                }
            }
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists2(string username, string password)
        {
            return _context.Users.Any(e => e.username == username && e.password == password);
        }

        [HttpPost, ValidateJsonAntiForgeryToken]
        public JsonResult FacebookLogin(string name, string email)
        {
            Console.WriteLine(email);
            Console.WriteLine(name);

            HttpContext.Session.SetString("emailFB", email);
            HttpContext.Session.SetString("nameFB", name);


            return Json(new { success = "True" });

        }

        [HttpPost, ValidateJsonAntiForgeryToken]
        public JsonResult FacebookLogOut()
        {

            HttpContext.Session.Clear();
            Console.WriteLine("logout====        " + HttpContext.Session.GetString("emailFB"));
            Console.WriteLine("logout====        " + HttpContext.Session.GetString("nameFB"));
            return Json(new { success = "True" });

        }

        [HttpPost, ValidateJsonAntiForgeryToken]
        public JsonResult GoogleLogin(string name, string email)
        {
            Console.WriteLine(email);
            Console.WriteLine(name);

            HttpContext.Session.SetString("emailGoogle", email);
            HttpContext.Session.SetString("nameGoogle", name);

            return Json(new { success = "True" });

        }

        [HttpPost, ValidateJsonAntiForgeryToken]
        public JsonResult GoogleLogOut()
        {

            HttpContext.Session.Clear();
            Console.WriteLine("logout====        " + HttpContext.Session.GetString("emailGoogle"));
            Console.WriteLine("logout====        " + HttpContext.Session.GetString("nameGoogle"));
            return Json(new { success = "True" });

        }

        public async Task<IActionResult> Test()
        {
            Console.WriteLine("====        " + HttpContext.Session.GetString("emailFB"));
            Console.WriteLine("====        " + HttpContext.Session.GetString("nameFB"));
            return View();
        }

        //[HttpPost]
        //public IActionResult LoginForm([FromBody] Users users)
        //{
        //    var dateNow = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
        //    var result = from u in _context.Users
        //                 where u.username == users.username &&
        //                 u.password == users.password &&
        //                 u.ShopsId == users.ShopsId &&
        //                 u.Shops.time_open <= dateNow &&
        //                 u.Shops.time_close >= dateNow &&
        //                 u.isDeleted == false
        //                 select new
        //                 {
        //                     u.id,
        //                     u.name,
        //                     u.images,
        //                     u.username,
        //                     u.password,
        //                     u.permalink,
        //                     positionsId = u.PositionsId,
        //                     shopsId = u.ShopsId
        //                 };
        //    //HttpContext.Session.SetString("username", users.username);
        //    var us = new Users();
        //    us.username = users.username;
        //    us.ShopsId = users.ShopsId;
        //    us.PositionsId = users.PositionsId;
        //    HttpContext.Session.SetObjectAsJson("user", us);
        //    return Ok(result);
        //}

        [HttpPost]
        public IActionResult LoginForm([FromBody] Users users)
        {
            if (ModelState.IsValid)
            {
                if (!_context.Users.Any(u => u.username == users.username && u.password == users.password && u.ShopsId == users.ShopsId))
                {
                    return Content("Tài khoản hoặc mật khẩu không đúng !");
                }
                else
                {
                    var dateNow = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
                    var result = from u in _context.Users
                                 where u.username == users.username &&
                                 u.password == users.password &&
                                 u.ShopsId == users.ShopsId &&
                                 u.Shops.time_open <= dateNow &&
                                 u.Shops.time_close >= dateNow &&
                                 u.isDeleted == false
                                 select new
                                 {
                                     u.id,
                                     u.name,
                                     u.username,
                                     u.PositionsId,
                                     u.ShopsId
                                 };

                    if (result.Count() > 0)
                    {
                        var userData = _context.Users.Find(result.First().id);
                        var remember_token = _tokenBuilder.BuildToken(userData);

                        userData.remember_token = remember_token;
                        _context.Update(userData);
                        _context.SaveChangesAsync();

                        var us = new Users();
                        us.id = userData.id;
                        us.username = userData.username;
                        us.ShopsId = userData.ShopsId;
                        us.PositionsId = userData.PositionsId;
                        us.remember_token = remember_token;
                        HttpContext.Session.SetObjectAsJson("user", us);
                        //return Ok(token);

                        return CreatedAtAction("GetUser", result);
                    }
                    else
                        return StatusCode(202, "Tài khoản hết hạn !");
                }
            }
            else
            {
                return Content("Vui lòng kiểm tra lại thông tin");
            }
        }


        //[HttpPost]
        //public async Task<IActionResult> RegisterForm([FromBody] Users users)
        //{
        //    Console.WriteLine(users.username);
        //    users.PositionsId = 6;
        //    _context.Add(users);
        //    await _context.SaveChangesAsync();
        //    //return RedirectToAction("Index", "Login");
        //    return RedirectToAction(nameof(Index));
        //}

        [HttpPost]
        public async Task<ActionResult<Users>> RegisterForm([FromBody] Users users)
        {
            if (ModelState.IsValid)
            {
                if (!_context.Users.Any(u => u.username == users.username))
                {
                    users.PositionsId = 1;
                    _context.Users.Add(users);
                    await _context.SaveChangesAsync();
                    return CreatedAtAction("GetUser", new { id = users.id }, users);
                }
                else
                {
                    return Content("Tên này đã tồn tại trong hệ thống !");
                }
            }
            else
            {
                //ViewBag.Message = "Vui lòng kiểm tra lại thông tin";
                //return BadRequest();
                return Content("Vui lòng kiểm tra lại thông tin");
            }
        }

        [HttpPost]
        public async Task<IActionResult> LogOut([FromBody] Users users)
        {
            var userOld = _context.Users.Find(users.id);
            userOld.updated_at = DateTime.Now;
            userOld.updated_by = users.updated_by;
            userOld.remember_token = null;

            HttpContext.Session.Clear();
            _context.Update(userOld);
            _context.SaveChangesAsync();
            return NoContent();
        }
    }
}