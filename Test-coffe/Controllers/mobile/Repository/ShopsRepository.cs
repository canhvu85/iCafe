using Dapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test_coffe.Controllers.mobile.Services;
using Test_coffe.Controllers.Services;
using Test_coffe.Models;

namespace Test_coffe.Controllers.mobile.Repository
{
    public class ShopsRepository : IShops
    {
        private string get_Shops;
        private string get_Shops1;
        private string get_Shop;
        private string create_Shops;
        private string update_Shops;
        private string remove_Shops;

        public void CreateShops(Shops shop)
        {
           
        }

        public IList GetAllShopsByCity(int? city_id)
        {
            get_Shops = "SELECT s.[id],s.[name],s.[info],s.[images],s.[permalink],s.[status],s.[time_open],"
                        +"s.[time_close],s.[isDeleted],s.[deleted_at],s.[deleted_by],s.[created_at],"
                        + "s.[created_by],s.[updated_at],s.[updated_by],c.[id] [cityId],c.[name] [cityName]"
                        +" FROM [Shops] s JOIN [Cities] c ON s.CitiesId = c.id"
                        +" WHERE s.[isDeleted] = 0 AND s.CitiesId = @city_id  ORDER BY s.[name]";
            get_Shops1 = "SELECT s.[id],s.[name],s.[info],s.[images],s.[permalink],s.[status],s.[time_open],"
                        + "s.[time_close],s.[isDeleted],s.[deleted_at],s.[deleted_by],s.[created_at],"
                        + "s.[created_by],s.[updated_at],s.[updated_by],c.[id] [cityId],c.[name] [cityName]"
                        +" FROM [Shops] s JOIN [Cities] c"
                        +" ON s.CitiesId = c.id"
                        +" WHERE s.[isDeleted] = 0  ORDER BY s.[name]";
            if (city_id != null)
            {
                var query = SQLUtils.ExecuteCommand(SQLUtils._connStr,
                          conn => conn.Query(get_Shops, new { @city_id = city_id })).ToList();
                return query;
            }
            else
            {
                var query = SQLUtils.ExecuteCommand(SQLUtils._connStr,
                         conn => conn.Query(get_Shops1)).ToList();
                return query;
            }

        }

        public Object GetShop(int id)
        {
            get_Shop = "SELECT s.[id],s.[name],s.[info],s.[images],s.[permalink],s.[status],s.[time_open],"
                        + "s.[time_close],s.[isDeleted],s.[deleted_at],s.[deleted_by],s.[created_at],"
                        + "s.[created_by],s.[updated_at],s.[updated_by],c.[id] [cityId],c.[name] [cityName]"
                        +" FROM [Shops] s JOIN [Cities] c"
                        +" ON s.CitiesId = c.id"
                        +" WHERE s.[isDeleted] = 0 AND s.id = @id";
            var query = SQLUtils.ExecuteCommand(SQLUtils._connStr,
                         conn => conn.QueryFirstOrDefault(get_Shop, new { @id = id }));
          
            return query;
        }

        public void RemoveShops(int id, string username)
        {
            throw new NotImplementedException();
        }

        public void UpdateShops(int id, Shops shop)
        {
            throw new NotImplementedException();
        }
    }
}
