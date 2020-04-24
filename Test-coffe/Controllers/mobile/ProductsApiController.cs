using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LazZiya.ImageResize;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test_coffe.Models;

namespace Test_coffe.Controllers
{
    [Route("api/mobile/[controller]")]
    [ApiController]
    public class ProductsApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;

        public ProductsApiController(ApplicationDbContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            this._hostingEnvironment = hostingEnvironment;
        }

        // GET: api/ProductsApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Products>>> GetProductList(int? cataId)
        {
           
            if (cataId == null)
            {
               var  result = from s in _context.Products
                             join c in _context.Cataloges on s.CatalogesId equals c.id
                             where s.isDeleted == false
                             orderby s.created_at descending
                             select new
                             {
                                 id = s.id,
                                 name = s.name,
                                 price = s.price,
                                 images = s.images,
                                 permalink = s.permalink,
                                 isDeleted = s.isDeleted,
                                 deleted_at = s.deleted_at,
                                 deleted_by = s.deleted_by,
                                 created_at = s.created_at,
                                 created_by = s.created_by,
                                 updated_at = s.updated_at,
                                 updated_by = s.updated_by,
                                 catalogeId = c.id,
                                 catalogeName = c.name
                             };
                return Ok(result);
            }
            else
            {
              var  result = from s in _context.Products
                             join c in _context.Cataloges on s.CatalogesId equals c.id
                             where s.isDeleted == false && cataId == s.CatalogesId
                            orderby s.updated_at descending
                            select new
                             {
                                 id = s.id,
                                 name = s.name,
                                 price = s.price,
                                 images = s.images,
                                 permalink = s.permalink,
                                 isDeleted = s.isDeleted,
                                 deleted_at = s.deleted_at,
                                 deleted_by = s.deleted_by,
                                 created_at = s.created_at,
                                 created_by = s.created_by,
                                 updated_at = s.updated_at,
                                 updated_by = s.updated_by,
                                 catalogeId = c.id,
                                 catalogeName = c.name
                             };
                return Ok(result);
            }
            
           // return await _context.Products.ToListAsync();
        }


        // GET: api/ProductsApi
        [HttpGet("shop")]
        public ActionResult<IEnumerable<Products>> GetProductListByShopId(int? shop_id)
        {

            if (shop_id == null)
            {
                
                return NotFound();
            }
            else
            {
                return  _context.Products.Where(p => p.Cataloges.ShopsId == shop_id && p.isDeleted == false).ToList();
               
            }

        }


        // GET: api/ProductsApi/5
        [HttpGet("{id}")]
        public ActionResult<Products> GetProduct(int id)
        {
            var result = (from s in _context.Products
                          join c in _context.Cataloges on s.CatalogesId equals c.id
                          where s.id == id
                          select new
                          {
                              id = s.id,
                              name = s.name,
                              price = s.price,
                              images = s.images,
                              permalink = s.permalink,
                              isDeleted = s.isDeleted,
                              deleted_at = s.deleted_at,
                              deleted_by = s.deleted_by,
                              created_at = s.created_at,
                              created_by = s.created_by,
                              updated_at = s.updated_at,
                              updated_by = s.updated_by,
                              catalogeId = c.id,
                              catalogeName = c.name
                          }).FirstOrDefault();
            return Ok(result);
        }

        // PUT: api/ProductsApi/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public ActionResult<String> PutProduct(int id)
        {
            if (id != int.Parse(HttpContext.Request.Form["id"]))
            {
                return BadRequest();
            }
            string str = "";
            var product = _context.Products.Find(id);

            product.id = id;
            product.name = HttpContext.Request.Form["name"];
            product.price =decimal.Parse(HttpContext.Request.Form["price"]);
  
            product.permalink = HttpContext.Request.Form["permalink"];

            product.updated_at = DateTime.Now;
            product.updated_by = "vu";
            product.CatalogesId = int.Parse(HttpContext.Request.Form["CatalogeId"]);
            //shop.avatar = "abc";
            var httpPostedFile = HttpContext.Request.Form.Files["avatarFile"];
            if (httpPostedFile != null)
            {
                string directory = Path.Combine(_hostingEnvironment.WebRootPath, "uploads/products/" + product.id);
                string uniqueFileName = null;
                string uniqueFileName1 = null;
                string uploadsFolder = directory;
                uniqueFileName = Guid.NewGuid().ToString() + "_" + httpPostedFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                httpPostedFile.CopyTo(new FileStream(filePath, FileMode.Create));
               // product.avatar = uniqueFileName;

                uniqueFileName1 = Guid.NewGuid().ToString() + "_" + httpPostedFile.FileName;
                using (var stream = httpPostedFile.OpenReadStream())
                {
                    var uploadedImage = Image.FromStream(stream);
                    var x = uploadedImage.Width;
                    var y = uploadedImage.Height;
                    if (x > y)
                    {
                        x = 175;
                        y = y / x * 175;
                    }
                    else
                    {
                        y = 150;
                        x = x / y * 150;
                    }
                    //returns Image file
                    var img = ImageResize.Scale(uploadedImage, x, y);

                    img.SaveAs(uploadsFolder + "/" + uniqueFileName1);
                }


                product.images = "{" + '"' + "avatar" + '"' + ":" + '"' + uniqueFileName + '"' + "," + '"' + "thumb" + '"' + ":" + '"' + uniqueFileName1 + '"' + "}";
                //product.thumb = uniqueFileName;
            };
            //else
            //{
            //    product.avatar = "no-image.png";
            //    product.thumb = "no-image.png";
            //}

            using (var db = _context)
            {
                db.Products.Attach(product);
                db.Entry(product).Property(n => n.name).IsModified = true;
                db.Entry(product).Property(i => i.price).IsModified = true;
                db.Entry(product).Property(c => c.CatalogesId).IsModified = true;               
                db.Entry(product).Property(u => u.permalink).IsModified = true;             
                db.Entry(product).Property(a => a.updated_at).IsModified = true;
                db.Entry(product).Property(b => b.updated_by).IsModified = true;
                if (httpPostedFile != null)
                {
                    db.Entry(product).Property(x => x.images).IsModified = true;
                  
                }
                db.SaveChanges();
            }

            return product.images;
        }

