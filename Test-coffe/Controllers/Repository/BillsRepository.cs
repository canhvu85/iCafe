using Dapper;
using Test_coffe.Controllers.Services;
using Test_coffe.Models;

namespace Test_coffe.Controllers.Repository
{
    public class BillsRepository : IBills
    {
        private string get_Bills;
        private string create_Bills;
        private string update_Bills;

        public dynamic GetBillByTable(int? TableId)
        {
            get_Bills = "SELECT b.[id], t.[name] as tablesName, b.[sub_total], b.[fee_service], b.[total_money], " +
                "b.[created_by] FROM[Bills] b JOIN [Tables] t ON b.[TablesId] = t.[id] " +
                "WHERE b.[isDeleted] = 0 AND t.[isDeleted] = 0  AND t.[status] <> 0 " +
                "AND b.[status] = 0 AND b.[TablesId] = @TableId";

            var query = SQLUtils.ExecuteCommand(SQLUtils._connStr,
                                  conn => conn.Query(get_Bills, new { TableId }));
            return query;
        }

        public dynamic GetBillByDate(int? shopsId, string startDate, string endDate)
        {
            get_Bills = "SELECT b.[id], b.[time_out], t.[name], b.[status], b.[created_by], " +
                "b.[sub_total], b.[fee_service], b.[total_money] FROM[Bills] b JOIN[Tables] " +
                "t on b.[TablesId] = t.[id] JOIN[Floors] f on f.[id] = t.[FloorsId] " +
                "WHERE b.[isDeleted] = 0 AND t.[isDeleted] = 0 AND f.[isDeleted] = 0 " +
                "AND f.[ShopsId] = @shopsId AND CAST(b.[time_out] as startDate) >= @startDate " +
                "AND CAST(b.[time_out] as endDate) <= @endDate";

            var query = SQLUtils.ExecuteCommand(SQLUtils._connStr,
                                  conn => conn.Query(get_Bills, new {
                                      ShopsId = shopsId ,
                                      startDate,
                                      endDate
                                  }));
            return query;
        }

        public void CreateBills(Bills Bills)
        {
            create_Bills = "INSERT INTO [Bills] ([time_enter], [status], [sub_total], "+
                "[fee_service], [total_money] ,[TablesId], [isDeleted], [created_at], "+
                "[created_by]) VALUES(GETDATE(), 0, 0, 0, 0, @TablesId, 0, GETDATE(), @created_by)";

            SQLUtils.ExecuteCommand(SQLUtils._connStr, conn =>
            {
                var query = conn.Query<Bills>(create_Bills,
                    new
                    {
                        Bills.TablesId,
                        Bills.created_by
                    });
            });
        }

        public void UpdateBills(int id, Bills Bills)
        {
            update_Bills = "UPDATE [Bills] SET [time_out] = GETDATE(), [status] = @status, " +
                "[sub_total] = @sub_total, [total_money] = @total_money, [updated_at] = GETDATE(), "+
                "[updated_by] = @updated_by WHERE [id] = @id AND [isDeleted] = 0";

            SQLUtils.ExecuteCommand(SQLUtils._connStr, conn =>
            {
                var query = conn.Query<Bills>(update_Bills,
                    new
                    {
                        Bills.status,
                        Bills.sub_total,
                        Bills.total_money,
                        Bills.updated_by,
                        id
                    });
            });
        }
    }
}
