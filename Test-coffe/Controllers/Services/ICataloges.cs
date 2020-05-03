using Test_coffe.Models;

namespace Test_coffe.Controllers.Services
{
    public interface ICataloges
    {
        dynamic GetAllCataloges();

        void CreateCataloges(Cataloges Cataloges);

        void UpdateCataloges(int id, Cataloges Cataloges);

        void RemoveCataloges(int id, string username);

        dynamic GetAllCatalogesByShop(int? shopsId);

    }
}
