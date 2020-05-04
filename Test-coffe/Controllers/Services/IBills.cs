using Test_coffe.Models;

namespace Test_coffe.Controllers.Services
{
    public interface IBills
    {
        dynamic GetBillByTable(int? TableId);

        dynamic GetBillByDate(int? shopsId, string? startDate, string? endDate);

        void CreateBills(Bills bills);

        void UpdateBills(int id, Bills bills);
    }
}
