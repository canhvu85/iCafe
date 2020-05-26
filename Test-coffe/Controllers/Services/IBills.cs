using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Test_coffe.Models;

namespace Test_coffe.Controllers.Services
{
    public interface IBills
    {
        dynamic GetBillByTable(int? TableId);

        dynamic GetBillByShop(int? ShopsId);

        dynamic GetBillByDate(int? shopsId, string? startDate, string? endDate);

        int CreateBills(Bills bills);

        void UpdateBills(int id, Bills bills);
    }
}
