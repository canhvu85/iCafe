using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test_coffe.Models;

namespace Test_coffe.Controllers.mobile.Services
{
    public interface IShops
    {
        IList GetAllShopsByCity(int? city_id);

        Object GetShop(int id);

        void CreateShops(Shops shop);

        void UpdateShops(int id, Shops shop);

        void RemoveShops(int id, string username);
    }
}
