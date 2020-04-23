using LazZiya.ImageResize;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Test_coffe.Models;

namespace Test_coffe.Controllers
{
    [Route("api/mobile/[controller]")]
    [ApiController]
    public class CitiesApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;

        public CitiesApiController(ApplicationDbContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            this._hostingEnvironment = hostingEnvironment;
        }

        // GET: api/CitiesAPI
        [HttpGet]
        public IList<Cities> GetCity()
        {
            return _context.Cities.FromSqlRaw("Select * From Cities Where isDeleted = 0").ToList();
        }

        [HttpGet("full")]
        public async Task<ActionResult<IEnumerable<Cities>>> GetCityFull()
        {
            return await _context.Cities.ToListAsync();
        }

        // GET: api/CitiesAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cities>> GetCity(int id)
        {
            var city = await _context.Cities.FindAsync(id);

            if (city == null)
            {
                return NotFound();
            }

            return city;
        }

        // PUT: api/CitiesAPI/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutCity(int id,Cities city)
        //{
        //    if (id != city.id)
        //    {
        //        return BadRequest();
        //    }
        //    city.updated_at = DateTime.Now;
        //    city.updated_by = "Đan";
        //    _context.Entry(city).State = EntityState.Modified;
        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!CityExists(id))
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

        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutCity(int id)
        //{
        //    var city = new Cities();
        //    city.id = id;
        //    city.name = HttpContext.Request.Form["name"];
        //    city.permalink = HttpContext.Request.Form["permalink"];
        //    //shop.avatar = "abc";
        //    var httpPostedFile = HttpContext.Request.Form.Files["avatarFile"];
        //    string uniqueFileName = null;
        //    string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "uploads/avatar");
        //    uniqueFileName = Guid.NewGuid().ToString() + "_" + httpPostedFile.FileName;
        //    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
        //    httpPostedFile.CopyTo(new FileStream(filePath, FileMode.Create));
        //    city.avatar = uniqueFileName;
        //    string uploadsFolder1 = Path.Combine(_hostingEnvironment.WebRootPath, "uploads/thumb");
        //    string filePath1 = Path.Combine(uploadsFolder1, uniqueFileName);
        //    var input_Image_Path = filePath;
        //    // var output_Image_Path = Path.Combine(_hostingEnvironment.WebRootPath, "uploads/thumb");
        //    using (var stream = httpPostedFile.OpenReadStream())
        //    {
        //        var uploadedImage = Image.FromStream(stream);
        //        //returns Image file
        //        var img = ImageResize.Scale(uploadedImage, 800, 600);
        //        img.SaveAs($"wwwroot\\uploads\\thumb\\{uniqueFileName}");
        //    }
        //    city.thumb = uniqueFileName;

        //    city.updated_at = DateTime.Now;
        //    city.updated_by = "Đan";
        //    //_context.Cities.Add(city);
        //    _context.Entry(city).State = EntityState.Modified;
        //    await _context.SaveChangesAsync();
        //    return NoContent();
        //}

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCity(int id)
        {
            var city = _context.Cities.Find(id);
            //var city = new Cities();
            city.id = id;
            city.name = HttpContext.Request.Form["name"];
            city.permalink = HttpContext.Request.Form["permalink"];
            
            city.updated_at = DateTime.Now;
            city.updated_by = "Đan";
            _context.Entry(city).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetCity", new { id = city.id });
        }

        // POST: api/CitiesAPI
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        //[HttpPost]
        //public async Task<ActionResult<Cities>> PostCity()
        //{
        //    //city.created_by = "Đan";
        //    //_context.Cities.Add(city);
        //    //await _context.SaveChangesAsync();

        //    //return CreatedAtAction("GetCity", new { id = city.id }, city);


        //    var city = new Cities();
        //    city.name = HttpContext.Request.Form["name"];
        //    city.permalink = HttpContext.Request.Form["permalink"];
        //    city.created_by = "Đan";
        //    var image = HttpContext.Request.Form.Files["avatarFile"];
        //    var slugAvatar = HttpContext.Request.Form["slugAvatar"];
        //    Console.WriteLine(image.FileName);
        //    if (image != null)
        //    {
        //        string directory = Path.Combine(_hostingEnvironment.WebRootPath, "uploads/city");

        //        if (!Directory.Exists(Path.Combine(_hostingEnvironment.WebRootPath, "uploads/avatar3")))
        //        {
        //            Directory.CreateDirectory(directory);
        //        }

        //        string uniqueFileName = null;
        //        string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "uploads/avatar");
        //        uniqueFileName = Guid.NewGuid().ToString() + "_" + slugAvatar;
        //        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
        //        image.CopyTo(new FileStream(filePath, FileMode.Create));
        //        city.avatar = uniqueFileName;
        //        string uploadsFolder1 = Path.Combine(_hostingEnvironment.WebRootPath, "uploads/thumb");
        //        string filePath1 = Path.Combine(uploadsFolder1, uniqueFileName);
        //        var input_Image_Path = filePath;
        //        // var output_Image_Path = Path.Combine(_hostingEnvironment.WebRootPath, "uploads/thumb");
        //        using (var stream = image.OpenReadStream())
        //        {
        //            var uploadedImage = Image.FromStream(stream);
        //            //returns Image file
        //            var img = ImageResize.Scale(uploadedImage, 800, 600);
        //            img.SaveAs($"wwwroot\\uploads\\thumb\\{uniqueFileName}");
        //        }
        //        city.thumb = uniqueFileName;
        //    }
        //    _context.Cities.Add(city);
        //    await _context.SaveChangesAsync();
        //    return CreatedAtAction("GetCity", new { id = city.id }, new { city.id, city.avatar, city.thumb });
        //}

        [HttpPost]
        public async Task<ActionResult<Cities>> PostCity()
        {
            var city = new Cities();
            city.name = HttpContext.Request.Form["name"];
            city.permalink = HttpContext.Request.Form["permalink"];
            _context.Cities.Add(city);
            await _context.SaveChangesAsync();

            var cityImg = _context.Cities.Find(city.id);
            cityImg.created_by = "Đan";
            _context.Entry(cityImg).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetCity", new { id = city.id });
        }

        // DELETE: api/CitiesAPI/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<Cities>> DeleteCity(int id)
        //{
        //    var city = await _context.Cities.FindAsync(id);
        //    if (city == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Cities.Remove(city);
        //    await _context.SaveChangesAsync();

        //    return city;
        //}

        [HttpDelete("{id}")]
        public async Task<ActionResult<Cities>> DeleteCity(int id)
        {
            var city = await _context.Cities.FindAsync(id);
            if (city == null)
            {
                return NotFound();
            }
            city.deleted_by = "Đan";
            city.deleted_at = DateTime.Now;
            city.isDeleted = true;
            _context.Entry(city).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return city;
        }


        private bool CityExists(int id)
        {
            return _context.Cities.Any(e => e.id == id);
        }
    }
}
