using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PutProduct.Data.Migrations
{
    public partial class modifyOrderEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "OrderTime",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "userId",
                table: "Orders",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "userOrderId",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_userId",
                table: "Orders",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_userId",
                table: "Orders",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_userId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_userId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderTime",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "userId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "userOrderId",
                table: "Orders");
        }
    }
}
