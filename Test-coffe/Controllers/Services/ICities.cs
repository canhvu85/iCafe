using Test_coffe.Models;

namespace Test_coffe.Controllers.Services
{
    public interface ICities
    {
        dynamic GetAllCities();

        void CreateCities(Cities cities);

        void UpdateCities(int id, Cities cities);

        void RemoveCities(int id, string username);
    }
}
