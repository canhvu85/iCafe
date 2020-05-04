using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test_coffe.Models;

namespace Test_coffe.Controllers.mobile.Services
{
    public interface ITablesMobile
    {
        dynamic GetAllTablesShop(int? shop_id);

        dynamic GetAllTablesFloor(int? floor_id);

        void CreateTable(Tables table);

        void UpdateTable(int id, Tables table);

        void RemoveTable(int id, string username);
    }
}
