﻿using System;
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
using Test_coffe.Controllers.mobile.Services;
using Test_coffe.Models;

namespace Test_coffe.Controllers
{
    [Route("api/mobile/[controller]")]
    [ApiController]
    public class ProductsApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;
        private IUploadImage _uploadImage;
        private IProducts _productsRepository;

        public ProductsApiController(ApplicationDbContext context, IHostingEnvironment hostingEnvironment, IUploadImage uploadImage, IProducts productsRepository)
        {
            _context = context;
            _uploadImage = uploadImage;
            this._hostingEnvironment = hostingEnvironment;
            _productsRepository = productsRepository;
        }

        // GET: api/ProductsApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Products>>> GetProductList(int? cataId)
        {
            var result = _productsRepository.GetAllProductsByCataloge(cataId);
            return Ok(result);
            //if (cataId == null)
            //{
            //   var  result = from s in _context.Products
            //                 join c in _context.Cataloges on s.CatalogesId equals c.id
            //                 where s.isDeleted == false
            //                 orderby s.created_at descending
            //                 select new
            //                 {
            //                     id = s.id,
            //                     name = s.name,
            //                     price = s.price,
            //                     images = s.images,
            //                     unit = s.unit,
            //                     permalink = s.permalink,
            //                     isDeleted = s.isDeleted,
            //                     deleted_at = s.deleted_at,
            //                     deleted_by = s.deleted_by,
            //                     created_at = s.created_at,
            //                     created_by = s.created_by,
            //                     updated_at = s.updated_at,
            //                     updated_by = s.updated_by,
            //                     catalogeId = c.id,
            //                     catalogeName = c.name
            //                 };
            //    return Ok(result);
            //}
            //else
            //{
            //  var  result = from s in _context.Products
            //                 join c in _context.Cataloges on s.CatalogesId equals c.id
            //                 where s.isDeleted == false && cataId == s.CatalogesId
            //                orderby s.updated_at descending
            //                select new
            //                 {
            //                     id = s.id,
            //                     name = s.name,
            //                     price = s.price,
            //                     images = s.images,
            //                     unit = s.unit,
            //                     permalink = s.permalink,
            //                     isDeleted = s.isDeleted,
            //                     deleted_at = s.deleted_at,
            //                     deleted_by = s.deleted_by,
            //                     created_at = s.created_at,
            //                     created_by = s.created_by,
            //                     updated_at = s.updated_at,
            //                     updated_by = s.updated_by,
            //                     catalogeId = c.id,
            //                     catalogeName = c.name
            //                 };
            //    return Ok(result);
            //}
            
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
                // return  _context.Products.Where(p => p.Cataloges.ShopsId == shop_id && p.isDeleted == false).ToList();
                var result = _productsRepository.GetAllProductsByShop(shop_id);
                return Ok(result);
            }

        }


        //for CP
        // GET: api/ProductsApi
        [HttpGet("shop/cp")]
        public ActionResult<IEnumerable<Products>> GetProductListByShopIdCp(int? shop_id)
        {

            if (shop_id == null)
            {

                return NotFound();
            }
            else
            {
                ////return  _context.Products.Where(p => p.Cataloges.ShopsId == shop_id && p.isDeleted == false).ToList();
                //var result = from s in _context.Products
                //             join c in _context.Cataloges on s.CatalogesId equals c.id
                //             where s.isDeleted == false && shop_id == s.Cataloges.ShopsId
                //             orderby s.updated_at descending
                //             select new
                //             {
                //                 id = s.id,
                //                 name = s.name,
                //                 price = s.price,
                //                 images = s.images,
                //                 unit = s.unit,
                //                 permalink = s.permalink,
                //                 isDeleted = s.isDeleted,
                //                 deleted_at = s.deleted_at,
                //                 deleted_by = s.deleted_by,
                //                 created_at = s.created_at,
                //                 created_by = s.created_by,
                //                 updated_at = s.updated_at,
                //                 updated_by = s.updated_by,
                //                 catalogeId = c.id,
                //                 catalogeName = c.name
                //             };
                var result = _productsRepository.GetAllProductsByShopCp(shop_id);
                return Ok(result);

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
                              unit = s.unit,
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
            product.unit = HttpContext.Request.Form["unit"];
            product.updated_at = DateTime.Now;
            var user = HttpContext.Session.GetObjectFromJson<Users>("user");
            product.updated_by = user.username;
            product.CatalogesId = int.Parse(HttpContext.Request.Form["CatalogeId"]);
            //shop.avatar = "abc";
            var httpPostedFile = HttpContext.Request.Form.Files["avatarFile"];
            var img = _uploadImage.changeImage(_hostingEnvironment, httpPostedFile, "products", product.id);
            if (img != "")
            {
                product.images = img;
            }
      
            using (var db = _context)
            {
                db.Products.Attach(product);
                db.Entry(product).Property(n => n.name).IsModified = true;
                db.Entry(product).Property(i => i.price).IsModified = true;
                db.Entry(product).Property(x => x.unit).IsModified = true;
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

            //var productOld = _context.Products.Find(id);
            //productOld.isDeleted = true;
            //productOld.deleted_at = DateTime.Now;
            //productOld.deleted_by = name;

            //using (var db = _context)
            //{
            //    //db.Users.Attach(user);
            //    db.Products.Attach(productOld);
            //    db.Entry(productOld).Property(n => n.isDeleted).IsModified = true;
            //    db.Entry(productOld).Property(i => i.deleted_at).IsModified = true;
            //    db.Entry(productOld).Property(c => c.deleted_by).IsModified = true;
            //    db.SaveChanges();
            //}
            _productsRepository.RemoveProducts(id, name);
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
            product.unit = HttpContext.Request.Form["unit"];
            product.CatalogesId = int.Parse(HttpContext.Request.Form["CatalogeId"]);
            var user = HttpContext.Session.GetObjectFromJson<Users>("user");
            product.created_by = user.username;
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            
            var httpPostedFile = HttpContext.Request.Form.Files["avatarFile"];
           
            product.images = _uploadImage.upload(_hostingEnvironment,httpPostedFile,"products",product.id);
              
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
