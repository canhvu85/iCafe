using Test_coffe.Models;

namespace Test_coffe.Controllers.Services
{
    public interface ITables
    {
        dynamic GetAllTables(int? shopsId);

        void UpdateTables(int id, Tables tables);
    }
}
