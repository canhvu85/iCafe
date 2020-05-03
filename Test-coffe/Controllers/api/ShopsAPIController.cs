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
    [Route("api/[controller]")]
    [ApiController]
    public class ShopsAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ShopsAPIController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ShopsAPI
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Shops>>> GetShop()
        //{
        //    return await _context.Shops.OrderBy(s => s.name).ToListAsync();
        //}

        [HttpGet]
        public IActionResult GetShop2()
        {
            var result = from s in _context.Shops
                         where s.isDeleted == false
                         orderby s.name
                         select new
                         {
                             id = s.id,
                             name = s.name,
                             citiesId = s.CitiesId
                         };
            return Ok(result);
        }

        [HttpGet("cities/{citiesId}")]
        public IActionResult GetShopByCities(int? citiesId)
        {
            var result = from s in _context.Shops
                         where s.isDeleted == false && s.CitiesId == citiesId
                         orderby s.name
                         select new
                         {
                             s.id,
                             s.name,
                             s.CitiesId
                         };
            return Ok(result);
        }
    }
}
