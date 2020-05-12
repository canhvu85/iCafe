using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Test_coffe.Migrations
{
    public partial class up : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(maxLength: 255, nullable: false),
                    permalink = table.Column<string>(maxLength: 255, nullable: true),
                    isDeleted = table.Column<bool>(nullable: false),
                    deleted_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    deleted_by = table.Column<string>(maxLength: 255, nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    created_by = table.Column<string>(maxLength: 255, nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    updated_by = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(maxLength: 255, nullable: true),
                    permalink = table.Column<string>(maxLength: 255, nullable: true),
                    isDeleted = table.Column<bool>(nullable: false),
                    deleted_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    deleted_by = table.Column<string>(maxLength: 255, nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    created_by = table.Column<string>(maxLength: 255, nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    updated_by = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Menu",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(maxLength: 255, nullable: true),
                    url = table.Column<string>(maxLength: 255, nullable: true),
                    parentId = table.Column<int>(nullable: false),
                    isDeleted = table.Column<bool>(nullable: false),
                    deleted_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    deleted_by = table.Column<string>(maxLength: 255, nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    created_by = table.Column<string>(maxLength: 255, nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    updated_by = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menu", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(maxLength: 255, nullable: true),
                    permalink = table.Column<string>(maxLength: 255, nullable: true),
                    isDeleted = table.Column<bool>(nullable: false),
                    deleted_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    deleted_by = table.Column<string>(maxLength: 255, nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    created_by = table.Column<string>(maxLength: 255, nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    updated_by = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "TypeMoneys",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(maxLength: 255, nullable: true),
                    quantity = table.Column<int>(nullable: false),
                    isDeleted = table.Column<bool>(nullable: false),
                    deleted_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    deleted_by = table.Column<string>(maxLength: 255, nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    created_by = table.Column<string>(maxLength: 255, nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    updated_by = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeMoneys", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Units",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(maxLength: 255, nullable: true),
                    isDeleted = table.Column<bool>(nullable: false),
                    deleted_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    deleted_by = table.Column<string>(maxLength: 255, nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    created_by = table.Column<string>(maxLength: 255, nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    updated_by = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Units", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Shops",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(maxLength: 255, nullable: true),
                    info = table.Column<string>(maxLength: 255, nullable: true),
                    images = table.Column<string>(maxLength: 255, nullable: true),
                    time_open = table.Column<DateTime>(type: "datetime", nullable: false),
                    time_close = table.Column<DateTime>(type: "datetime", nullable: false),
                    status = table.Column<int>(nullable: false),
                    permalink = table.Column<string>(maxLength: 255, nullable: true),
                    CitiesId = table.Column<int>(nullable: true),
                    isDeleted = table.Column<bool>(nullable: false),
                    deleted_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    deleted_by = table.Column<string>(maxLength: 255, nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    created_by = table.Column<string>(maxLength: 255, nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    updated_by = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shops", x => x.id);
                    table.ForeignKey(
                        name: "FK_Shops_Cities_CitiesId",
                        column: x => x.CitiesId,
                        principalTable: "Cities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(maxLength: 255, nullable: true),
                    permalink = table.Column<string>(maxLength: 255, nullable: true),
                    GroupsId = table.Column<int>(nullable: true),
                    isDeleted = table.Column<bool>(nullable: false),
                    deleted_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    deleted_by = table.Column<string>(maxLength: 255, nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    created_by = table.Column<string>(maxLength: 255, nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    updated_by = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.id);
                    table.ForeignKey(
                        name: "FK_Permissions_Groups_GroupsId",
                        column: x => x.GroupsId,
                        principalTable: "Groups",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Cataloges",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(maxLength: 255, nullable: true),
                    permalink = table.Column<string>(maxLength: 255, nullable: true),
                    ShopsId = table.Column<int>(nullable: true),
                    isDeleted = table.Column<bool>(nullable: false),
                    deleted_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    deleted_by = table.Column<string>(maxLength: 255, nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    created_by = table.Column<string>(maxLength: 255, nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    updated_by = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cataloges", x => x.id);
                    table.ForeignKey(
                        name: "FK_Cataloges_Shops_ShopsId",
                        column: x => x.ShopsId,
                        principalTable: "Shops",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Floors",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(maxLength: 255, nullable: true),
                    permalink = table.Column<string>(maxLength: 255, nullable: true),
                    ShopsId = table.Column<int>(nullable: true),
                    isDeleted = table.Column<bool>(nullable: false),
                    deleted_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    deleted_by = table.Column<string>(maxLength: 255, nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    created_by = table.Column<string>(maxLength: 255, nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    updated_by = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Floors", x => x.id);
                    table.ForeignKey(
                        name: "FK_Floors_Shops_ShopsId",
                        column: x => x.ShopsId,
                        principalTable: "Shops",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(maxLength: 255, nullable: true),
                    images = table.Column<string>(maxLength: 255, nullable: true),
                    username = table.Column<string>(maxLength: 255, nullable: true),
                    password = table.Column<string>(maxLength: 255, nullable: true),
                    remember_token = table.Column<string>(maxLength: 255, nullable: true),
                    permalink = table.Column<string>(maxLength: 255, nullable: true),
                    PositionsId = table.Column<int>(nullable: true),
                    ShopsId = table.Column<int>(nullable: true),
                    isDeleted = table.Column<bool>(nullable: false),
                    deleted_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    deleted_by = table.Column<string>(maxLength: 255, nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    created_by = table.Column<string>(maxLength: 255, nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    updated_by = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id);
                    table.ForeignKey(
                        name: "FK_Users_Positions_PositionsId",
                        column: x => x.PositionsId,
                        principalTable: "Positions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_Shops_ShopsId",
                        column: x => x.ShopsId,
                        principalTable: "Shops",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(maxLength: 255, nullable: true),
                    images = table.Column<string>(maxLength: 255, nullable: true),
                    price = table.Column<decimal>(type: "decimal(8,0)", nullable: false),
                    permalink = table.Column<string>(maxLength: 255, nullable: true),
                    unit = table.Column<string>(maxLength: 255, nullable: true),
                    CatalogesId = table.Column<int>(nullable: true),
                    isDeleted = table.Column<bool>(nullable: false),
                    deleted_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    deleted_by = table.Column<string>(maxLength: 255, nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    created_by = table.Column<string>(maxLength: 255, nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    updated_by = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.id);
                    table.ForeignKey(
                        name: "FK_Products_Cataloges_CatalogesId",
                        column: x => x.CatalogesId,
                        principalTable: "Cataloges",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tables",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(maxLength: 255, nullable: true),
                    status = table.Column<int>(nullable: false),
                    permalink = table.Column<string>(maxLength: 255, nullable: true),
                    FloorsId = table.Column<int>(nullable: true),
                    isDeleted = table.Column<bool>(nullable: false),
                    deleted_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    deleted_by = table.Column<string>(maxLength: 255, nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    created_by = table.Column<string>(maxLength: 255, nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    updated_by = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tables", x => x.id);
                    table.ForeignKey(
                        name: "FK_Tables_Floors_FloorsId",
                        column: x => x.FloorsId,
                        principalTable: "Floors",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    amount = table.Column<decimal>(type: "decimal(8,0)", nullable: false),
                    type_card = table.Column<string>(maxLength: 255, nullable: true),
                    info_card = table.Column<string>(maxLength: 255, nullable: true),
                    permalink = table.Column<string>(maxLength: 255, nullable: true),
                    UsersId = table.Column<int>(nullable: true),
                    isDeleted = table.Column<bool>(nullable: false),
                    deleted_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    deleted_by = table.Column<string>(maxLength: 255, nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    created_by = table.Column<string>(maxLength: 255, nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    updated_by = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.id);
                    table.ForeignKey(
                        name: "FK_Accounts_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PermissionDetails",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    permalink_permissions = table.Column<string>(maxLength: 255, nullable: true),
                    action = table.Column<string>(maxLength: 255, nullable: true),
                    PermissionsId = table.Column<int>(nullable: true),
                    UsersId = table.Column<int>(nullable: true),
                    isDeleted = table.Column<bool>(nullable: false),
                    deleted_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    deleted_by = table.Column<string>(maxLength: 255, nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    created_by = table.Column<string>(maxLength: 255, nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    updated_by = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionDetails", x => x.id);
                    table.ForeignKey(
                        name: "FK_PermissionDetails_Permissions_PermissionsId",
                        column: x => x.PermissionsId,
                        principalTable: "Permissions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PermissionDetails_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Bills",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    time_enter = table.Column<DateTime>(type: "datetime", nullable: false),
                    time_out = table.Column<DateTime>(type: "datetime", nullable: true),
                    status = table.Column<int>(nullable: false),
                    sub_total = table.Column<decimal>(type: "decimal(8,0)", nullable: false),
                    fee_service = table.Column<decimal>(type: "decimal(8,0)", nullable: false),
                    total_money = table.Column<decimal>(type: "decimal(8,0)", nullable: false),
                    TablesId = table.Column<int>(nullable: true),
                    isDeleted = table.Column<bool>(nullable: false),
                    deleted_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    deleted_by = table.Column<string>(maxLength: 255, nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    created_by = table.Column<string>(maxLength: 255, nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    updated_by = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bills", x => x.id);
                    table.ForeignKey(
                        name: "FK_Bills_Tables_TablesId",
                        column: x => x.TablesId,
                        principalTable: "Tables",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BillDetails",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    price = table.Column<decimal>(type: "decimal(8,0)", nullable: false),
                    quantity = table.Column<int>(nullable: false),
                    total = table.Column<decimal>(type: "decimal(8,0)", nullable: false),
                    permalink = table.Column<string>(maxLength: 255, nullable: true),
                    status = table.Column<int>(nullable: false),
                    ProductsId = table.Column<int>(nullable: true),
                    BillsId = table.Column<int>(nullable: true),
                    isDeleted = table.Column<bool>(nullable: false),
                    deleted_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    deleted_by = table.Column<string>(maxLength: 255, nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    created_by = table.Column<string>(maxLength: 255, nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    updated_by = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillDetails", x => x.id);
                    table.ForeignKey(
                        name: "FK_BillDetails_Bills_BillsId",
                        column: x => x.BillsId,
                        principalTable: "Bills",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BillDetails_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "id", "created_at", "created_by", "deleted_at", "deleted_by", "isDeleted", "name", "permalink", "updated_at", "updated_by" },
                values: new object[] { 1, new DateTime(2020, 5, 12, 20, 23, 1, 321, DateTimeKind.Local).AddTicks(4232), null, null, null, false, "Huế", "Hue", null, null });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "id", "created_at", "created_by", "deleted_at", "deleted_by", "isDeleted", "name", "permalink", "updated_at", "updated_by" },
                values: new object[] { 2, new DateTime(2020, 5, 12, 20, 23, 1, 323, DateTimeKind.Local).AddTicks(2572), null, null, null, false, "Sài Gòn", "Sai_Gon", null, null });

            migrationBuilder.InsertData(
                table: "Positions",
                columns: new[] { "id", "created_at", "created_by", "deleted_at", "deleted_by", "isDeleted", "name", "permalink", "updated_at", "updated_by" },
                values: new object[] { 1, new DateTime(2020, 5, 12, 20, 23, 1, 325, DateTimeKind.Local).AddTicks(6861), null, null, null, false, "Nhân Viên", null, null, null });

            migrationBuilder.InsertData(
                table: "Shops",
                columns: new[] { "id", "CitiesId", "created_at", "created_by", "deleted_at", "deleted_by", "images", "info", "isDeleted", "name", "permalink", "status", "time_close", "time_open", "updated_at", "updated_by" },
                values: new object[] { 1, 1, new DateTime(2020, 5, 12, 20, 23, 1, 326, DateTimeKind.Local).AddTicks(492), null, null, null, null, null, false, "The One", null, 0, new DateTime(2020, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null });

            migrationBuilder.InsertData(
                table: "Shops",
                columns: new[] { "id", "CitiesId", "created_at", "created_by", "deleted_at", "deleted_by", "images", "info", "isDeleted", "name", "permalink", "status", "time_close", "time_open", "updated_at", "updated_by" },
                values: new object[] { 2, 1, new DateTime(2020, 5, 12, 20, 23, 1, 329, DateTimeKind.Local).AddTicks(5099), null, null, null, null, null, false, "HighLand", null, 0, new DateTime(2020, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null });

            migrationBuilder.InsertData(
                table: "Cataloges",
                columns: new[] { "id", "ShopsId", "created_at", "created_by", "deleted_at", "deleted_by", "isDeleted", "name", "permalink", "updated_at", "updated_by" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2020, 5, 12, 20, 23, 1, 330, DateTimeKind.Local).AddTicks(8103), null, null, null, false, "Coffee", "Coffee", null, null },
                    { 2, 1, new DateTime(2020, 5, 12, 20, 23, 1, 331, DateTimeKind.Local).AddTicks(1509), null, null, null, false, "MilkTea", "MilkTea", null, null },
                    { 3, 1, new DateTime(2020, 5, 12, 20, 23, 1, 331, DateTimeKind.Local).AddTicks(1578), null, null, null, false, "Food", "Food", null, null },
                    { 4, 2, new DateTime(2020, 5, 12, 20, 23, 1, 331, DateTimeKind.Local).AddTicks(1581), null, null, null, false, "MilkTea", "MilkTea", null, null }
                });

            migrationBuilder.InsertData(
                table: "Floors",
                columns: new[] { "id", "ShopsId", "created_at", "created_by", "deleted_at", "deleted_by", "isDeleted", "name", "permalink", "updated_at", "updated_by" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2020, 5, 12, 20, 23, 1, 329, DateTimeKind.Local).AddTicks(7295), null, null, null, false, "Tầng 1", "Tang_1", null, null },
                    { 2, 1, new DateTime(2020, 5, 12, 20, 23, 1, 330, DateTimeKind.Local).AddTicks(857), null, null, null, false, "Tầng 2", "Tang_2", null, null },
                    { 3, 2, new DateTime(2020, 5, 12, 20, 23, 1, 330, DateTimeKind.Local).AddTicks(927), null, null, null, false, "Tầng 1", "Tang_1", null, null }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "id", "CatalogesId", "created_at", "created_by", "deleted_at", "deleted_by", "images", "isDeleted", "name", "permalink", "price", "unit", "updated_at", "updated_by" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2020, 5, 12, 20, 23, 1, 331, DateTimeKind.Local).AddTicks(3224), null, null, null, null, false, "Cà phê đen", null, 10000m, null, null, null },
                    { 2, 1, new DateTime(2020, 5, 12, 20, 23, 1, 331, DateTimeKind.Local).AddTicks(6936), null, null, null, null, false, "Cà phê sữa", null, 12000m, null, null, null },
                    { 3, 2, new DateTime(2020, 5, 12, 20, 23, 1, 331, DateTimeKind.Local).AddTicks(6993), null, null, null, null, false, "Trà sữa socola", null, 18000m, null, null, null },
                    { 4, 2, new DateTime(2020, 5, 12, 20, 23, 1, 331, DateTimeKind.Local).AddTicks(6996), null, null, null, null, false, "Trà sữa matcha", null, 20000m, null, null, null },
                    { 5, 3, new DateTime(2020, 5, 12, 20, 23, 1, 331, DateTimeKind.Local).AddTicks(6999), null, null, null, null, false, "Khoai tây chiên", null, 15000m, null, null, null },
                    { 6, 4, new DateTime(2020, 5, 12, 20, 23, 1, 331, DateTimeKind.Local).AddTicks(7001), null, null, null, null, false, "Cà phê đen", null, 10000m, null, null, null }
                });

            migrationBuilder.InsertData(
                table: "Tables",
                columns: new[] { "id", "FloorsId", "created_at", "created_by", "deleted_at", "deleted_by", "isDeleted", "name", "permalink", "status", "updated_at", "updated_by" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2020, 5, 12, 20, 23, 1, 330, DateTimeKind.Local).AddTicks(2845), null, null, null, false, "Bàn 1", "Ban_1", 0, null, null },
                    { 2, 1, new DateTime(2020, 5, 12, 20, 23, 1, 330, DateTimeKind.Local).AddTicks(6334), null, null, null, false, "Bàn 2", "Ban_2", 0, null, null },
                    { 3, 2, new DateTime(2020, 5, 12, 20, 23, 1, 330, DateTimeKind.Local).AddTicks(6390), null, null, null, false, "Bàn 3", "Ban_3", 0, null, null },
                    { 4, 3, new DateTime(2020, 5, 12, 20, 23, 1, 330, DateTimeKind.Local).AddTicks(6393), null, null, null, false, "Bàn 1", "Ban_1", 0, null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_UsersId",
                table: "Accounts",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_BillDetails_BillsId",
                table: "BillDetails",
                column: "BillsId");

            migrationBuilder.CreateIndex(
                name: "IX_BillDetails_ProductsId",
                table: "BillDetails",
                column: "ProductsId");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_TablesId",
                table: "Bills",
                column: "TablesId");

            migrationBuilder.CreateIndex(
                name: "IX_Cataloges_ShopsId",
                table: "Cataloges",
                column: "ShopsId");

            migrationBuilder.CreateIndex(
                name: "IX_Floors_ShopsId",
                table: "Floors",
                column: "ShopsId");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionDetails_PermissionsId",
                table: "PermissionDetails",
                column: "PermissionsId");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionDetails_UsersId",
                table: "PermissionDetails",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_GroupsId",
                table: "Permissions",
                column: "GroupsId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CatalogesId",
                table: "Products",
                column: "CatalogesId");

            migrationBuilder.CreateIndex(
                name: "IX_Shops_CitiesId",
                table: "Shops",
                column: "CitiesId");

            migrationBuilder.CreateIndex(
                name: "IX_Tables_FloorsId",
                table: "Tables",
                column: "FloorsId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_PositionsId",
                table: "Users",
                column: "PositionsId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ShopsId",
                table: "Users",
                column: "ShopsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "BillDetails");

            migrationBuilder.DropTable(
                name: "Menu");

            migrationBuilder.DropTable(
                name: "PermissionDetails");

            migrationBuilder.DropTable(
                name: "TypeMoneys");

            migrationBuilder.DropTable(
                name: "Units");

            migrationBuilder.DropTable(
                name: "Bills");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Tables");

            migrationBuilder.DropTable(
                name: "Cataloges");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Positions");

            migrationBuilder.DropTable(
                name: "Floors");

            migrationBuilder.DropTable(
                name: "Shops");

            migrationBuilder.DropTable(
                name: "Cities");
        }
    }
}
