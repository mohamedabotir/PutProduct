using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PutProduct.Data.Migrations
{
    public partial class product : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Products");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Category",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
