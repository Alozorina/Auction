using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class UpdateItemProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotosId",
                table: "Items");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndSaleDate",
                table: "Items",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartSaleDate",
                table: "Items",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndSaleDate", "StartSaleDate" },
                values: new object[] { new DateTime(2022, 8, 15, 22, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 7, 22, 12, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "EndSaleDate", "StartSaleDate" },
                values: new object[] { new DateTime(2022, 8, 15, 12, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 8, 12, 10, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndSaleDate",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "StartSaleDate",
                table: "Items");

            migrationBuilder.AddColumn<int>(
                name: "PhotosId",
                table: "Items",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
