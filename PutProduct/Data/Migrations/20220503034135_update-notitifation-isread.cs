using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PutProduct.Data.Migrations
{
    public partial class updatenotitifationisread : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isRead",
                table: "Notifications",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isRead",
                table: "Notifications");
        }
    }
}
