using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test_coffe.Models;

namespace Test_coffe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillDetailsAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BillDetailsAPIController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/BillDetailsAPI
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<BillDetails>>> GetBillDetail(int? TableId)
        //{
        //    return await _context.BillDetails.Include(b => b.Bills).Include(b => b.Products).Where(b => b.Bills.status == 0 && b.Bills.TablesId == TableId).ToListAsync();
        //}

        [HttpGet]
        public IActionResult GetBillDetail2(int? TableId)
        {
            var result = from b in _context.BillDetails
                         where b.isDeleted == false &&
                         b.Bills.status == 0 &&
                         b.Bills.TablesId == TableId &&
                         (b.status == 0 || b.status == 4 || b.status == 1)
                         select new
                         {
                             id = b.id,
                             billsId = b.BillsId,
                             tablesId = b.Bills.TablesId,
                             productsId = b.ProductsId,
                             productsName = b.Products.name,
                             price = b.price,
                             quantity = b.quantity,
                             total = b.total,
                             status = b.status
                         };

            return Ok(result);
        }

        [HttpGet("bills/{billsId}")]
        public IActionResult GetBillDetailByBills(int? billsId)
        {
            var result = from b in _context.BillDetails
                         where b.isDeleted == false &&
                         b.Bills.id == billsId
                         select new
                         {
                             productsName = b.Products.name,
                             b.price,
                             b.quantity,
                             b.total
                         };

            return Ok(result);
        }

        [HttpGet("TableId/{TableId}")]
        public IActionResult GetGroupOrderPrinted(int? TableId)
        {
            var result = from b in _context.BillDetails
                         join p in _context.Products on b.ProductsId equals p.id
                         where b.isDeleted == false &&
                         b.Bills.status == 0 &&
                         b.Bills.TablesId == TableId &&
                         b.status == 1
                         group b by new { p.name, b.price, b.BillsId } into grp
                         select new
                         {
                             productsName = grp.Key.name,
                             price = grp.Key.price,
                             quantity = grp.Sum(x => x.quantity),
                             total = grp.Sum(x => x.total),
                             billsid = grp.Key.BillsId
                         };
            return Ok(result);
        }

        [HttpGet("new/{TableId}")]
        public IActionResult GetOrderNewWaiter(int? TableId)
        {
            var result = from b in _context.BillDetails
                         where b.isDeleted == false &&
                         b.Bills.status == 0 &&
                         b.Bills.TablesId == TableId &&
                         b.status == 0
                         select new
                         {
                             id = b.id,
                             billsId = b.BillsId,
                             tablesId = b.Bills.TablesId,
                             productsId = b.ProductsId,
                             productsName = b.Products.name,
                             price = b.price,
                             quantity = b.quantity,
                             total = b.total,
                             status = b.status
                         };
            return Ok(result);
        }

        [HttpGet("printed/{TableId}")]
        public IActionResult GetOrderPrinted(int? TableId)
        {
            var result = from b in _context.BillDetails
                         where b.isDeleted == false &&
                         b.Bills.status == 0 &&
                         b.Bills.TablesId == TableId &&
                         b.status == 1
                         select new
                         {
                             id = b.id,
                             billsId = b.BillsId,
                             tablesId = b.Bills.TablesId,
                             productsId = b.ProductsId,
                             productsName = b.Products.name,
                             price = b.price,
                             quantity = b.quantity,
                             total = b.total,
                             status = b.status
                         };
            return Ok(result);
        }


        // GET: api/BillDetailsAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BillDetails>> GetBillDetail(int id)
        {
            var billDetails = await _context.BillDetails.FindAsync(id);

            if (billDetails == null)
            {
                return NotFound();
            }

            return billDetails;
        }

        // PUT: api/BillDetailsAPI/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutBillDetail(int id, BillDetails billDetails)
        //{
        //    if (id != billDetails.id)
        //    {
        //        return BadRequest();
        //    }
        //    var billDetailsOld = _context.BillDetails.Find(id);
        //    billDetailsOld.updated_at = DateTime.Now;
        //    billDetailsOld.updated_by = billDetails.created_by;
        //    billDetailsOld.quantity = billDetails.quantity;
        //    billDetailsOld.total = billDetails.total;
        //    billDetailsOld.status = billDetails.status;
        //    _context.Entry(billDetailsOld).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!BillDetailExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBillDetail(int id, BillDetails billDetails)
        {
            if (id != billDetails.id || !BillDetailExists(id))
            {
                return new OkObjectResult(new { message = "Không tìm thấy" });
            }
            var billDetailsOld = _context.BillDetails.Find(id);
            billDetailsOld.updated_at = DateTime.Now;
            billDetailsOld.updated_by = billDetails.updated_by;
            billDetailsOld.quantity = billDetails.quantity;
            billDetailsOld.total = billDetails.total;
            billDetailsOld.status = billDetails.status;
            _context.Entry(billDetailsOld).State = EntityState.Modified;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // POST: api/BillDetailsAPI
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<BillDetails>> PostBillDetail(BillDetails billDetails)
        {
            _context.BillDetails.Add(billDetails);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBillDetail", new { id = billDetails.id }, billDetails);
        }

        // DELETE: api/BillDetailsAPI/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<BillDetails>> DeleteBillDetail(int id)
        {
            var billDetails = await _context.BillDetails.FindAsync(id);
            if (billDetails == null)
            {
                return NotFound();
            }

            _context.BillDetails.Remove(billDetails);
            await _context.SaveChangesAsync();

            return billDetails;
        }

        private bool BillDetailExists(int id)
        {
            return _context.BillDetails.Any(e => e.id == id);
        }
    }
}
