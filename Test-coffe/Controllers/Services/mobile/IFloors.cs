using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test_coffe.Models;

namespace Test_coffe.Controllers.mobile.Services
{
    public interface IFloors
    {
        dynamic GetAllFloors(int? shop_id);

        void CreateFloor(Floors floor);

        void UpdateFloor(int id, Floors floor);

        void RemoveFloor(int id, string username);
    }
}
