using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Test_coffe.Controllers.Services;
using Test_coffe.Models;

namespace Test_coffe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TablesAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ITables _tablesRepository;
        private dynamic result;

        public TablesAPIController(ApplicationDbContext context, ITables tablesRepository)
        {
            _context = context;
            _tablesRepository = tablesRepository;
        }

        // GET: api/TablesAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tables>>> GetTable2(int? shop_id)
        {
            result = _tablesRepository.GetAllTables(shop_id);
            return Ok(result);
        }

        // PUT: api/TablesAPI/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTable(int id, Tables tables)
        {
            if (id != tables.id)
            {
                return BadRequest();
            }
            _tablesRepository.UpdateTables(id, tables);
            return NoContent();
        }
    }
}
