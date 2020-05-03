using Dapper;
using System.Linq;
using Test_coffe.Controllers.Services;
using Test_coffe.Models;

namespace Test_coffe.Controllers.Repository
{
    public class TablesRepository : ITables
    {
        private string get_Tables;
        private string update_Tables;

        public dynamic GetAllTables(int? shopsId)
        {
            get_Tables = "SELECT t.[id], t.[name] FROM [Tables] t JOIN [Floors] f ON t.FloorsId = f.id " +
                         "WHERE f.ShopsId = @ShopsId AND t.[isDeleted] = 0 AND f.[isDeleted] = 0 ORDER BY t.[name]";

            var query = SQLUtils.ExecuteCommand(SQLUtils._connStr,
                      conn => conn.Query(get_Tables, new { ShopsId = shopsId })).ToList();
            return query;
        }

        public void UpdateTables(int id, Tables Tables)
        {
            update_Tables = "UPDATE [Tables] SET [status] = @status, [updated_at] = GETDATE(), " +
                                   "[updated_by] = @updated_by WHERE [id] = @id AND [isDeleted] = 0";

            SQLUtils.ExecuteCommand(SQLUtils._connStr, conn =>
            {
                var query = conn.Query<Tables>(update_Tables,
                    new { status = Tables.status, updated_by = Tables.updated_by, id = id });
            });
        }
    }
}
