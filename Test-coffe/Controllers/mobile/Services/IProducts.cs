using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test_coffe.Models;

namespace Test_coffe.Controllers.mobile.Services
{
   public interface IProducts
    {
        IEnumerable<Products> GetAllProductsByShop(int? shop_id);

        IEnumerable GetAllProductsByShopCp(int? shop_id);

        IList GetAllProductsByCataloge(int? cata_id);

        Object GetProduct(int id);

        Products CreateProducts(Products product);

        string UpdateProducts(int id, Products product);

        void RemoveProducts(int id, string username);
    }
}
