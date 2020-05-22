using Dapper;
using Test_coffe.Controllers.Services;

namespace Test_coffe.Controllers.Repository
{
    public class MenuRepository : IMenu
    {
        private string get_Menu;

        public dynamic GetMenu()
        {
            get_Menu = "SELECT [id], [name], [url], [icon], [parentId] FROM [Menu] WHERE [isDeleted] = 0";

            var query = SQLUtils.ExecuteCommand(SQLUtils._connStr,
                      conn => conn.Query(get_Menu));
            return query;
        }
    }
}
