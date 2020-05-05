using Dapper;
using Test_coffe.Controllers.Services;
using Test_coffe.Models;

namespace Test_coffe.Controllers.Repository
{
    public class CatalogesRepository : ICataloges
    {
        private string get_Cataloges;
        private string create_Cataloges;
        private string update_Cataloges;
        private string remove_Cataloges;

        public dynamic GetAllCataloges()
        {
            get_Cataloges = "SELECT c.[id], c.[name], c.[ShopsId] AS shopsId, s.[name] AS shopsName " +
                            "FROM [Cataloges] c JOIN [Shops] s ON c.[ShopsId] = s.[id] " +
                            "WHERE c.[isDeleted] = 0  AND s.[isDeleted] = 0 ORDER BY c.[name]";

            var query = SQLUtils.ExecuteCommand(SQLUtils._connStr,
                      conn => conn.Query(get_Cataloges));
            return query;
        }

        public void CreateCataloges(Cataloges Cataloges)
        {
            create_Cataloges = "INSERT INTO [Cataloges] ([name], [permalink], [ShopsId], [isDeleted], " +
                                   "[created_at], [created_by]) " +
                                   "VALUES(@name, @permalink, @ShopsId, 0, GETDATE(), @created_by)";

            SQLUtils.ExecuteCommand(SQLUtils._connStr, conn =>
            {
                var query = conn.Query<Cataloges>(create_Cataloges,
                    new
                    {
                        Cataloges.name,
                        Cataloges.permalink,
                        Cataloges.ShopsId,
                        Cataloges.created_by
                    });
            });
        }

        public void UpdateCataloges(int id, Cataloges Cataloges)
        {
            update_Cataloges = "UPDATE [Cataloges] SET [name] = @name, [permalink] = @permalink, [updated_at] = GETDATE(), " +
                                   "[updated_by] = @updated_by WHERE [id] = @id AND [isDeleted] = 0";

            SQLUtils.ExecuteCommand(SQLUtils._connStr, conn =>
            {
                var query = conn.Query<Cataloges>(update_Cataloges,
                    new
                    {
                        Cataloges.name,
                        Cataloges.permalink,
                        Cataloges.updated_by,
                        id
                    });
            });
        }

        public void RemoveCataloges(int id, string username)
        {
            remove_Cataloges = "UPDATE [Cataloges] SET [isDeleted] = @isDeleted, " +
                                   "[deleted_at] = GETDATE(), [deleted_by] = @deleted_by WHERE [id] = @id";

            SQLUtils.ExecuteCommand(SQLUtils._connStr, conn =>
            {
                var query = conn.Query<Cataloges>(remove_Cataloges,
                    new
                    {
                        isDeleted = 1,
                        deleted_by = username,
                        id
                    });
            });
        }

        public dynamic GetAllCatalogesByShop(int? shopsId)
        {
            get_Cataloges = "SELECT c.[id], c.[name], c.[ShopsId] AS shopsId, s.[name] AS shopsName " +
                "FROM [Cataloges] c JOIN [Shops] s ON c.[ShopsId] = s.[id] " +
                "WHERE c.[isDeleted] = 0 AND s.[isDeleted] = 0 AND c.[ShopsId] = @ShopsId ORDER BY c.[name]";

            var query = SQLUtils.ExecuteCommand(SQLUtils._connStr,
                      conn => conn.Query(get_Cataloges, new { ShopsId = shopsId }
                      ));
            return query;

        }
    }
}
