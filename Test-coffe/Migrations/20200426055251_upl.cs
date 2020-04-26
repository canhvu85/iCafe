using Microsoft.EntityFrameworkCore.Migrations;

namespace Test_coffe.Migrations
{
    public partial class upl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "unit",
                table: "Products",
                maxLength: 255,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "unit",
                table: "Products");
        }
    }
}
