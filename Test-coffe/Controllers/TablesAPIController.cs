using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
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
    public class TablesAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ITokenBuilder _tokenBuilder;
        private bool isExpired;
        private readonly ITables _tablesRepository;

        public TablesAPIController(ApplicationDbContext context, ITokenBuilder tokenBuilder, ITables tablesRepository)
        {
            _context = context;
            _tokenBuilder = tokenBuilder;
            _tablesRepository = tablesRepository;
        }

        // GET: api/TablesAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tables>>> GetTable2(int? shop_id)
        {
            isExpired = _tokenBuilder.isExpiredToken();
            if (isExpired == false)
            {
                var result = _tablesRepository.GetAllTables(shop_id);
                return Ok(result);
            }
            else
                return Unauthorized();
        }

        // PUT: api/TablesAPI/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTable(int id, Tables tables)
        {
            isExpired = _tokenBuilder.isExpiredToken();
            if (isExpired == false)
            {
                if (id != tables.id)
                {
                    return BadRequest();
                }
                var user = HttpContext.Session.GetObjectFromJson<Users>("user");
                tables.updated_by = user.username;
                _tablesRepository.UpdateTables(id, tables);
                return NoContent();
            }
            else
                return Unauthorized();
        }
    }
}
