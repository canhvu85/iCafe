using Dapper;
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

        public dynamic GetAllCities()
        {
            get_Cities = "SELECT [id],[name] FROM [Cities] WHERE [isDeleted] = 0 ORDER BY [name]";

            var query = SQLUtils.ExecuteCommand(SQLUtils._connStr,
                      conn => conn.Query(get_Cities));
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
                    new
                    {
                        cities.name,
                        cities.permalink,
                        cities.created_by
                    });
            });
        }

        public void UpdateCities(int id, Cities cities)
        {
            update_Cities = "UPDATE [Cities] SET [name] = @name, [permalink] = @permalink, [updated_at] = GETDATE(), " +
                                   "[updated_by] = @updated_by WHERE [id] = @id AND [isDeleted] = 0";

            SQLUtils.ExecuteCommand(SQLUtils._connStr, conn =>
                {
                    var query = conn.Query<Cities>(update_Cities,
                        new
                        {
                            cities.name,
                            cities.permalink,
                            cities.updated_by,
                            id
                        });
                });
        }

        public void RemoveCities(int id, string username)
        {
            remove_Cities = "UPDATE [Cities] SET [isDeleted] = @isDeleted, " +
                                   "[deleted_at] = GETDATE(), [deleted_by] = @deleted_by WHERE [id] = @id";

            SQLUtils.ExecuteCommand(SQLUtils._connStr, conn =>
            {
                var query = conn.Query<Cities>(remove_Cities,
                    new
                    {
                        isDeleted = 1,
                        deleted_by = username,
                        id
                    });
            });
        }
    }
}

