using System.Collections.Generic;
using System.Linq;
using Test_coffe.Controllers.Services;
using Test_coffe.Models;

namespace Test_coffe.Controllers.Repository
{
    public class ProductRepository : IProduct
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public dynamic GetProduct(int? ShopId)
        {
            return from p in _context.Products
                   join c in _context.Cataloges on p.CatalogesId equals c.id
                   where c.ShopsId == ShopId && p.isDeleted == false
                   select new
                   {
                       p.id,
                       p.name,
                       p.images,
                       p.price,
                       catalogesId = p.Cataloges.id
                   };
        }
    }
}
