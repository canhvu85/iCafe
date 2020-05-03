using Dapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test_coffe.Controllers.mobile.Services;
using Test_coffe.Controllers.Services;
using Test_coffe.Models;

namespace Test_coffe.Controllers.mobile.Repository
{
    public class TablesMobileRepository : ITablesMobile
    {
        private readonly ApplicationDbContext _context;
        private string remove_Table;

        public TablesMobileRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void CreateTable(Tables table)
        {
            throw new NotImplementedException();
        }

        public dynamic GetAllTablesFloor(int? floor_id)
        {
            return _context.Tables.Include(t => t.Floors).Where(t => t.Floors.id == floor_id && t.isDeleted == false).ToList();
        }

        public dynamic GetAllTablesShop(int? shop_id)
        {
            return _context.Tables.Include(t => t.Floors).Where(t => t.Floors.ShopsId == shop_id && t.isDeleted == false).ToList();
        }

        public void RemoveTable(int id, string username)
        {
            remove_Table = "UPDATE [Tables] SET [isDeleted] = @isDeleted, " +
                                  "[deleted_at] = GETDATE(), [deleted_by] = @deleted_by WHERE [id] = @id";

            SQLUtils.ExecuteCommand(SQLUtils._connStr, conn =>
            {
                var query = conn.Query<Tables>(remove_Table,
                    new { isDeleted = 1, deleted_by = username, id = id });
            });
        }

        public void UpdateTable(int id, Tables table)
        {
            throw new NotImplementedException();
        }
    }
}
