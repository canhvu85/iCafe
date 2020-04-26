﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Test_coffe.Models;

namespace Test_coffe.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20200426083920_upgh")]
    partial class upgh
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Test_coffe.Models.Accounts", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("UsersId")
                        .HasColumnType("int");

                    b.Property<decimal>("amount")
                        .HasColumnType("decimal(8,0)");

                    b.Property<DateTime>("created_at")
                        .HasColumnType("datetime");

                    b.Property<string>("created_by")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<DateTime?>("deleted_at")
                        .HasColumnType("datetime");

                    b.Property<string>("deleted_by")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("info_card")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<bool>("isDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("permalink")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("type_card")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<DateTime?>("updated_at")
                        .HasColumnType("datetime");

                    b.Property<string>("updated_by")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.HasKey("id");

                    b.HasIndex("UsersId");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("Test_coffe.Models.BillDetails", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BillsId")
                        .HasColumnType("int");

                    b.Property<int?>("ProductsId")
                        .HasColumnType("int");

                    b.Property<DateTime>("created_at")
                        .HasColumnType("datetime");

                    b.Property<string>("created_by")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<DateTime?>("deleted_at")
                        .HasColumnType("datetime");

                    b.Property<string>("deleted_by")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<bool>("isDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("permalink")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<decimal>("price")
                        .HasColumnType("decimal(8,0)");

                    b.Property<int>("quantity")
                        .HasColumnType("int");

                    b.Property<int>("status")
                        .HasColumnType("int");

                    b.Property<decimal>("total")
                        .HasColumnType("decimal(8,0)");

                    b.Property<DateTime?>("updated_at")
                        .HasColumnType("datetime");

                    b.Property<string>("updated_by")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.HasKey("id");

                    b.HasIndex("BillsId");

                    b.HasIndex("ProductsId");

                    b.ToTable("BillDetails");
                });

            modelBuilder.Entity("Test_coffe.Models.Bills", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("TablesId")
                        .HasColumnType("int");

                    b.Property<DateTime>("created_at")
                        .HasColumnType("datetime");

                    b.Property<string>("created_by")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<DateTime?>("deleted_at")
                        .HasColumnType("datetime");

                    b.Property<string>("deleted_by")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<decimal>("fee_service")
                        .HasColumnType("decimal(8,0)");

                    b.Property<bool>("isDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("status")
                        .HasColumnType("int");

                    b.Property<decimal>("sub_total")
                        .HasColumnType("decimal(8,0)");

                    b.Property<DateTime>("time_enter")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("time_out")
                        .HasColumnType("datetime");

                    b.Property<decimal>("total_money")
                        .HasColumnType("decimal(8,0)");

                    b.Property<DateTime?>("updated_at")
                        .HasColumnType("datetime");

                    b.Property<string>("updated_by")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.HasKey("id");

                    b.HasIndex("TablesId");

                    b.ToTable("Bills");
                });

            modelBuilder.Entity("Test_coffe.Models.Cataloges", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ShopsId")
                        .HasColumnType("int");

                    b.Property<DateTime>("created_at")
                        .HasColumnType("datetime");

                    b.Property<string>("created_by")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<DateTime?>("deleted_at")
                        .HasColumnType("datetime");

                    b.Property<string>("deleted_by")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<bool>("isDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("name")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("permalink")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<DateTime?>("updated_at")
                        .HasColumnType("datetime");

                    b.Property<string>("updated_by")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.HasKey("id");

                    b.HasIndex("ShopsId");

                    b.ToTable("Cataloges");
                });

            modelBuilder.Entity("Test_coffe.Models.Cities", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("created_at")
                        .HasColumnType("datetime");

                    b.Property<string>("created_by")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<DateTime?>("deleted_at")
                        .HasColumnType("datetime");

                    b.Property<string>("deleted_by")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<bool>("isDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("permalink")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<DateTime?>("updated_at")
                        .HasColumnType("datetime");

                    b.Property<string>("updated_by")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.HasKey("id");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("Test_coffe.Models.Floors", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ShopsId")
                        .HasColumnType("int");

                    b.Property<DateTime>("created_at")
                        .HasColumnType("datetime");

                    b.Property<string>("created_by")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<DateTime?>("deleted_at")
                        .HasColumnType("datetime");

                    b.Property<string>("deleted_by")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<bool>("isDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("name")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("permalink")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<DateTime?>("updated_at")
                        .HasColumnType("datetime");

                    b.Property<string>("updated_by")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.HasKey("id");

                    b.HasIndex("ShopsId");

                    b.ToTable("Floors");
                });

            modelBuilder.Entity("Test_coffe.Models.Groups", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("created_at")
                        .HasColumnType("datetime");

                    b.Property<string>("created_by")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<DateTime?>("deleted_at")
                        .HasColumnType("datetime");

                    b.Property<string>("deleted_by")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<bool>("isDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("name")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("permalink")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<DateTime?>("updated_at")
                        .HasColumnType("datetime");

                    b.Property<string>("updated_by")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.HasKey("id");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("Test_coffe.Models.PermissionDetails", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("PermissionsId")
                        .HasColumnType("int");

                    b.Property<int?>("UsersId")
                        .HasColumnType("int");

                    b.Property<DateTime>("created_at")
                        .HasColumnType("datetime");

                    b.Property<string>("created_by")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<DateTime?>("deleted_at")
                        .HasColumnType("datetime");

                    b.Property<string>("deleted_by")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<bool>("isDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("updated_at")
                        .HasColumnType("datetime");

                    b.Property<string>("updated_by")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.HasKey("id");

                    b.HasIndex("PermissionsId");

                    b.HasIndex("UsersId");

                    b.ToTable("PermissionDetails");
                });

            modelBuilder.Entity("Test_coffe.Models.Permissions", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("GroupsId")
                        .HasColumnType("int");

                    b.Property<DateTime>("created_at")
                        .HasColumnType("datetime");

                    b.Property<string>("created_by")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<DateTime?>("deleted_at")
                        .HasColumnType("datetime");

                    b.Property<string>("deleted_by")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<bool>("isDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("name")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("permalink")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<DateTime?>("updated_at")
                        .HasColumnType("datetime");

                    b.Property<string>("updated_by")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.HasKey("id");

                    b.HasIndex("GroupsId");

                    b.ToTable("Permissions");
                });

            modelBuilder.Entity("Test_coffe.Models.Positions", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("created_at")
                        .HasColumnType("datetime");

                    b.Property<string>("created_by")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<DateTime?>("deleted_at")
                        .HasColumnType("datetime");

                    b.Property<string>("deleted_by")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<bool>("isDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("name")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("permalink")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<DateTime?>("updated_at")
                        .HasColumnType("datetime");

                    b.Property<string>("updated_by")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.HasKey("id");

                    b.ToTable("Positions");
                });

            modelBuilder.Entity("Test_coffe.Models.Products", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CatalogesId")
                        .HasColumnType("int");

                    b.Property<DateTime>("created_at")
                        .HasColumnType("datetime");

                    b.Property<string>("created_by")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<DateTime?>("deleted_at")
                        .HasColumnType("datetime");

                    b.Property<string>("deleted_by")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("images")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<bool>("isDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("name")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("permalink")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<decimal>("price")
                        .HasColumnType("decimal(8,0)");

                    b.Property<DateTime?>("updated_at")
                        .HasColumnType("datetime");

                    b.Property<string>("updated_by")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.HasKey("id");

                    b.HasIndex("CatalogesId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Test_coffe.Models.Shops", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CitiesId")
                        .HasColumnType("int");

                    b.Property<DateTime>("created_at")
                        .HasColumnType("datetime");

                    b.Property<string>("created_by")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<DateTime?>("deleted_at")
                        .HasColumnType("datetime");

                    b.Property<string>("deleted_by")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("images")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("info")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<bool>("isDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("name")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("permalink")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<int>("status")
                        .HasColumnType("int");

                    b.Property<DateTime>("time_close")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("time_open")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("updated_at")
                        .HasColumnType("datetime");

                    b.Property<string>("updated_by")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.HasKey("id");

                    b.HasIndex("CitiesId");

                    b.ToTable("Shops");
                });

            modelBuilder.Entity("Test_coffe.Models.Tables", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("FloorsId")
                        .HasColumnType("int");

                    b.Property<DateTime>("created_at")
                        .HasColumnType("datetime");

                    b.Property<string>("created_by")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<DateTime?>("deleted_at")
                        .HasColumnType("datetime");

                    b.Property<string>("deleted_by")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<bool>("isDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("name")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("permalink")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<int>("status")
                        .HasColumnType("int");

                    b.Property<DateTime?>("updated_at")
                        .HasColumnType("datetime");

                    b.Property<string>("updated_by")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.HasKey("id");

                    b.HasIndex("FloorsId");

                    b.ToTable("Tables");
                });

            modelBuilder.Entity("Test_coffe.Models.TypeMoneys", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("created_at")
                        .HasColumnType("datetime");

                    b.Property<string>("created_by")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<DateTime?>("deleted_at")
                        .HasColumnType("datetime");

                    b.Property<string>("deleted_by")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<bool>("isDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("name")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<int>("quantity")
                        .HasColumnType("int");

                    b.Property<DateTime?>("updated_at")
                        .HasColumnType("datetime");

                    b.Property<string>("updated_by")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.HasKey("id");

                    b.ToTable("TypeMoneys");
                });

            modelBuilder.Entity("Test_coffe.Models.Units", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("created_at")
                        .HasColumnType("datetime");

                    b.Property<string>("created_by")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<DateTime?>("deleted_at")
                        .HasColumnType("datetime");

                    b.Property<string>("deleted_by")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<bool>("isDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("name")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<DateTime?>("updated_at")
                        .HasColumnType("datetime");

                    b.Property<string>("updated_by")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.HasKey("id");

                    b.ToTable("Units");
                });

            modelBuilder.Entity("Test_coffe.Models.Users", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("PositionsId")
                        .HasColumnType("int");

                    b.Property<int?>("ShopsId")
                        .HasColumnType("int");

                    b.Property<DateTime>("created_at")
                        .HasColumnType("datetime");

                    b.Property<string>("created_by")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<DateTime?>("deleted_at")
                        .HasColumnType("datetime");

                    b.Property<string>("deleted_by")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("images")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<bool>("isDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("name")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("password")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("permalink")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("remember_token")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<DateTime?>("updated_at")
                        .HasColumnType("datetime");

                    b.Property<string>("updated_by")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("username")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.HasKey("id");

                    b.HasIndex("PositionsId");

                    b.HasIndex("ShopsId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Test_coffe.Models.Accounts", b =>
                {
                    b.HasOne("Test_coffe.Models.Users", "Users")
                        .WithMany("Accounts")
                        .HasForeignKey("UsersId");
                });

            modelBuilder.Entity("Test_coffe.Models.BillDetails", b =>
                {
                    b.HasOne("Test_coffe.Models.Bills", "Bills")
                        .WithMany("BillDetails")
                        .HasForeignKey("BillsId");

                    b.HasOne("Test_coffe.Models.Products", "Products")
                        .WithMany("BillDetails")
                        .HasForeignKey("ProductsId");
                });

            modelBuilder.Entity("Test_coffe.Models.Bills", b =>
                {
                    b.HasOne("Test_coffe.Models.Tables", "Tables")
                        .WithMany("Bills")
                        .HasForeignKey("TablesId");
                });

            modelBuilder.Entity("Test_coffe.Models.Cataloges", b =>
                {
                    b.HasOne("Test_coffe.Models.Shops", "Shops")
                        .WithMany("Cataloges")
                        .HasForeignKey("ShopsId");
                });

            modelBuilder.Entity("Test_coffe.Models.Floors", b =>
                {
                    b.HasOne("Test_coffe.Models.Shops", "Shops")
                        .WithMany("Floors")
                        .HasForeignKey("ShopsId");
                });

            modelBuilder.Entity("Test_coffe.Models.PermissionDetails", b =>
                {
                    b.HasOne("Test_coffe.Models.Permissions", "Permissions")
                        .WithMany("PermissionDetails")
                        .HasForeignKey("PermissionsId");

                    b.HasOne("Test_coffe.Models.Users", "Users")
                        .WithMany("PermissionDetails")
                        .HasForeignKey("UsersId");
                });

            modelBuilder.Entity("Test_coffe.Models.Permissions", b =>
                {
                    b.HasOne("Test_coffe.Models.Groups", "Groups")
                        .WithMany("Permissions")
                        .HasForeignKey("GroupsId");
                });

            modelBuilder.Entity("Test_coffe.Models.Products", b =>
                {
                    b.HasOne("Test_coffe.Models.Cataloges", "Cataloges")
                        .WithMany("Products")
                        .HasForeignKey("CatalogesId");
                });

            modelBuilder.Entity("Test_coffe.Models.Shops", b =>
                {
                    b.HasOne("Test_coffe.Models.Cities", "Cities")
                        .WithMany("Shops")
                        .HasForeignKey("CitiesId");
                });

            modelBuilder.Entity("Test_coffe.Models.Tables", b =>
                {
                    b.HasOne("Test_coffe.Models.Floors", "Floors")
                        .WithMany("Tables")
                        .HasForeignKey("FloorsId");
                });

            modelBuilder.Entity("Test_coffe.Models.Users", b =>
                {
                    b.HasOne("Test_coffe.Models.Positions", "Positions")
                        .WithMany("Users")
                        .HasForeignKey("PositionsId");

                    b.HasOne("Test_coffe.Models.Shops", "Shops")
                        .WithMany("Users")
                        .HasForeignKey("ShopsId");
                });
#pragma warning restore 612, 618
        }
    }
}
