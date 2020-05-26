using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Test_coffe.Controllers.Services;
using Test_coffe.Models;

namespace Test_coffe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillsAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IBills _billsRepository;
        private dynamic result;

        public BillsAPIController(ApplicationDbContext context, IBills billsRepository)
        {
            _context = context;
            _billsRepository = billsRepository;
        }

        [HttpGet]
        public IActionResult GetBillByTable(int? TableId)
        {
            result = _billsRepository.GetBillByTable(TableId);
            return Ok(result);
        }

        [HttpGet("shop/{shopsId}")]
        public IActionResult GetBill2(int? shopsId)
        {
            result = _billsRepository.GetBillByShop(shopsId);
            return Ok(result);
        }

        [HttpGet("shop/{shopsId}/date/{startDate}/{endDate}")]
        public IActionResult GetBillByDate(int? shopsId, string? startDate, string? endDate)
        {
            startDate = String.Format("{0:yyyy/M/d}", DateTime.Parse(startDate));
            endDate = String.Format("{0:yyyy/M/d}", DateTime.Parse(endDate));
            result = _billsRepository.GetBillByDate(shopsId, startDate, endDate);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBill(int id, Bills bills)
        {
            if (id != bills.id)
            {
                return BadRequest();
            }
            _billsRepository.UpdateBills(id, bills);
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Bills>> PostBill(Bills bills)
        {
            result = _billsRepository.CreateBills(bills);
            return Ok(result);
        }
    }
}
