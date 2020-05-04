using Dapper;
using System.Linq;
using Test_coffe.Controllers.Services;
using Test_coffe.Models;

namespace Test_coffe.Controllers.Repository
{
    public class LoginRepository : ILogin
    {
        private string get_Users;
        private string create_Users;

        public dynamic GetUser(Users users)
        {
            get_Users = "SELECT u.[id], u.[name], u.[username], u.[PositionsId], u.[ShopsId], " +
                        "u.[remember_token] FROM [Users] u JOIN [Shops] s ON u.[ShopsId] = s.[id] " +
                        "WHERE u.[isDeleted] = 0 AND s.[isDeleted] = 0 AND u.[username] = @username " +
                        "AND u.[password] = @password AND u.[ShopsId] = @ShopsId " +
                        "AND CAST(s.[time_open] as [date]) <= GETDATE() AND " +
                        "CAST(GETDATE() as [date]) <= CAST(s.[time_close] as [date])";

            var query = SQLUtils.ExecuteCommand(SQLUtils._connStr,
                      conn => conn.Query(get_Users,
                      new
                      {
                          users.username,
                          users.password,
                          users.ShopsId
                      })).FirstOrDefault();
            return query;
        }

        public void Register(Users users)
        {
            create_Users = "INSERT INTO [Users] ([username], [password], [PositionsId], [ShopsId], [isDeleted], [created_at])" +
                                   "VALUES(@username, @password, @PositionsId, @ShopsId, 0, GETDATE())";

            SQLUtils.ExecuteCommand(SQLUtils._connStr, conn =>
            {
                var query = conn.Query<Users>(create_Users,
                    new
                    {
                        users.username,
                        users.password,
                        users.PositionsId,
                        users.ShopsId
                    });
            });
        }
    }
}
