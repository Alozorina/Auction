using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class AddItemPhotos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "ItemPhoto");

            migrationBuilder.UpdateData(
                table: "ItemPhoto",
                keyColumn: "Id",
                keyValue: 1,
                column: "Path",
                value: "steve-johnson-unsplash.jpg");

            migrationBuilder.UpdateData(
                table: "ItemPhoto",
                keyColumn: "Id",
                keyValue: 2,
                column: "Path",
                value: "pexels-steve-johnson-1840624.jpg");

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "BuyerId", "CurrentBid", "Description", "EndSaleDate", "Name", "OwnerId", "StartSaleDate", "StartingPrice", "StatusId" },
                values: new object[,]
                {
                    { 3, null, 0m, null, new DateTime(2022, 8, 15, 12, 0, 0, 0, DateTimeKind.Unspecified), "Sunset", 2, new DateTime(2022, 7, 10, 10, 0, 0, 0, DateTimeKind.Unspecified), 30m, 1 },
                    { 4, 11, 20m, null, new DateTime(2022, 8, 3, 12, 0, 0, 0, DateTimeKind.Unspecified), "Spinning Around", 6, new DateTime(2022, 6, 14, 10, 0, 0, 0, DateTimeKind.Unspecified), 5m, 5 },
                    { 5, null, 0m, null, new DateTime(2022, 8, 10, 12, 0, 0, 0, DateTimeKind.Unspecified), "Antarctica Is Changing", 6, new DateTime(2022, 7, 9, 10, 0, 0, 0, DateTimeKind.Unspecified), 60m, 4 }
                });

            migrationBuilder.InsertData(
                table: "ItemPhoto",
                columns: new[] { "Id", "ItemId", "Path" },
                values: new object[,]
                {
                    { 3, 3, "pexels-steve-johnson-1174000.jpg" },
                    { 4, 4, "pexels-steve-johnson-1286632.jpg" },
                    { 5, 5, "steve-johnson-RzykwoNjoLw-unsplash.jpg" },
                    { 6, 5, "steve-johnson-RzykwoNjoLw-unsplash-mockup.jpg" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ItemPhoto",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ItemPhoto",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ItemPhoto",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ItemPhoto",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "ItemPhoto",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "ItemPhoto",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Name", "Path" },
                values: new object[] { "steve-johnson-unsplash.jpg", "/images/" });

            migrationBuilder.UpdateData(
                table: "ItemPhoto",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Name", "Path" },
                values: new object[] { "pexels-steve-johnson-1840624.jpg", "/images/" });
        }
    }
}
