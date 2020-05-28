//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.SignalR;
//using Microsoft.Data.SqlClient;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;

//using Test_coffe.Models;

//namespace Test_coffe.Controllers
//{
//    [Route("api/mobile/[controller]")]
//    [ApiController]
//    public class BillDetailsApiController : ControllerBase
//    {
//        private readonly ApplicationDbContext _context;
//        private readonly IHostingEnvironment _hostingEnvironment;
//        private readonly IHubContext<SignalServer> _contextSignal;
       
//        string connectionString = "";
      
//        public BillDetailsApiController(ApplicationDbContext context, IHostingEnvironment hostingEnvironment, IConfiguration configuration, IHubContext<SignalServer> contextSignal)
//        {
//            _context = context;
//            this._hostingEnvironment = hostingEnvironment;
//            _contextSignal = contextSignal;
//             connectionString = ConfigurationExtensions.GetConnectionString(configuration, "DefaultConnection");


//        }

//        // GET: api/BillDetailsApi
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<BillDetails>>> GetBillDetail(int? table_id)
//        {
//            //return await _context.BillDetails.Include(b => b.Products).Where(b => b.status == 1 && b.Bills.TablesId == table_id).ToListAsync();

//            var result = from b in _context.BillDetails
//                         join p in _context.Products on b.ProductsId equals p.id
//                         join s in _context.Bills on b.BillsId equals s.id
//                         where b.isDeleted == false &&
//                         s.TablesId == table_id &&
//                         b.status == 1
//                         group b by new {p.id, p.name, b.price, p.images, b.BillsId } into grp
//                         select new
//                         {
//                             productsId = grp.Key.id,
//                             productsName = grp.Key.name,
//                             price = grp.Key.price,
//                             images = grp.Key.images,
//                             quantity = grp.Sum(x => x.quantity),
//                             total = grp.Sum(x => x.total),
//                             billsid = grp.Key.BillsId
//                         };
//            return Ok(result);

//        }

//        // GET: api/BillDetailsApi/5
//        [HttpGet("{id}")]
//        public async Task<ActionResult<BillDetails>> GetBillDetail(int id)
//        {
//            var billDetail = await _context.BillDetails.FindAsync(id);

//            if (billDetail == null)
//            {
//                return NotFound();
//            }

//            return billDetail;
//        }

//        // PUT: api/BillDetailsApi/5
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
//        // more details see https://aka.ms/RazorPagesCRUD.
//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutBillDetail(int id, BillDetails billDetail)
//        {
//            if (id != billDetail.id)
//            {
//                return BadRequest();
//            }

//            _context.Entry(billDetail).State = EntityState.Modified;

//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!BillDetailExists(id))
//                {
//                    return NotFound();
//                }
//                else
//                {
//                    throw;
//                }
//            }

//            return NoContent();
//        }

//        // POST: api/BillDetailsApi
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
//        // more details see https://aka.ms/RazorPagesCRUD.
//        [HttpPost]
//        public async Task<ActionResult<BillDetails>> PostBillDetail(BillDetails billDetail)
//        {
//            _context.BillDetails.Add(billDetail);
//            await _context.SaveChangesAsync();
//            var b = GetAllBillDetails();
//            return CreatedAtAction("GetBillDetail", new { id = billDetail.id }, billDetail);
//        }

//        // DELETE: api/BillDetailsApi/5
//        [HttpDelete("{id}")]
//        public async Task<ActionResult<BillDetails>> DeleteBillDetail(int id)
//        {
//            var billDetail = await _context.BillDetails.FindAsync(id);
//            if (billDetail == null)
//            {
//                return NotFound();
//            }

//            _context.BillDetails.Remove(billDetail);
//            await _context.SaveChangesAsync();

//            return billDetail;
//        }

//        private bool BillDetailExists(int id)
//        {
//            return _context.BillDetails.Any(e => e.id == id);
//        }


//        public List<BillDetails> GetAllBillDetails()
//        {
//            var billDetailsList = new List<BillDetails>();
//            using (SqlConnection conn = new SqlConnection(connectionString))
//            {
//                conn.Open();

//                SqlDependency.Start(connectionString);

//                string commandText = "select * from dbo.BillDetails";

//                SqlCommand cmd = new SqlCommand(commandText, conn);

//                SqlDependency dependency = new SqlDependency(cmd);

//                dependency.OnChange += new OnChangeEventHandler(dbChangeNotification);

//                var reader = cmd.ExecuteReader();

//                while (reader.Read())
//                {
//                    var billDetails = new BillDetails
//                    {
//                        id = Convert.ToInt32(reader["id"]),
//                        //Name = reader["Name"].ToString(),
//                        //Age = Convert.ToInt32(reader["Age"])
//                    };

//                    billDetailsList.Add(billDetails);
//                }

//                conn.Close();
//            }

//            return billDetailsList;

//        }

//        private void dbChangeNotification(object sender, SqlNotificationEventArgs e)
//        {
//            _contextSignal.Clients.All.SendAsync("refreshBillDetails");
           
//        }

//    }

    

//}
