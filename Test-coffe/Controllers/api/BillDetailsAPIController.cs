using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Test_coffe.Controllers.Services;
using Test_coffe.Models;

namespace Test_coffe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillDetailsAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IBillDetails _billsDetailsRepository;
        private dynamic result;

        public BillDetailsAPIController(ApplicationDbContext context, IBillDetails billsDetailsRepository)
        {
            _context = context;
            _billsDetailsRepository = billsDetailsRepository;
        }

        [HttpGet]
        public IActionResult GetBillDetail2(int? TableId)
        {
            //var result = from b in _context.BillDetails
            //             where b.isDeleted == false &&
            //             b.Bills.status == 0 &&
            //             b.Bills.TablesId == TableId &&
            //             (b.status == 0 || b.status == 4 || b.status == 1)
            //             select new
            //             {
            //                 b.id,
            //                 billsId = b.BillsId,
            //                 tablesId = b.Bills.TablesId,
            //                 productsId = b.ProductsId,
            //                 productsName = b.Products.name,
            //                 b.price,
            //                 b.quantity,
            //                 b.total,
            //                 b.status
            //             };
            result = _billsDetailsRepository.GetBillDetail(TableId);
            return Ok(result);
        }

        [HttpGet("bills/{billsId}")]
        public IActionResult GetBillDetailByBills(int? billsId)
        {
            //var result = from b in _context.BillDetails
            //             where b.isDeleted == false &&
            //             b.Bills.id == billsId
            //             select new
            //             {
            //                 productsName = b.Products.name,
            //                 b.price,
            //                 b.quantity,
            //                 b.total
            //             };

            result = _billsDetailsRepository.GetBillDetailByBills(billsId);
            return Ok(result);
        }

        [HttpGet("TableId/{TableId}")]
        public IActionResult GetGroupOrderPrinted(int? TableId)
        {
            //var result = from b in _context.BillDetails
            //             join p in _context.Products on b.ProductsId equals p.id
            //             where b.isDeleted == false &&
            //             b.Bills.status == 0 &&
            //             b.Bills.TablesId == TableId &&
            //             b.status == 1
            //             group b by new { p.name, b.price, b.BillsId } into grp
            //             select new
            //             {
            //                 productsName = grp.Key.name,
            //                 grp.Key.price,
            //                 quantity = grp.Sum(x => x.quantity),
            //                 total = grp.Sum(x => x.total),
            //                 billsid = grp.Key.BillsId
            //             };
            result = _billsDetailsRepository.GetGroupOrderPrinted(TableId);
            return Ok(result);
        }

        [HttpGet("new/{TableId}")]
        public IActionResult GetOrderNewWaiter(int? TableId)
        {
            //var result = from b in _context.BillDetails
            //             where b.isDeleted == false &&
            //             b.Bills.status == 0 &&
            //             b.Bills.TablesId == TableId &&
            //             b.status == 0
            //             select new
            //             {
            //                 b.id,
            //                 billsId = b.BillsId,
            //                 tablesId = b.Bills.TablesId,
            //                 productsId = b.ProductsId,
            //                 productsName = b.Products.name,
            //                 b.price,
            //                 b.quantity,
            //                 b.total,
            //                 b.status
            //             };
            result = _billsDetailsRepository.GetOrderNewWaiter(TableId);
            return Ok(result);
        }

        [HttpGet("printed/{TableId}")]
        public IActionResult GetOrderPrinted(int? TableId)
        {
            //var result = from b in _context.BillDetails
            //             where b.isDeleted == false &&
            //             b.Bills.status == 0 &&
            //             b.Bills.TablesId == TableId &&
            //             b.status == 1
            //             select new
            //             {
            //                 b.id,
            //                 billsId = b.BillsId,
            //                 tablesId = b.Bills.TablesId,
            //                 productsId = b.ProductsId,
            //                 productsName = b.Products.name,
            //                 b.price,
            //                 b.quantity,
            //                 b.total,
            //                 b.status
            //             };
            result = _billsDetailsRepository.GetOrderPrinted(TableId);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public IActionResult PutBillDetail(int id, BillDetails billDetails)
        {
            if (id != billDetails.id || !BillDetailExists(id))
            {
                return new OkObjectResult(new { message = "Không tìm thấy" });
            }
            _billsDetailsRepository.UpdateBillDetails(id, billDetails);
            return NoContent();
        }

        [HttpPost]
        public ActionResult<BillDetails> PostBillDetail(BillDetails billDetails)
        {
            result = _billsDetailsRepository.CreateBillDetails(billDetails);
            return Ok(result);
        }

        private bool BillDetailExists(int id)
        {
            return _context.BillDetails.Any(e => e.id == id);
        }
    }
}
