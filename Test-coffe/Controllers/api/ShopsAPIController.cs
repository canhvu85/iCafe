using Microsoft.AspNetCore.Mvc;
using Test_coffe.Controllers.Services;
using Test_coffe.Models;

namespace Test_coffe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopsAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IShop _shopRepository;
        private dynamic result;

        public ShopsAPIController(ApplicationDbContext context, IShop shopRepository)
        {
            _context = context;
            _shopRepository = shopRepository;
        }

        [HttpGet]
        public IActionResult GetShop2(int? shopsId)
        {
            //var result = from s in _context.Shops
            //             where s.isDeleted == false
            //             orderby s.name
            //             select new
            //             {
            //                 id = s.id,
            //                 name = s.name,
            //                 citiesId = s.CitiesId
            //             };
            //result = _shopRepository.GetShop();
            //return Ok(result);

            var result = _shopRepository.GetShopById(shopsId);
            return Ok(result);
        }

        [HttpGet("cities/{citiesId}")]
        public IActionResult GetShopByCities(int? citiesId)
        {
            //var result = from s in _context.Shops
            //             where s.isDeleted == false && s.CitiesId == citiesId
            //             orderby s.name
            //             select new
            //             {
            //                 s.id,
            //                 s.name,
            //                 s.CitiesId
            //             };
            result = _shopRepository.GetShopByCities(citiesId);
            return Ok(result);
        }
    }
}
