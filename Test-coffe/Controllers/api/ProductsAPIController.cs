using Microsoft.AspNetCore.Mvc;
using Test_coffe.Controllers.Services;
using Test_coffe.Models;

namespace Test_coffe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IProduct _productRepository;
        private dynamic result;

        public ProductsAPIController(ApplicationDbContext context, IProduct productRepository)
        {
            _context = context;
            _productRepository = productRepository;
        }

        [HttpGet("shop/{ShopId}")]
        public IActionResult GetProduct(int? ShopId)
        {
            //var result = from p in _context.Products
            //             join c in _context.Cataloges on p.CatalogesId equals c.id
            //             where c.ShopsId == ShopId && p.isDeleted == false
            //             select new
            //             {
            //                 p.id,
            //                 p.name,
            //                 p.images,
            //                 p.price,
            //                 catalogesId = p.Cataloges.id
            //             };
            result = _productRepository.GetProduct(ShopId);
            return Ok(result);
        }
    }
}
