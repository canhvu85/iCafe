using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Test_coffe.Models;

namespace Test_coffe.Controllers
{
    [Route("admin/[controller]")]
    public class CitiesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private string route;
        private string method;
        private Users user;

        public CitiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            route = Request.Path.Value;
            method = Request.Method;
            user = HttpContext.Session.GetObjectFromJson<Users>("user");
            if (user != null &&
                _context.PermissionDetails.Any(pd => pd.UsersId == user.id &&
                pd.permalink_permissions == route && pd.action == method))
            {
                return View();
            }
            else
            {
                return Redirect("/");
            }

            //return Redirect("/");
            //return RedirectToAction("Index", "Login");
            //return View(await _context.Cities.ToListAsync());
        }

    }
}