        //Soft Delete

        [HttpPut("del/{id}")]
        public IActionResult SoftDeleteProduct(int id, String name)
        {
            //if (id != product.id)
            //{
            //    return BadRequest();
            //}
            var productOld = _context.Products.Find(id);
            productOld.isDeleted = true;
            productOld.deleted_at = DateTime.Now;
            productOld.deleted_by = name;
           // var product1 = new Products() { id = product.id, isDeleted = product.isDeleted, deleted_at = DateTime.Now, deleted_by = product.deleted_by };
            using (var db = _context)
            {
                //db.Users.Attach(user);
                db.Products.Attach(productOld);
                db.Entry(productOld).Property(n => n.isDeleted).IsModified = true;
                db.Entry(productOld).Property(i => i.deleted_at).IsModified = true;
                db.Entry(productOld).Property(c => c.deleted_by).IsModified = true;
                db.SaveChanges();
            }

            return NoContent();
        }

        // POST: api/ProductsApi
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        //[HttpPost]
        //public async Task<ActionResult<Products>> PostProduct(Products product)
        //{
        //    _context.Products.Add(product);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetProduct", new { id = product.id }, product);
        //}

        [HttpPost]
        public async Task<ActionResult<Products>> PostProduct()
        {
            var product = new Products();
            product.name = HttpContext.Request.Form["name"];
            product.price =decimal.Parse(HttpContext.Request.Form["price"]);
            product.permalink = HttpContext.Request.Form["permalink"];
            product.CatalogesId = int.Parse(HttpContext.Request.Form["CatalogeId"]);
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            
            var httpPostedFile = HttpContext.Request.Form.Files["avatarFile"];
            if (httpPostedFile != null)
            {
                string directory = Path.Combine(_hostingEnvironment.WebRootPath, "uploads/products");
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                string subDirectory = directory + "/" + product.id;
                if (!Directory.Exists(subDirectory))
                {
                    Directory.CreateDirectory(subDirectory);
                }

                string uniqueFileName = null;
                string uniqueFileName1 = null;
                string uploadsFolder = subDirectory;
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                // string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "uploads/avatar");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + httpPostedFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                httpPostedFile.CopyTo(new FileStream(filePath, FileMode.Create));

                // product.avatar = uniqueFileName;
                uniqueFileName1 = Guid.NewGuid().ToString() + "_" + httpPostedFile.FileName;
                // string uploadsFolder1 = subDirectory + "/thumb";
                //if (!Directory.Exists(uploadsFolder1))
                //{
                //    Directory.CreateDirectory(uploadsFolder1);
                //}

                //string filePath1 = Path.Combine(uploadsFolder1, uniqueFileName);
                //var input_Image_Path = filePath;
                // var output_Image_Path = Path.Combine(_hostingEnvironment.WebRootPath, "uploads/thumb");
                using (var stream = httpPostedFile.OpenReadStream())
                {
                    var uploadedImage = Image.FromStream(stream);
                    //returns Image file
                    var x = uploadedImage.Width;
                    var y = uploadedImage.Height;
                    if (x > y)
                    {
                        x = 175;
                        y = y / x * 175;
                    }
                    else
                    {
                        y = 150;
                        x = x / y * 150;
                    }
                    var img = ImageResize.Scale(uploadedImage, x, y);
                    img.SaveAs(uploadsFolder + "/" + uniqueFileName1);
                }

                product.images = "{" + '"' + "avatar" + '"' + ":" + '"' + uniqueFileName + '"' + "," + '"' + "thumb" + '"' + ":" + '"' + uniqueFileName1 + '"' + "}";
                //product.thumb = uniqueFileName;
            }
            else
            {
                string directory = Path.Combine(_hostingEnvironment.WebRootPath, "uploads/products");
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                string subDirectory = directory + "/" + product.id;
                if (!Directory.Exists(subDirectory))
                {
                    Directory.CreateDirectory(subDirectory);
                }

                string uploadsFolder = subDirectory;
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                //string uploadsFolder1 = subDirectory;
                //if (!Directory.Exists(uploadsFolder1))
                //{
                //    Directory.CreateDirectory(uploadsFolder1);
                //}

                string n = product.id.ToString();
                string sourceDir = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
                string backupDir = Path.Combine(_hostingEnvironment.WebRootPath, "uploads/products" + n);
                string uniqueFileName = null;
                uniqueFileName = "no-image.png";
                //  System.IO.File.Copy(Path.Combine(sourceDir, "no-image.png"), Path.Combine(uploadsFolder, uniqueFileName), true);
                //  System.IO.File.Copy(Path.Combine(sourceDir, "no-image.png"), Path.Combine(uploadsFolder1, uniqueFileName), true);
                //product.avatar = uniqueFileName;
                //product.thumb = uniqueFileName;
                product.images = "{" + '"' + "avatar" + '"' + ":" + '"' + uniqueFileName + '"' + "," + '"' + "thumb" + '"' + ":" + '"' + uniqueFileName + '"' + "}";
            }
               
            await _context.SaveChangesAsync();
            
            return CreatedAtAction("GetProduct", new { id = product.id }, new { product.id, product.images });
        }



        // DELETE: api/ProductsApi/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Products>> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return product;
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.id == id);
        }
    }
}
