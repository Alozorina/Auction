using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class ChangePasswordLength : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "User",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(32)",
                oldMaxLength: 32);

            migrationBuilder.InsertData(
                table: "ItemCategories",
                columns: new[] { "Id", "CategoryId", "ItemId" },
                values: new object[,]
                {
                    { 5, 2, 3 },
                    { 6, 2, 4 },
                    { 7, 2, 5 },
                    { 8, 1, 2 },
                    { 9, 1, 1 },
                    { 10, 1, 3 },
                    { 11, 1, 4 },
                    { 12, 1, 5 }
                });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndSaleDate", "StartSaleDate" },
                values: new object[] { new DateTime(2023, 8, 15, 22, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 5, 22, 12, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "EndSaleDate", "StartSaleDate" },
                values: new object[] { new DateTime(2023, 8, 15, 12, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 12, 12, 10, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "EndSaleDate", "StartSaleDate" },
                values: new object[] { new DateTime(2023, 8, 15, 12, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 3, 9, 10, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Upcoming");

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "BirthDate", "Email", "FirstName", "LastName", "Password", "RoleId" },
                values: new object[,]
                {
                    { 6, new DateTime(1980, 2, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "peter@mail.com", "Peter", "Choi", "password123", 1 },
                    { 11, new DateTime(1997, 8, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "dana@mail.com", "Dana", "Meng", "password123", 1 }
                });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 5,
                column: "EndSaleDate",
                value: new DateTime(2023, 8, 10, 12, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 6,
                column: "EndSaleDate",
                value: new DateTime(2023, 8, 10, 12, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 7,
                column: "EndSaleDate",
                value: new DateTime(2023, 8, 19, 12, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ItemCategories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ItemCategories",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ItemCategories",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ItemCategories",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "ItemCategories",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "ItemCategories",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "ItemCategories",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "ItemCategories",
                keyColumn: "Id",
                keyValue: 12);

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
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "User",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: false,
                oldClrType: typeof(string));

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

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "EndSaleDate", "StartSaleDate" },
                values: new object[] { new DateTime(2022, 8, 15, 12, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 7, 9, 10, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "BuyerId", "CreatedBy", "CurrentBid", "Description", "EndSaleDate", "Name", "OwnerId", "StartSaleDate", "StartingPrice", "StatusId" },
                values: new object[,]
                {
                    { 7, null, "Pawel Czerwinski", 0m, null, new DateTime(2022, 8, 19, 12, 0, 0, 0, DateTimeKind.Unspecified), "Black Gold", 11, new DateTime(2022, 8, 9, 11, 0, 0, 0, DateTimeKind.Unspecified), 60m, 2 },
                    { 6, null, "Jesse Zheng", 70m, null, new DateTime(2022, 8, 10, 12, 0, 0, 0, DateTimeKind.Unspecified), "Green Hills", 6, new DateTime(2022, 7, 9, 10, 0, 0, 0, DateTimeKind.Unspecified), 60m, 4 },
                    { 5, null, "Steve Johnson", 70m, "Following the success of the inaugural edition in March 2021, consignments are now open for the second iteration of Veiling Steve Johnson's Bright Colors auction this July. Running the gamut of the avant-garde from the late 19th century through to artists working today, the sale presents exceptional artworks by artists who dared to innovate and experiment over the course of 150 years.", new DateTime(2022, 8, 10, 12, 0, 0, 0, DateTimeKind.Unspecified), "Antarctica Is Changing", 6, new DateTime(2022, 7, 9, 10, 0, 0, 0, DateTimeKind.Unspecified), 60m, 4 },
                    { 4, 11, "Steve Johnson", 20m, "Following the success of the inaugural edition in March 2021, consignments are now open for the second iteration of Veiling Steve Johnson's Bright Colors auction this July. Running the gamut of the avant-garde from the late 19th century through to artists working today, the sale presents exceptional artworks by artists who dared to innovate and experiment over the course of 150 years.", new DateTime(2022, 8, 3, 12, 0, 0, 0, DateTimeKind.Unspecified), "Spinning Around", 6, new DateTime(2022, 6, 14, 10, 0, 0, 0, DateTimeKind.Unspecified), 5m, 5 }
                });

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Approved");

            migrationBuilder.InsertData(
                table: "ItemPhoto",
                columns: new[] { "Id", "ItemId", "Path" },
                values: new object[,]
                {
                    { 8, 7, "pawel-czerwinski-xubOAAKUwXc-unsplash.jpg" },
                    { 7, 6, "pexels-jesse-zheng-732548.jpg" },
                    { 6, 5, "steve-johnson-RzykwoNjoLw-unsplash-mockup.jpg" },
                    { 5, 5, "steve-johnson-RzykwoNjoLw-unsplash.jpg" },
                    { 4, 4, "pexels-steve-johnson-1286632.jpg" }
                });
        }
    }
}
