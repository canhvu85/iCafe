using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test_coffe.Controllers.Services;
using Test_coffe.Models;

namespace Test_coffe.Controllers.Repository
{
    public class BillDetailsRepository : IBillDetails
    {
        private readonly ApplicationDbContext _context;

        public BillDetailsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public dynamic GetBillDetailByBills(int? billsId)
        {
            return from b in _context.BillDetails
                   where b.isDeleted == false &&
                   b.Bills.id == billsId
                   select new
                   {
                       productsName = b.Products.name,
                       b.price,
                       b.quantity,
                       b.total
                   };
        }

        public dynamic GetBillDetail(int? TableId)
        {
            return from b in _context.BillDetails
                   where b.isDeleted == false &&
                   b.Bills.status == 0 &&
                   b.Bills.TablesId == TableId &&
                   (b.status == 0 || b.status == 4 || b.status == 1)
                   select new
                   {
                       b.id,
                       billsId = b.BillsId,
                       tablesId = b.Bills.TablesId,
                       productsId = b.ProductsId,
                       productsName = b.Products.name,
                       b.price,
                       b.quantity,
                       b.total,
                       b.status
                   };
        }

        public dynamic GetGroupOrderPrinted(int? TableId)
        {
            return from b in _context.BillDetails
                   join p in _context.Products on b.ProductsId equals p.id
                   where b.isDeleted == false &&
                   b.Bills.status == 0 &&
                   b.Bills.TablesId == TableId &&
                   b.status == 1
                   group b by new { p.name, b.price, b.BillsId } into grp
                   select new
                   {
                       productsName = grp.Key.name,
                       grp.Key.price,
                       quantity = grp.Sum(x => x.quantity),
                       total = grp.Sum(x => x.total),
                       billsid = grp.Key.BillsId
                   };
        }

        public dynamic GetOrderNewWaiter(int? TableId)
        {
            return from b in _context.BillDetails
                   where b.isDeleted == false &&
                   b.Bills.status == 0 &&
                   b.Bills.TablesId == TableId &&
                   b.status == 0
                   select new
                   {
                       b.id,
                       billsId = b.BillsId,
                       tablesId = b.Bills.TablesId,
                       productsId = b.ProductsId,
                       productsName = b.Products.name,
                       b.price,
                       b.quantity,
                       b.total,
                       b.status
                   };
        }

        public dynamic GetOrderPrinted(int? TableId)
        {
            return from b in _context.BillDetails
                   where b.isDeleted == false &&
                   b.Bills.status == 0 &&
                   b.Bills.TablesId == TableId &&
                   b.status == 1
                   select new
                   {
                       b.id,
                       billsId = b.BillsId,
                       tablesId = b.Bills.TablesId,
                       productsId = b.ProductsId,
                       productsName = b.Products.name,
                       b.price,
                       b.quantity,
                       b.total,
                       b.status
                   };
        }

        public void CreateBillDetails(BillDetails billDetails)
        {
            _context.BillDetails.Add(billDetails);
            _context.SaveChangesAsync();
        }

        public void UpdateBillDetails(int id, BillDetails billDetails)
        {
            var billDetailsOld = _context.BillDetails.Find(id);
            billDetailsOld.updated_at = DateTime.Now;
            billDetailsOld.updated_by = billDetails.updated_by;
            billDetailsOld.quantity = billDetails.quantity;
            billDetailsOld.total = billDetails.total;
            billDetailsOld.status = billDetails.status;
            _context.Entry(billDetailsOld).State = EntityState.Modified;

            _context.SaveChangesAsync();
        }
    }
}
