﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test_coffe.Models;

namespace Test_coffe.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Accounts> Accounts { get; set; }
        public DbSet<Bills> Bills { get; set; }
        public DbSet<BillDetails> BillDetails { get; set; }
        public DbSet<Cataloges> Cataloges { get; set; }
        public DbSet<Cities> Cities { get; set; }
        public DbSet<Floors> Floors { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<Shops> Shops { get; set; }
        public DbSet<Tables> Tables { get; set; }
        public DbSet<TypeMoneys> TypeMoneys { get; set; }
        public DbSet<Units> Units { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Test_coffe.Models.Groups> Groups { get; set; }
        public DbSet<Test_coffe.Models.Permissions> Permissions { get; set; }
        public DbSet<Test_coffe.Models.PermissionDetails> PermissionDetails { get; set; }
        public DbSet<Test_coffe.Models.Positions> Positions { get; set; }
    }
}
