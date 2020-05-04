using Test_coffe.Models;

namespace Test_coffe.Controllers.Services
{
    public interface IBillDetails
    {
        dynamic GetBillDetailByBills(int? billsId);

        dynamic GetBillDetail(int? TableId);

        dynamic GetGroupOrderPrinted(int? TableId);

        dynamic GetOrderNewWaiter(int? TableId);

        dynamic GetOrderPrinted(int? TableId);

        void CreateBillDetails(BillDetails billDetails);

        void UpdateBillDetails(int id, BillDetails billDetails);
    }
}
