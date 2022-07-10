using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class AddNewItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Items",
                maxLength: 80,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedBy", "Description", "StatusId" },
                values: new object[] { "Steve Johnson", "Following the success of the inaugural edition in March 2021, consignments are now open for the second iteration of Veiling Steve Johnson's Bright Colors auction this July. Running the gamut of the avant-garde from the late 19th century through to artists working today, the sale presents exceptional artworks by artists who dared to innovate and experiment over the course of 150 years.", 2 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedBy", "Description", "StatusId" },
                values: new object[] { "Steve Johnson", "Following the success of the inaugural edition in March 2021, consignments are now open for the second iteration of Veiling Steve Johnson's Bright Colors auction this July. Running the gamut of the avant-garde from the late 19th century through to artists working today, the sale presents exceptional artworks by artists who dared to innovate and experiment over the course of 150 years.", 2 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedBy", "CurrentBid", "Description", "StartSaleDate", "StatusId" },
                values: new object[] { "Steve Johnson", 40m, "Following the success of the inaugural edition in March 2021, consignments are now open for the second iteration of Veiling Steve Johnson's Bright Colors auction this July. Running the gamut of the avant-garde from the late 19th century through to artists working today, the sale presents exceptional artworks by artists who dared to innovate and experiment over the course of 150 years.", new DateTime(2022, 7, 9, 10, 0, 0, 0, DateTimeKind.Unspecified), 4 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedBy", "Description" },
                values: new object[] { "Steve Johnson", "Following the success of the inaugural edition in March 2021, consignments are now open for the second iteration of Veiling Steve Johnson's Bright Colors auction this July. Running the gamut of the avant-garde from the late 19th century through to artists working today, the sale presents exceptional artworks by artists who dared to innovate and experiment over the course of 150 years." });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedBy", "CurrentBid", "Description" },
                values: new object[] { "Steve Johnson", 70m, "Following the success of the inaugural edition in March 2021, consignments are now open for the second iteration of Veiling Steve Johnson's Bright Colors auction this July. Running the gamut of the avant-garde from the late 19th century through to artists working today, the sale presents exceptional artworks by artists who dared to innovate and experiment over the course of 150 years." });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "BuyerId", "CreatedBy", "CurrentBid", "Description", "EndSaleDate", "Name", "OwnerId", "StartSaleDate", "StartingPrice", "StatusId" },
                values: new object[,]
                {
                    { 6, null, "Jesse Zheng", 70m, null, new DateTime(2022, 8, 10, 12, 0, 0, 0, DateTimeKind.Unspecified), "Green Hills", 6, new DateTime(2022, 7, 9, 10, 0, 0, 0, DateTimeKind.Unspecified), 60m, 4 },
                    { 7, null, "Pawel Czerwinski", 0m, null, new DateTime(2022, 8, 19, 12, 0, 0, 0, DateTimeKind.Unspecified), "Black Gold", 11, new DateTime(2022, 8, 9, 11, 0, 0, 0, DateTimeKind.Unspecified), 60m, 2 }
                });

            migrationBuilder.InsertData(
                table: "ItemPhoto",
                columns: new[] { "Id", "ItemId", "Path" },
                values: new object[] { 7, 6, "pexels-jesse-zheng-732548.jpg" });

            migrationBuilder.InsertData(
                table: "ItemPhoto",
                columns: new[] { "Id", "ItemId", "Path" },
                values: new object[] { 8, 7, "pawel-czerwinski-xubOAAKUwXc-unsplash.jpg" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ItemPhoto",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ItemPhoto",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Items");

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "StatusId" },
                values: new object[] { null, 1 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "StatusId" },
                values: new object[] { null, 1 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CurrentBid", "Description", "StartSaleDate", "StatusId" },
                values: new object[] { 0m, null, new DateTime(2022, 7, 10, 10, 0, 0, 0, DateTimeKind.Unspecified), 1 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 4,
                column: "Description",
                value: null);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CurrentBid", "Description" },
                values: new object[] { 0m, null });
        }
    }
}
