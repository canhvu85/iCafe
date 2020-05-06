using Microsoft.EntityFrameworkCore;
using System;
using Test_coffe.Models;

namespace Test_coffe.Migrations
{
    public static class Config
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cities>().HasData(
                    new Cities
                    {
                        id = 1,
                        name = "Huế",
                        permalink = "Hue"
                    },
                   new Cities
                   {
                       id = 2,
                       name = "Sài Gòn",
                       permalink = "Sai_Gon"
                   });

            modelBuilder.Entity<Positions>().HasData(
                    new Positions
                    {
                        id = 1,
                        name = "Nhân Viên"
                    });

            modelBuilder.Entity<Shops>().HasData(
                    new Shops
                    {
                        id = 1,
                        name = "The One",
                        time_open = DateTime.Parse("2020/01/01"),
                        time_close = DateTime.Parse("2020/10/01"),
                        CitiesId = 1
                    },
                   new Shops
                   {
                       id = 2,
                       name = "HighLand",
                       time_open = DateTime.Parse("2020/01/01"),
                       time_close = DateTime.Parse("2020/10/01"),
                       CitiesId = 1
                   });

            modelBuilder.Entity<Floors>().HasData(
                    new Floors
                    {
                        id = 1,
                        name = "Tầng 1",
                        permalink = "Tang_1",
                        ShopsId = 1
                    },
                   new Floors
                   {
                       id = 2,
                       name = "Tầng 2",
                       permalink = "Tang_2",
                       ShopsId = 1
                   }, new Floors
                   {
                       id = 3,
                       name = "Tầng 1",
                       permalink = "Tang_1",
                       ShopsId = 2
                   });

            modelBuilder.Entity<Tables>().HasData(
                    new Tables
                    {
                        id = 1,
                        name = "Bàn 1",
                        permalink = "Ban_1",
                        FloorsId = 1
                    },
                   new Tables
                   {
                       id = 2,
                       name = "Bàn 2",
                       permalink = "Ban_2",
                       FloorsId = 1
                   }, new Tables
                   {
                       id = 3,
                       name = "Bàn 3",
                       permalink = "Ban_3",
                       FloorsId = 2
                   }, new Tables
                   {
                       id = 4,
                       name = "Bàn 1",
                       permalink = "Ban_1",
                       FloorsId = 3
                   });

            modelBuilder.Entity<Cataloges>().HasData(
                   new Cataloges
                   {
                       id = 1,
                       name = "Coffee",
                       permalink = "Coffee",
                       ShopsId = 1
                   },
                   new Cataloges
                   {
                       id = 2,
                       name = "MilkTea",
                       permalink = "MilkTea",
                       ShopsId = 1
                   },
                   new Cataloges
                   {
                       id = 3,
                       name = "Food",
                       permalink = "Food",
                       ShopsId = 1
                   },
                   new Cataloges
                   {
                       id = 4,
                       name = "MilkTea",
                       permalink = "MilkTea",
                       ShopsId = 2
                   });

            modelBuilder.Entity<Products>().HasData(
                   new Products
                   {
                       id = 1,
                       name = "Cà phê đen",
                       price = 10000,
                       CatalogesId = 1
                   },
                   new Products
                   {
                       id = 2,
                       name = "Cà phê sữa",
                       price = 12000,
                       CatalogesId = 1
                   },
                   new Products
                   {
                       id = 3,
                       name = "Trà sữa socola",
                       price = 18000,
                       CatalogesId = 2
                   },
                   new Products
                   {
                       id = 4,
                       name = "Trà sữa matcha",
                       price = 20000,
                       CatalogesId = 2
                   },
                   new Products
                   {
                       id = 5,
                       name = "Khoai tây chiên",
                       price = 15000,
                       CatalogesId = 3
                   },
                   new Products
                   {
                       id = 6,
                       name = "Cà phê đen",
                       price = 10000,
                       CatalogesId = 4
                   });
        }
    }
}
