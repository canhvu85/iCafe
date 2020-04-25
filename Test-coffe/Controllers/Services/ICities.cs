using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test_coffe.Models;

namespace Test_coffe.Controllers.Services
{
    public interface ICities
    {
        List<Cities> GetAllCities();

        void CreateCities(Cities cities);

        void UpdateCities(int id, Cities cities);

        void RemoveCities(int id, string username);
    }
}
