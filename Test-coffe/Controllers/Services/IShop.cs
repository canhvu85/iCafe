namespace Test_coffe.Controllers.Services
{
    public interface IShop
    {
        dynamic GetShop();

        dynamic GetShopByCities(int? citiesId);
    }
}
