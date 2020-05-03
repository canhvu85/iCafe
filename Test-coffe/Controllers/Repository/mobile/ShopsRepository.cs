using Dapper;
using Microsoft.AspNetCore.Hosting;
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
        private readonly ApplicationDbContext _context;
        private string get_Shops;
        private string get_Shops1;
        private string get_Shop;
        private string create_Shops;
        private string update_Shops;
        private string remove_Shops;
        private string active_Shops;

        public ShopsRepository(ApplicationDbContext context)
        {
            _context = context;
        }
       
        public Shops CreateShops(Shops shop)
        {
            //create_Shops = "INSERT INTO [Shops] ([name], [permalink], [isDeleted], " +
            //                        "[time_open], [time_close]) " +
            //                        "[CityId], [info]) " +
            //                        "[created_at], [created_by]) " +
            //                        "VALUES(@name, @permalink, 0, @time_open, @time_close,@CityId, @info, GETDATE(), @created_by)";

            //SQLUtils.ExecuteCommand(SQLUtils._connStr, conn =>
            //{
            //    var query = conn.Query<Shops>(create_Shops,
            //        new { name = shop.name,info = shop.info,CityId = shop.CitiesId, time_open = shop.time_open, time_close = shop.time_close, permalink = shop.permalink, created_by = shop.created_by });
            //});
            return shop;

        }

        public IList GetAllShopsByCity(int? city_id)
        {
            get_Shops = "SELECT s.[id],s.[name],s.[info],s.[images],s.[permalink],s.[status],s.[time_open],"
                        +"s.[time_close],"
                        +"c.[id] [cityId],c.[name] [cityName]"
                        +" FROM [Shops] s JOIN [Cities] c ON s.CitiesId = c.id"
                        +" WHERE s.[isDeleted] = 0 AND s.CitiesId = @city_id  ORDER BY s.[name]";

            get_Shops1 = "SELECT s.[id],s.[name],s.[info],s.[images],s.[permalink],s.[status],s.[time_open],"
                        + "s.[time_close],s.[isDeleted],"
                        + "c.[id] [cityId],c.[name] [cityName]"
                        +" FROM [Shops] s JOIN [Cities] c"
                        +" ON s.CitiesId = c.id"
                        +" ORDER BY s.[name]";
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
                        + "s.[time_close],"
                        + "c.[id] [cityId],c.[name] [cityName]"
                        +" FROM [Shops] s JOIN [Cities] c"
                        +" ON s.CitiesId = c.id"
                        +" WHERE s.[isDeleted] = 0 AND s.id = @id";
            var query = SQLUtils.ExecuteCommand(SQLUtils._connStr,
                         conn => conn.QueryFirstOrDefault(get_Shop, new { @id = id }));
          
            return query;
        }

        public void RemoveShops(int id, string username)
        {
            remove_Shops = "UPDATE [Shops] SET [isDeleted] = @isDeleted, " +
                                   "[deleted_at] = GETDATE(), [deleted_by] = @deleted_by WHERE [id] = @id";

            SQLUtils.ExecuteCommand(SQLUtils._connStr, conn =>
            {
                var query = conn.Query<Shops>(remove_Shops,
                    new { isDeleted = 1, deleted_by = username, id = id });
            });
        }

        public void setActiveShop(int id, string username)
        {
            active_Shops = "UPDATE [Shops] SET [isDeleted] = @isDeleted, " +
                                   "[deleted_at] = GETDATE(), [deleted_by] = @deleted_by WHERE [id] = @id";

            SQLUtils.ExecuteCommand(SQLUtils._connStr, conn =>
            {
                var query = conn.Query<Shops>(active_Shops,
                    new { isDeleted = 0, deleted_by = username, id = id });
            });
        }

        public void setInActiveShop(int id, string username)
        {
            remove_Shops = "UPDATE [Shops] SET [isDeleted] = @isDeleted, " +
                                   "[deleted_at] = GETDATE(), [deleted_by] = @deleted_by WHERE [id] = @id";

            SQLUtils.ExecuteCommand(SQLUtils._connStr, conn =>
            {
                var query = conn.Query<Shops>(remove_Shops,
                    new { isDeleted = 1, deleted_by = username, id = id });
            });
        }

        public string UpdateShops(int id, Shops shop)
        {
            throw new NotImplementedException();
        }
    }
}
