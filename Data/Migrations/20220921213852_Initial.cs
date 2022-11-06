using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Statuses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    LastName = table.Column<string>(maxLength: 50, nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    RoleId = table.Column<int>(nullable: false),
                    BirthDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 80, nullable: false),
                    Description = table.Column<string>(maxLength: 1000, nullable: true),
                    StartingPrice = table.Column<decimal>(nullable: false),
                    CurrentBid = table.Column<decimal>(nullable: false),
                    StatusId = table.Column<int>(nullable: false),
                    StartSaleDate = table.Column<DateTime>(nullable: false),
                    EndSaleDate = table.Column<DateTime>(nullable: false),
                    OwnerId = table.Column<int>(nullable: false),
                    BuyerId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Items_User_BuyerId",
                        column: x => x.BuyerId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Items_User_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Items_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<int>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemCategories_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemPhoto",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Path = table.Column<string>(nullable: false),
                    ItemId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemPhoto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemPhoto_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Painting" },
                    { 2, "Steve Johnson" },
                    { 3, "Sculpture" },
                    { 4, "Ceramics" },
                    { 5, "Chinese Art" },
                    { 6, "Porcelain" },
                    { 7, "Jewelry" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "User" },
                    { 2, "Admin" }
                });

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "On Approval" },
                    { 2, "Upcoming" },
                    { 3, "Rejected" },
                    { 4, "Open" },
                    { 5, "Closed" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "BirthDate", "Email", "FirstName", "LastName", "Password", "RoleId" },
                values: new object[,]
                {
                    { 2, new DateTime(2000, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "johnmail@mail.com", "John", "Doe", "$2a$11$aNSJVlkVUD.Pey9VMhjWa.nvu2xrWWyHEG8.u00rE//FuChtVQoZO", 1 },
                    { 6, new DateTime(1980, 2, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "peter@mail.com", "Peter", "Choi", "$2a$11$MGdcEAp/9uHaOzXq7ytxU.2QUYiZviot2cqOaCo.glRWO9wYQo/UC", 1 },
                    { 11, new DateTime(1997, 8, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "dana@mail.com", "Dana", "Meng", "$2a$11$YHH96qbt92vvatfgjvt2huT2oAx7KazrnylNXzbeAT50issPy7HLa", 1 },
                    { 1, new DateTime(2000, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "janemail@mail.com", "Jane", "Doe", "$2a$11$J03pGeqUE6CaD5YnAgH2/.a5YTtAzutzJj8WHBzu7wpe4a9iOswl6", 2 }
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "BuyerId", "CreatedBy", "CurrentBid", "Description", "EndSaleDate", "Name", "OwnerId", "StartSaleDate", "StartingPrice", "StatusId" },
                values: new object[,]
                {
                    { 2, null, "Steve Johnson", 0m, "Following the success of the inaugural edition in March 2021, consignments are now open for the second iteration of Veiling Steve Johnson's Bright Colors auction this July. Running the gamut of the avant-garde from the late 19th century through to artists working today, the sale presents exceptional artworks by artists who dared to innovate and experiment over the course of 150 years.", new DateTime(2023, 8, 15, 12, 0, 0, 0, DateTimeKind.Unspecified), "Revolution", 2, new DateTime(2022, 12, 12, 10, 0, 0, 0, DateTimeKind.Unspecified), 60m, 2 },
                    { 3, null, "Steve Johnson", 40m, "Following the success of the inaugural edition in March 2021, consignments are now open for the second iteration of Veiling Steve Johnson's Bright Colors auction this July. Running the gamut of the avant-garde from the late 19th century through to artists working today, the sale presents exceptional artworks by artists who dared to innovate and experiment over the course of 150 years.", new DateTime(2023, 8, 15, 12, 0, 0, 0, DateTimeKind.Unspecified), "Sunset", 2, new DateTime(2023, 3, 9, 10, 0, 0, 0, DateTimeKind.Unspecified), 30m, 4 },
                    { 5, null, "Steve Johnson", 70m, "Following the success of the inaugural edition in March 2021, consignments are now open for the second iteration of Veiling Steve Johnson's Bright Colors auction this July. Running the gamut of the avant-garde from the late 19th century through to artists working today, the sale presents exceptional artworks by artists who dared to innovate and experiment over the course of 150 years.", new DateTime(2023, 8, 10, 12, 0, 0, 0, DateTimeKind.Unspecified), "Antarctica Is Changing", 6, new DateTime(2022, 7, 9, 10, 0, 0, 0, DateTimeKind.Unspecified), 60m, 4 },
                    { 6, null, "Jesse Zheng", 70m, null, new DateTime(2023, 8, 10, 12, 0, 0, 0, DateTimeKind.Unspecified), "Green Hills", 6, new DateTime(2022, 7, 9, 10, 0, 0, 0, DateTimeKind.Unspecified), 60m, 4 },
                    { 4, 11, "Steve Johnson", 20m, "Following the success of the inaugural edition in March 2021, consignments are now open for the second iteration of Veiling Steve Johnson's Bright Colors auction this July. Running the gamut of the avant-garde from the late 19th century through to artists working today, the sale presents exceptional artworks by artists who dared to innovate and experiment over the course of 150 years.", new DateTime(2022, 8, 3, 12, 0, 0, 0, DateTimeKind.Unspecified), "Spinning Around", 6, new DateTime(2022, 6, 14, 10, 0, 0, 0, DateTimeKind.Unspecified), 5m, 5 },
                    { 7, null, "Pawel Czerwinski", 0m, null, new DateTime(2023, 8, 19, 12, 0, 0, 0, DateTimeKind.Unspecified), "Black Gold", 11, new DateTime(2022, 8, 9, 11, 0, 0, 0, DateTimeKind.Unspecified), 60m, 2 },
                    { 1, null, "Steve Johnson", 0m, "Following the success of the inaugural edition in March 2021, consignments are now open for the second iteration of Veiling Steve Johnson's Bright Colors auction this July. Running the gamut of the avant-garde from the late 19th century through to artists working today, the sale presents exceptional artworks by artists who dared to innovate and experiment over the course of 150 years.", new DateTime(2023, 8, 15, 22, 0, 0, 0, DateTimeKind.Unspecified), "Blue Marble", 1, new DateTime(2023, 5, 22, 12, 0, 0, 0, DateTimeKind.Unspecified), 50m, 2 }
                });

            migrationBuilder.InsertData(
                table: "ItemCategories",
                columns: new[] { "Id", "CategoryId", "ItemId" },
                values: new object[,]
                {
                    { 2, 1, 2 },
                    { 3, 2, 2 },
                    { 8, 1, 2 },
                    { 4, 2, 1 },
                    { 5, 2, 3 },
                    { 10, 1, 3 },
                    { 1, 1, 1 },
                    { 7, 2, 5 },
                    { 12, 1, 5 },
                    { 9, 1, 1 },
                    { 6, 2, 4 },
                    { 11, 1, 4 }
                });

            migrationBuilder.InsertData(
                table: "ItemPhoto",
                columns: new[] { "Id", "ItemId", "Path" },
                values: new object[,]
                {
                    { 8, 7, "pawel-czerwinski-xubOAAKUwXc-unsplash.jpg" },
                    { 4, 4, "pexels-steve-johnson-1286632.jpg" },
                    { 5, 5, "steve-johnson-RzykwoNjoLw-unsplash.jpg" },
                    { 6, 5, "steve-johnson-RzykwoNjoLw-unsplash-mockup.jpg" },
                    { 3, 3, "pexels-steve-johnson-1174000.jpg" },
                    { 2, 2, "pexels-steve-johnson-1840624.jpg" },
                    { 7, 6, "pexels-jesse-zheng-732548.jpg" },
                    { 1, 1, "steve-johnson-unsplash.jpg" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemCategories_CategoryId",
                table: "ItemCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemCategories_ItemId",
                table: "ItemCategories",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemPhoto_ItemId",
                table: "ItemPhoto",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_BuyerId",
                table: "Items",
                column: "BuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_OwnerId",
                table: "Items",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_StatusId",
                table: "Items",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_User_RoleId",
                table: "User",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemCategories");

            migrationBuilder.DropTable(
                name: "ItemPhoto");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Statuses");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
