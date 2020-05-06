namespace Test_coffe.Controllers.Services
{
    public interface IShop
    {
        //dynamic GetShop();
        dynamic GetShopById(int? shopsId);

        dynamic GetShopByCities(int? citiesId);
    }
}
