﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test_coffe.Models;

namespace Test_coffe.Controllers.mobile
{
    [Route("api/mobile/[controller]")]
    [ApiController]
    public class UsersApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UsersApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Users>>> GetUser()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Users>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }
        //get by username, password, shopId
        [HttpPost("mobile/userlogin")]
        public async Task<ActionResult<Users>> PostUser1(Users user)
        {
            var dateCurrent = DateTime.Now;
             Console.WriteLine(user.username);
            Console.WriteLine(user.password);
            var result = (from u in _context.Users
                         // join s in _context.Shops on u.id equals s.id
                          where u.isDeleted == false
                              && u.username == user.username
                              && u.password == user.password
                              && u.ShopsId == user.ShopsId
                              && u.Shops.time_open <= dateCurrent
                              && dateCurrent <= u.Shops.time_close
                          select new Users
                          {
                              id = u.id,
                              name = u.name,
                              images = u.images
                          }).FirstOrDefault();

            // Console.WriteLine(result);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }



        // PUT: api/Users/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, Users user)
        {
            if (id != user.id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost("mobile/userregister")]
        public async Task<ActionResult<Users>> PostUser(Users user)
        {
           
            if (_context.Users.Any(e => e.username == user.username))
            {
                return NotFound("Tên này đã tồn tại trong hệ thống !");
            }
            else
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetUser", new { id = user.id }, user);
            }
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Users>> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return user;
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.id == id);
        }
    }
}
