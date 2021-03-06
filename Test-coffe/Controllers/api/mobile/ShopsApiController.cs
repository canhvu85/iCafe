﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LazZiya.ImageResize;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Test_coffe.Controllers.mobile.Services;
using Test_coffe.Controllers.Services;
using Test_coffe.Models;

namespace Test_coffe.Controllers
{
    [Route("api/mobile/[controller]")]
    [ApiController]
    public class ShopsApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IHubContext<SignalServer> _contextSignal;
        string connectionString = "";
        private readonly ITokenBuilder _tokenBuilder;
        private bool isExpired;
        private IShops _shopsRepository;
        private IUploadImage _uploadImage;

        public ShopsApiController(ApplicationDbContext context, IHostingEnvironment hostingEnvironment,
                                    IConfiguration configuration, IHubContext<SignalServer> contextSignal,
                                    ITokenBuilder tokenBuilder, IShops shopsRepository, IUploadImage uploadImage)
        {
            _context = context;
            _tokenBuilder = tokenBuilder;
            this._hostingEnvironment = hostingEnvironment;
            _contextSignal = contextSignal;
            connectionString = ConfigurationExtensions.GetConnectionString(configuration, "DefaultConnection");
            _shopsRepository = shopsRepository;
            _uploadImage = uploadImage;
        }

        // GET: api/ShopsApi
       
        [HttpGet]
        public ActionResult GetShop(int? city_id)
        {

            //isExpired = _tokenBuilder.isExpiredToken();
            //if (isExpired == false)
            //{
                var result = _shopsRepository.GetAllShopsByCity(city_id);
                return Ok(result);
                //if (city_id != null)
                //{
                //var result = from s in _context.Shops
                //             join c in _context.Cities on s.CitiesId equals c.id
                //             where (s.isDeleted == false && s.CitiesId == city_id)
                //             select new
                //             {
                //                 id = s.id,
                //                 name = s.name,
                //                 info = s.info,
                //                 images = s.images,
                //                 permalink = s.permalink,
                //                 status = s.status,
                //                 time_open = s.time_open,
                //                 time_close = s.time_close,
                //                 isDeleted = s.isDeleted,
                //                 deleted_at = s.deleted_at,
                //                 deleted_by = s.deleted_by,
                //                 created_at = s.created_at,
                //                 created_by = s.created_by,
                //                 updated_at = s.updated_at,
                //                 updated_by = s.updated_by,
                //                 cityId = c.id,
                //                 cityName = c.name
                //             };
                //return Ok(result);

                //}
                //else
                //{
                //    var result = from s in _context.Shops
                //                 join c in _context.Cities on s.CitiesId equals c.id
                //                 where s.isDeleted == false
                //                 select new
                //                 {
                //                     id = s.id,
                //                     name = s.name,
                //                     info = s.info,
                //                     images = s.images,
                //                     permalink = s.permalink,
                //                     status = s.status,
                //                     time_open = s.time_open,
                //                     time_close = s.time_close,
                //                     isDeleted = s.isDeleted,
                //                     deleted_at = s.deleted_at,
                //                     deleted_by = s.deleted_by,
                //                     created_at = s.created_at,
                //                     created_by = s.created_by,
                //                     updated_at = s.updated_at,
                //                     updated_by = s.updated_by,
                //                     cityId = c.id,
                //                     cityName = c.name
                //                 };
                //    return Ok(result);
                //}
            //}
            //else
            //    return Unauthorized();
        }

        // GET: api/ShopsApi/5
        [HttpGet("{id}")]
        public ActionResult<Shops> GetShop(int id)
        {
            var result = _shopsRepository.GetShop(id);
            return Ok(result);
            //var result = (from s in _context.Shops
            //              join c in _context.Cities on s.CitiesId equals c.id
            //              where s.id == id
            //              select new
            //              {
            //                  id = s.id,
            //                  name = s.name,
            //                  info = s.info,
            //                  images = s.images,
            //                  permalink = s.permalink,
            //                  status = s.status,
            //                  time_open = s.time_open,
            //                  time_close = s.time_close,
            //                  isDeleted = s.isDeleted,
            //                  deleted_at = s.deleted_at,
            //                  deleted_by = s.deleted_by,
            //                  created_at = s.created_at,
            //                  created_by = s.created_by,
            //                  updated_at = s.updated_at,
            //                  updated_by = s.updated_by,
            //                  cityId = c.id,
            //                  cityName = c.name
            //              }).FirstOrDefault();
            //return Ok(result);
        }

