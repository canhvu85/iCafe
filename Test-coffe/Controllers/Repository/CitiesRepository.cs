using Dapper;
using System.Collections.Generic;
using System.Linq;
using Test_coffe.Controllers.Services;
using Test_coffe.Models;

namespace Test_coffe.Controllers.Repository
{
    public class CitiesRepository : ICities
    {
        private string get_Cities;
        private string create_Cities;
        private string update_Cities;
        private string remove_Cities;

        public List<Cities> GetAllCities()
        {
            get_Cities = "SELECT [id],[name] FROM [Cities] WHERE [isDeleted] = 0 ORDER BY [name]";

            var query = SQLUtils.ExecuteCommand(SQLUtils._connStr,
                      conn => conn.Query<Cities>(get_Cities)).ToList();
            return query;
        }

        public void CreateCities(Cities cities)
        {
            create_Cities = "INSERT INTO [Cities] ([name], [permalink], [isDeleted], " +
                                   "[created_at], [created_by]) " +
                                   "VALUES(@name, @permalink, 0, GETDATE(), @created_by)";

            SQLUtils.ExecuteCommand(SQLUtils._connStr, conn =>
            {
                var query = conn.Query<Cities>(create_Cities,
                    new { name = cities.name, permalink = cities.permalink, created_by = cities.created_by });
            });
        }

        public void UpdateCities(int id, Cities cities)
        {
            update_Cities = "UPDATE [Cities] SET [name] = @name, [updated_at] = GETDATE(), " +
                                   "[updated_by] = @updated_by WHERE [id] = @id AND [isDeleted] = 0";

            SQLUtils.ExecuteCommand(SQLUtils._connStr, conn =>
            {
                var query = conn.Query<Cities>(update_Cities,
                    new { name = cities.name, updated_by = cities.updated_by, id = id });
            });
        }

        public void RemoveCities(int id, string username)
        {
            remove_Cities = "UPDATE [Cities] SET [isDeleted] = @isDeleted, " +
                                   "[deleted_at] = GETDATE(), [deleted_by] = @deleted_by WHERE [id] = @id";

            SQLUtils.ExecuteCommand(SQLUtils._connStr, conn =>
            {
                var query = conn.Query<Cities>(remove_Cities,
                    new { isDeleted = 1, deleted_by = username, id = id });
            });
        }

    }
}

