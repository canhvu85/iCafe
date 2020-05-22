using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Test_coffe.Migrations
{
    public partial class up2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "icon",
                table: "Menu",
                maxLength: 50,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Cataloges",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2020, 5, 19, 19, 31, 0, 83, DateTimeKind.Local).AddTicks(7843));

            migrationBuilder.UpdateData(
                table: "Cataloges",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2020, 5, 19, 19, 31, 0, 84, DateTimeKind.Local).AddTicks(1406));

            migrationBuilder.UpdateData(
                table: "Cataloges",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2020, 5, 19, 19, 31, 0, 84, DateTimeKind.Local).AddTicks(1477));

            migrationBuilder.UpdateData(
                table: "Cataloges",
                keyColumn: "id",
                keyValue: 4,
                column: "created_at",
                value: new DateTime(2020, 5, 19, 19, 31, 0, 84, DateTimeKind.Local).AddTicks(1481));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2020, 5, 19, 19, 31, 0, 74, DateTimeKind.Local).AddTicks(8611));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2020, 5, 19, 19, 31, 0, 76, DateTimeKind.Local).AddTicks(6231));

            migrationBuilder.UpdateData(
                table: "Floors",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2020, 5, 19, 19, 31, 0, 82, DateTimeKind.Local).AddTicks(7426));

            migrationBuilder.UpdateData(
                table: "Floors",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2020, 5, 19, 19, 31, 0, 83, DateTimeKind.Local).AddTicks(1281));

            migrationBuilder.UpdateData(
                table: "Floors",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2020, 5, 19, 19, 31, 0, 83, DateTimeKind.Local).AddTicks(1349));

            migrationBuilder.UpdateData(
                table: "Positions",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2020, 5, 19, 19, 31, 0, 78, DateTimeKind.Local).AddTicks(8981));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2020, 5, 19, 19, 31, 0, 84, DateTimeKind.Local).AddTicks(3048));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2020, 5, 19, 19, 31, 0, 84, DateTimeKind.Local).AddTicks(6386));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2020, 5, 19, 19, 31, 0, 84, DateTimeKind.Local).AddTicks(6439));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "id",
                keyValue: 4,
                column: "created_at",
                value: new DateTime(2020, 5, 19, 19, 31, 0, 84, DateTimeKind.Local).AddTicks(6443));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "id",
                keyValue: 5,
                column: "created_at",
                value: new DateTime(2020, 5, 19, 19, 31, 0, 84, DateTimeKind.Local).AddTicks(6445));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "id",
                keyValue: 6,
                column: "created_at",
                value: new DateTime(2020, 5, 19, 19, 31, 0, 84, DateTimeKind.Local).AddTicks(6448));

            migrationBuilder.UpdateData(
                table: "Shops",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2020, 5, 19, 19, 31, 0, 79, DateTimeKind.Local).AddTicks(2265));

            migrationBuilder.UpdateData(
                table: "Shops",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2020, 5, 19, 19, 31, 0, 82, DateTimeKind.Local).AddTicks(5349));

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2020, 5, 19, 19, 31, 0, 83, DateTimeKind.Local).AddTicks(3002));

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2020, 5, 19, 19, 31, 0, 83, DateTimeKind.Local).AddTicks(6209));

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2020, 5, 19, 19, 31, 0, 83, DateTimeKind.Local).AddTicks(6260));

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "id",
                keyValue: 4,
                column: "created_at",
                value: new DateTime(2020, 5, 19, 19, 31, 0, 83, DateTimeKind.Local).AddTicks(6264));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "icon",
                table: "Menu");

            migrationBuilder.UpdateData(
                table: "Cataloges",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2020, 5, 12, 20, 23, 1, 330, DateTimeKind.Local).AddTicks(8103));

            migrationBuilder.UpdateData(
                table: "Cataloges",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2020, 5, 12, 20, 23, 1, 331, DateTimeKind.Local).AddTicks(1509));

            migrationBuilder.UpdateData(
                table: "Cataloges",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2020, 5, 12, 20, 23, 1, 331, DateTimeKind.Local).AddTicks(1578));

            migrationBuilder.UpdateData(
                table: "Cataloges",
                keyColumn: "id",
                keyValue: 4,
                column: "created_at",
                value: new DateTime(2020, 5, 12, 20, 23, 1, 331, DateTimeKind.Local).AddTicks(1581));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2020, 5, 12, 20, 23, 1, 321, DateTimeKind.Local).AddTicks(4232));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2020, 5, 12, 20, 23, 1, 323, DateTimeKind.Local).AddTicks(2572));

            migrationBuilder.UpdateData(
                table: "Floors",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2020, 5, 12, 20, 23, 1, 329, DateTimeKind.Local).AddTicks(7295));

            migrationBuilder.UpdateData(
                table: "Floors",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2020, 5, 12, 20, 23, 1, 330, DateTimeKind.Local).AddTicks(857));

            migrationBuilder.UpdateData(
                table: "Floors",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2020, 5, 12, 20, 23, 1, 330, DateTimeKind.Local).AddTicks(927));

            migrationBuilder.UpdateData(
                table: "Positions",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2020, 5, 12, 20, 23, 1, 325, DateTimeKind.Local).AddTicks(6861));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2020, 5, 12, 20, 23, 1, 331, DateTimeKind.Local).AddTicks(3224));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2020, 5, 12, 20, 23, 1, 331, DateTimeKind.Local).AddTicks(6936));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2020, 5, 12, 20, 23, 1, 331, DateTimeKind.Local).AddTicks(6993));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "id",
                keyValue: 4,
                column: "created_at",
                value: new DateTime(2020, 5, 12, 20, 23, 1, 331, DateTimeKind.Local).AddTicks(6996));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "id",
                keyValue: 5,
                column: "created_at",
                value: new DateTime(2020, 5, 12, 20, 23, 1, 331, DateTimeKind.Local).AddTicks(6999));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "id",
                keyValue: 6,
                column: "created_at",
                value: new DateTime(2020, 5, 12, 20, 23, 1, 331, DateTimeKind.Local).AddTicks(7001));

            migrationBuilder.UpdateData(
                table: "Shops",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2020, 5, 12, 20, 23, 1, 326, DateTimeKind.Local).AddTicks(492));

            migrationBuilder.UpdateData(
                table: "Shops",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2020, 5, 12, 20, 23, 1, 329, DateTimeKind.Local).AddTicks(5099));

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2020, 5, 12, 20, 23, 1, 330, DateTimeKind.Local).AddTicks(2845));

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2020, 5, 12, 20, 23, 1, 330, DateTimeKind.Local).AddTicks(6334));

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2020, 5, 12, 20, 23, 1, 330, DateTimeKind.Local).AddTicks(6390));

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "id",
                keyValue: 4,
                column: "created_at",
                value: new DateTime(2020, 5, 12, 20, 23, 1, 330, DateTimeKind.Local).AddTicks(6393));
        }
    }
}
