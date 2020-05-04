using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Test_coffe.Models;

namespace Test_coffe.Controllers
{
    public class ShopsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ShopsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
