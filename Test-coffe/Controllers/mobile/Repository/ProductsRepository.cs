using Dapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test_coffe.Controllers.mobile.Services;
using Test_coffe.Controllers.Services;
using Test_coffe.Models;

namespace Test_coffe.Controllers.mobile.Repository
{
    public class ProductsRepository : IProducts
    {
        private readonly ApplicationDbContext _context;
        private string get_Products;
        private string get_Products1;
        private string get_Product;
        private string create_Product;
        private string update_Product;
        private string remove_Product;
        public ProductsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Products CreateProducts(Products product)
        {
            throw new NotImplementedException();
        }

        public IList GetAllProductsByCataloge(int? cata_id)
        {
            //get_Products = "SELECT s.[id],s.[name],s.[price],s.[images],s.[unit],s.[permalink],"
            //            + "c.[id] [catalogeId],c.[name] [catalogeName]"
            //            + " FROM [Products] s JOIN [Cataloges] c ON s.CatalogesId = c.id"
            //            + " WHERE s.[isDeleted] = 0 AND s.CatalogesId = @cata_id  ORDER BY s.[updated_at]";

            //get_Products1 = "SELECT s.[id],s.[name],s.[price],s.[images],s.[unit],s.[permalink],"
            //            + "c.[id] [catalogeId],c.[name] [catalogeName]"
            //            + " FROM [Products] s JOIN [Cataloges] c ON s.CatalogesId = c.id"
            //            + " WHERE s.[isDeleted] = 0 ORDER BY s.[updated_at]";

            //if (cata_id != null)
            //{
            //    var query = SQLUtils.ExecuteCommand(SQLUtils._connStr,
            //              conn => conn.Query(get_Products, new { cata_id = cata_id })).ToList();
            //    return query;
            //}
            //else
            //{
            //    var query = SQLUtils.ExecuteCommand(SQLUtils._connStr,
            //             conn => conn.Query(get_Products1)).ToList();
            //    return query;
            //}
            if (cata_id == null)
            {
                var result = (from s in _context.Products
                             join c in _context.Cataloges on s.CatalogesId equals c.id
                             where s.isDeleted == false
                             orderby s.created_at descending
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
                             }).ToList();
                return result;
            }
            else
            {
                var result = (from s in _context.Products
                             join c in _context.Cataloges on s.CatalogesId equals c.id
                             where s.isDeleted == false && cata_id == s.CatalogesId
                             orderby s.updated_at descending
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
                             }).ToList();
                return result;
            }
        }

        public IEnumerable<Products> GetAllProductsByShop(int? shop_id)
        {
            return _context.Products.Where(p => p.Cataloges.ShopsId == shop_id && p.isDeleted == false).ToList();
        }

        public IEnumerable GetAllProductsByShopCp(int? shop_id)
        {
            var result = (from s in _context.Products
                         join c in _context.Cataloges on s.CatalogesId equals c.id
                         where s.isDeleted == false && shop_id == s.Cataloges.ShopsId
                         orderby s.updated_at descending
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
                         }).ToList();
            return result;
        }

        public object GetProduct(int id)
        {
            throw new NotImplementedException();
        }

        public void RemoveProducts(int id, string username)
        {
            remove_Product = "UPDATE [Products] SET [isDeleted] = @isDeleted, " +
                                  "[deleted_at] = GETDATE(), [deleted_by] = @deleted_by WHERE [id] = @id";

            SQLUtils.ExecuteCommand(SQLUtils._connStr, conn =>
            {
                var query = conn.Query<Shops>(remove_Product,
                    new { isDeleted = 1, deleted_by = username, id = id });
            });
        }

        public string UpdateProducts(int id, Products product)
        {
            throw new NotImplementedException();
        }
    }
}
