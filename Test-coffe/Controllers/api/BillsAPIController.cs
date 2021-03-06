﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public BillsAPIController(ApplicationDbContext context, IBills billsRepository)
        {
            _context = context;
            _billsRepository = billsRepository;
        }

        [HttpGet]
        public IActionResult GetBillByTable(int? TableId)
        {
            //var result = from b in _context.Bills
            //             where b.isDeleted == false &&
            //             b.Tables.status != 0 &&
            //             b.status == 0 &&
            //             b.TablesId == TableId
            //             select new
            //             {
            //                 b.id,
            //                 tablesName = b.Tables.name,
            //                 b.created_by,
            //                 b.sub_total,
            //                 b.fee_service,
            //                 b.total_money
            //             };
            var result = _billsRepository.GetBillByTable(TableId);
            return Ok(result);
        }

        //[HttpGet("shop/{shopsId}")]
        //public IActionResult GetBill2(int? shopsId)
        //{
        //    var result = from b in _context.Bills
        //                 where b.isDeleted == false &&
        //                 b.Tables.Floors.ShopsId == shopsId
        //                 select new
        //                 {
        //                     b.id,
        //                     b.time_out,
        //                     b.Tables.name,
        //                     b.status,
        //                     b.created_by,
        //                     b.sub_total,
        //                     b.fee_service,
        //                     b.total_money
        //                 };
        //    return Ok(result);
        //}

        [HttpGet("shop/{shopsId}/date/{startDate}/{endDate}")]
        public IActionResult GetBillByDate(int? shopsId, string? startDate, string? endDate)
        {
            startDate = String.Format("{0:yyyy/M/d}", DateTime.Parse(startDate));
            endDate = String.Format("{0:yyyy/M/d}", DateTime.Parse(endDate));
            //var result = _context.Bills
            //    .FromSqlRaw("SELECT b.* FROM Bills b JOIN Tables t on b.TablesId = t.id JOIN Floors f on f.id = t.FloorsId " +
            //                "WHERE b.isDeleted = 0 AND f.ShopsId = " + shopsId + " AND " +
            //                "CAST(b.time_out as date) >= '" + startDate + "' AND CAST(b.time_out as date) <= '" + endDate + "'")
            //    .Select(b => new
            //    {
            //        b.id,
            //        b.time_out,
            //        b.Tables.name,
            //        b.status,
            //        b.created_by,
            //        b.sub_total,
            //        b.fee_service,
            //        b.total_money
            //    }).ToList();
            var result = _billsRepository.GetBillByDate(shopsId, startDate, endDate);
            return Ok(result);
        }

        // PUT: api/BillsAPI/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBill(int id, Bills bills)
        {
            if (id != bills.id)
            {
                return BadRequest();
            }
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
            _billsRepository.UpdateBills(id, billsOld);
            //_context.Entry(billsOld).State = EntityState.Modified;

            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!BillExists(id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}
            var result = _billsRepository.GetBillByTable(billsOld.TablesId);
            return CreatedAtAction("GetBill", result);
        }

        // POST: api/BillsAPI
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Bills>> PostBill(Bills bills)
        {
            //_context.Bills.Add(bills);
            //await _context.SaveChangesAsync();
            _billsRepository.CreateBills(bills);
            var result = _billsRepository.GetBillByTable(bills.TablesId);
            return CreatedAtAction("GetBill", result);
        }
    }
}
