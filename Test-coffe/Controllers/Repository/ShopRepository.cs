using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test_coffe.Controllers.Services;
using Test_coffe.Models;

namespace Test_coffe.Controllers.Repository
{
    public class ShopRepository : IShop
    {
        private readonly ApplicationDbContext _context;

        public ShopRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        //public dynamic GetShop()
        //{
        //    return from s in _context.Shops
        //           where s.isDeleted == false
        //           orderby s.name
        //           select new
        //           {
        //               s.id,
        //               s.name,
        //               citiesId = s.CitiesId
        //           };
        //}

        public dynamic GetShopById(int? shopsId)
        {
            return from s in _context.Shops
                   where s.isDeleted == false && s.id == shopsId
                   select new
                   {
                       s.id,
                       s.name,
                       s.info,
                       s.CitiesId
                   };
        }

        public dynamic GetShopByCities(int? citiesId)
        {
            return from s in _context.Shops
                   where s.isDeleted == false && s.CitiesId == citiesId
                   orderby s.name
                   select new
                   {
                       s.id,
                       s.name,
                       s.info,
                       s.CitiesId
                   };
        }
    }
}
