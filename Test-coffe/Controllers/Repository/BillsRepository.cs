﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Test_coffe.Controllers.Services;
using Test_coffe.Models;

namespace Test_coffe.Controllers.Repository
{
    public class BillsRepository : IBills
    {
        private readonly ApplicationDbContext _context;

        public BillsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public dynamic GetBillByTable(int? TableId)
        {
            return from b in _context.Bills
                   where b.isDeleted == false &&
                   b.Tables.status != 0 &&
                   b.status == 0 &&
                   b.TablesId == TableId
                   select new
                   {
                       b.id,
                       tablesName = b.Tables.name,
                       b.created_by,
                       b.sub_total,
                       b.fee_service,
                       b.total_money
                   };
        }

        public dynamic GetBillByDate(int? shopsId, string startDate, string endDate)
        {
            return _context.Bills
                .FromSqlRaw("SELECT b.* FROM Bills b JOIN Tables t on b.TablesId = t.id JOIN Floors f on f.id = t.FloorsId " +
                            "WHERE b.isDeleted = 0 AND f.ShopsId = " + shopsId + " AND " +
                            "CAST(b.time_out as date) >= '" + startDate + "' AND CAST(b.time_out as date) <= '" + endDate + "'")
                .Select(b => new
                {
                    b.id,
                    b.time_out,
                    b.Tables.name,
                    b.status,
                    b.created_by,
                    b.sub_total,
                    b.fee_service,
                    b.total_money
                }).ToList();
        }

        //public async Task<ActionResult<Bills>> CreateBills(Bills bills)
        //{

        //    _context.Bills.Add(bills);
        //    await _context.SaveChanges();
        //    return bills;
        //    //int? id = _context.Bills.Max(b => (int?)b.id);
        //    //Console.WriteLine(id);

        //    //return id;
        //}

        public int CreateBills(Bills bills)
        {
            _context.Bills.Add(bills);
            _context.SaveChanges();
            return _context.Bills.Max(b => b.id);
        }

        public void UpdateBills(int id, Bills bills)
        {
            var billsOld = _context.Bills.Find(id);
            billsOld.updated_at = DateTime.Now;
            billsOld.updated_by = bills.updated_by;
            billsOld.time_out = DateTime.Now;
            billsOld.status = bills.status;
            if (bills.sub_total != 0)
            {
                billsOld.sub_total = bills.sub_total;
                billsOld.total_money = billsOld.sub_total + billsOld.fee_service;
            }
            _context.Entry(billsOld).State = EntityState.Modified;

            _context.SaveChanges();
        }
    }
}
