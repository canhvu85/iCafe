using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test_coffe.Controllers.mobile.Services;
using Test_coffe.Controllers.Services;
using Test_coffe.Models;

namespace Test_coffe.Controllers.mobile.Repository
{
    public class FloorsRepository : IFloors
    {
        private readonly ApplicationDbContext _context;
        private string get_Floors;
        private string create_Floor;
        private string update_Floor;
        private string remove_Floor;

        public FloorsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void CreateFloor(Floors floor)
        {
            create_Floor = "INSERT INTO [Floors] ([name], [permalink],[ShopsId], [isDeleted], " +
                                 "[created_at], [created_by]) " +
                                 "VALUES(@name, @permalink,@ShopsId, 0, GETDATE(), @created_by)";

            SQLUtils.ExecuteCommand(SQLUtils._connStr, conn =>
            {
                var query = conn.Query<Floors>(create_Floor,
                    new { name = floor.name, permalink = floor.permalink, ShopsId = floor.ShopsId, created_by = floor.created_by });
            });
        }

        public dynamic GetAllFloors(int? shop_id)
        {
            //get_Floors = "SELECT [id],[name] FROM [Floors] WHERE [isDeleted] = 0 ORDER BY [name]";

            //var query = SQLUtils.ExecuteCommand(SQLUtils._connStr,
            //          conn => conn.Query(get_Floors));
            //return query;
            return _context.Floors.Where(f => f.ShopsId == shop_id && f.isDeleted == false).ToList();
        }

        public void RemoveFloor(int id, string username)
        {
            remove_Floor = "UPDATE [Floors] SET [isDeleted] = @isDeleted, " +
                                  "[deleted_at] = GETDATE(), [deleted_by] = @deleted_by WHERE [id] = @id";

            SQLUtils.ExecuteCommand(SQLUtils._connStr, conn =>
            {
                var query = conn.Query<Floors>(remove_Floor,
                    new { isDeleted = 1, deleted_by = username, id = id });
            });
        }

        public void UpdateFloor(int id, Floors floor)
        {
            update_Floor = "UPDATE [Floors] SET [name] = @name, [permalink] = @permalink, [updated_at] = GETDATE(), " +
                                   "[updated_by] = @updated_by WHERE [id] = @id AND [isDeleted] = 0";

            SQLUtils.ExecuteCommand(SQLUtils._connStr, conn =>
            {
                var query = conn.Query<Floors>(update_Floor,
                    new { name = floor.name, permalink = floor.permalink, updated_by = floor.updated_by, id = id });
            });
        }
    }
}