        // PUT: api/ShopsApi/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public ActionResult<String> PutShop(int id)
        {

            if (id != int.Parse(HttpContext.Request.Form["id"]))
            {
                return BadRequest();
            }
            var name = HttpContext.Request.Form["name"].ToString();
            var cityId = int.Parse(HttpContext.Request.Form["CityId"]);

            var shop = _context.Shops.Where(s => s.name == name && s.CitiesId == cityId && s.isDeleted == false).ToList().FirstOrDefault();
            if (shop != null && shop.id != id)
            {
                return Content("Shop này đã có, hãy nhập tên khác");
            }
            else
            {

                shop = _context.Shops.Find(id);

                shop.id = id;
                shop.name = HttpContext.Request.Form["name"];
                shop.info = HttpContext.Request.Form["info"];
                // shop.status = int.Parse(HttpContext.Request.Form["status"]);
                shop.permalink = HttpContext.Request.Form["permalink"];
                shop.time_open = DateTime.Parse(HttpContext.Request.Form["time_open"]);
                shop.time_close = DateTime.Parse(HttpContext.Request.Form["time_close"]);
                shop.updated_at = DateTime.Now;
                var user = HttpContext.Session.GetObjectFromJson<Users>("user");
                shop.updated_by = user.username;
                shop.CitiesId = int.Parse(HttpContext.Request.Form["CityId"]);
                //shop.avatar = "abc";
                var httpPostedFile = HttpContext.Request.Form.Files["avatarFile"];
                var img = _uploadImage.changeImage(_hostingEnvironment, httpPostedFile, "shops", shop.id);
                if (img != "") {
                    shop.images = img;
                }
                
                using (var db = _context)
                {
                    db.Shops.Attach(shop);
                    db.Entry(shop).Property(n => n.name).IsModified = true;
                    db.Entry(shop).Property(i => i.info).IsModified = true;
                    db.Entry(shop).Property(c => c.CitiesId).IsModified = true;
                    db.Entry(shop).Property(o => o.time_open).IsModified = true;
                    db.Entry(shop).Property(t => t.time_close).IsModified = true;
                    db.Entry(shop).Property(u => u.permalink).IsModified = true;
                    db.Entry(shop).Property(x => x.status).IsModified = true;
                    db.Entry(shop).Property(a => a.updated_at).IsModified = true;
                    db.Entry(shop).Property(b => b.updated_by).IsModified = true;
                    if (httpPostedFile != null)
                    {
                        db.Entry(shop).Property(x => x.images).IsModified = true;
                        //db.Entry(shop).Property(y => y.thumb).IsModified = true;
                    }
                    db.SaveChanges();
                }

                // return StatusCode(200,shop.images);
                return StatusCode(201,shop.images);
            }

        }

        //Soft Delete

        [HttpPut("del/{id}")]
        public IActionResult SoftDeleteShop(int id, string name)
        {
            var shop = _context.Shops.FindAsync(id);
            if (shop == null)
            {
                return NotFound();
            }
            var user = HttpContext.Session.GetObjectFromJson<Users>("user");
            _shopsRepository.RemoveShops(id, user.username);

            //var shopOld = _context.Shops.Find(id);
            //shopOld.isDeleted = true;
            //shopOld.deleted_at = DateTime.Now;
            //shopOld.deleted_by = name;
            //// var shop1 = new Shops() { id = shop.id, isDeleted = shop.isDeleted, deleted_at = DateTime.Now, deleted_by = shop.deleted_by };
            //using (var db = _context)
            //{
            //    //db.Users.Attach(user);
            //    db.Shops.Attach(shopOld);
            //    db.Entry(shopOld).Property(n => n.isDeleted).IsModified = true;
            //    db.Entry(shopOld).Property(i => i.deleted_at).IsModified = true;
            //    db.Entry(shopOld).Property(c => c.deleted_by).IsModified = true;
            //    db.SaveChanges();
            //}

            return NoContent();
        }

