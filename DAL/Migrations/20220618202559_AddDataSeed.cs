using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class AddDataSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Auctions_AuctionId",
                table: "Items");

            migrationBuilder.AlterColumn<int>(
                name: "AuctionId",
                table: "Items",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "BirthDate", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "a7043b5b-bede-48d7-9d36-c2f632c40f0f", 0, new DateTime(2000, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "4174f09a-a3c4-4651-a358-ae4b367ade48", "janemail@mail.com", false, "Jane", "Doe", false, null, null, null, null, "123-456-789", false, "cbec5110-5c11-420d-9606-3af0bcf66676", false, null });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "BirthDate", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "618ac316-948e-40de-bfe5-35abb551c95b", 0, new DateTime(2000, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "526ca0e4-560d-4ab7-9b5b-93dc9a56783a", "johnmail@mail.com", false, "John", "Doe", false, null, null, null, null, "143-456-789", false, "ed32f315-adec-4805-b397-f5ac3792dd44", false, null });

            migrationBuilder.InsertData(
                table: "Auctions",
                columns: new[] { "Id", "Description", "EndSaleDate", "Name", "StartSaleDate", "StatusId" },
                values: new object[] { 1, "Following the success of the inaugural edition in March 2021, consignments are now open for the second iteration of Veiling Steve Johnson's Bright Colors auction this June. Running the gamut of the avant-garde from the late 19th century through to artists working today, the sale presents exceptional artworks by artists who dared to innovate and experiment over the course of 150 years.", new DateTime(2022, 7, 1, 20, 0, 0, 0, DateTimeKind.Unspecified), "Steve Johnson's Bright Colors", new DateTime(2022, 6, 27, 14, 0, 0, 0, DateTimeKind.Unspecified), 1 });

            migrationBuilder.InsertData(
                table: "AuctionCategories",
                columns: new[] { "AuctionId", "CategoryId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 }
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "AuctionId", "BuyerId", "CurrentBid", "Description", "Name", "OwnerId", "StartingPrice", "StatusId" },
                values: new object[,]
                {
                    { 1, 1, null, 0m, null, "Blue Marble", "a7043b5b-bede-48d7-9d36-c2f632c40f0f", 50m, 1 },
                    { 2, 1, null, 0m, null, "Revolution", "618ac316-948e-40de-bfe5-35abb551c95b", 60m, 1 }
                });

            migrationBuilder.InsertData(
                table: "ItemCategories",
                columns: new[] { "ItemId", "CategoryId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 2, 1 },
                    { 2, 2 }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Auctions_AuctionId",
                table: "Items",
                column: "AuctionId",
                principalTable: "Auctions",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Auctions_AuctionId",
                table: "Items");

            migrationBuilder.DeleteData(
                table: "AuctionCategories",
                keyColumns: new[] { "AuctionId", "CategoryId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "AuctionCategories",
                keyColumns: new[] { "AuctionId", "CategoryId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "ItemCategories",
                keyColumns: new[] { "ItemId", "CategoryId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "ItemCategories",
                keyColumns: new[] { "ItemId", "CategoryId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "ItemCategories",
                keyColumns: new[] { "ItemId", "CategoryId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "ItemCategories",
                keyColumns: new[] { "ItemId", "CategoryId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "618ac316-948e-40de-bfe5-35abb551c95b");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a7043b5b-bede-48d7-9d36-c2f632c40f0f");

            migrationBuilder.DeleteData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AlterColumn<int>(
                name: "AuctionId",
                table: "Items",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Auctions_AuctionId",
                table: "Items",
                column: "AuctionId",
                principalTable: "Auctions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