        [HttpPut("inactive/{id}")]
        public IActionResult InActiveShop(int id, string name)
        {
            var shop = _context.Shops.FindAsync(id);
            if (shop == null)
            {
                return NotFound();
            }
            var user = HttpContext.Session.GetObjectFromJson<Users>("user");
            _shopsRepository.setInActiveShop(id, user.username);

            return NoContent();
        }


        [HttpPut("active/{id}")]
        public IActionResult ActiveShop(int id, string name)
        {
            var shop = _context.Shops.FindAsync(id);
            if (shop == null)
            {
                return NotFound();
            }
            var user = HttpContext.Session.GetObjectFromJson<Users>("user");
            _shopsRepository.setActiveShop(id, user.username);

            return NoContent();
        }

        // POST: api/ShopsApi
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.

        //[HttpPost]
        //public async Task<ActionResult<Shops>> PostShop(Shops shop)
        //{
        //    _context.Shops.Add(shop);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetShop", new { id = shop.id }, shop.id);
        //}

        //for formdata
        [HttpPost]
        public async Task<ActionResult<Shops>> PostShop()
        {
            var shop = new Shops();
            shop.name = HttpContext.Request.Form["name"];
            shop.CitiesId = int.Parse(HttpContext.Request.Form["CityId"]);
            var shopExist = _context.Shops.Where(s => s.name.ToLower() == shop.name.ToLower() && s.CitiesId == shop.CitiesId && s.isDeleted == false).FirstOrDefault();
            if(shopExist != null)
            {
                return StatusCode(418);
            }

            shop.info = HttpContext.Request.Form["info"];
           // shop.status = int.Parse(HttpContext.Request.Form["status"]);
            shop.permalink = HttpContext.Request.Form["permalink"];
            shop.time_open = DateTime.Parse(HttpContext.Request.Form["time_open"]);
            shop.time_close = DateTime.Parse(HttpContext.Request.Form["time_close"]);
            var user = HttpContext.Session.GetObjectFromJson<Users>("user");
            shop.created_by = user.username;

            _context.Shops.Add(shop);
            await _context.SaveChangesAsync();
              
            var httpPostedFile = HttpContext.Request.Form.Files["avatarFile"];
          
            shop.images = _uploadImage.upload(_hostingEnvironment,httpPostedFile, "shops", shop.id);
         
            await _context.SaveChangesAsync();
          //  var shops = GetAllShops();
            return CreatedAtAction("GetShop", new { id = shop.id }, new { shop.id, shop.images });
        }

        // DELETE: api/ShopsApi/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Shops>> DeleteShop(int id)
        {
            var shop = await _context.Shops.FindAsync(id);
            if (shop == null)
            {
                return NotFound();
            }
            _context.Shops.Remove(shop);
            await _context.SaveChangesAsync();
            return shop;
        }

        private bool ShopExists(int id)
        {
            return _context.Shops.Any(e => e.id == id);
        }


        public List<Shops> GetAllShops()
        {
            var shops = new List<Shops>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlDependency.Start(connectionString);

                string commandText = "select * from dbo.Shops";

                SqlCommand cmd = new SqlCommand(commandText, conn);

                SqlDependency dependency = new SqlDependency(cmd);

                dependency.OnChange += new OnChangeEventHandler(dbChangeNotification);

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var shop = new Shops
                    {
                        id = Convert.ToInt32(reader["id"]),
                        //Name = reader["Name"].ToString(),
                        //Age = Convert.ToInt32(reader["Age"])
                    };

                    shops.Add(shop);
                }

                conn.Close();
            }

            return shops;
        }

        private void dbChangeNotification(object sender, SqlNotificationEventArgs e)
        {
            _contextSignal.Clients.All.SendAsync("refreshShops");
        }
    }
}
