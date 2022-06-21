using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class ChangePrimaryKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemCategories",
                table: "ItemCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AuctionCategories",
                table: "AuctionCategories");

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

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ItemCategories",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "AuctionCategories",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemCategories",
                table: "ItemCategories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AuctionCategories",
                table: "AuctionCategories",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AuctionCategories",
                columns: new[] { "Id", "AuctionId", "CategoryId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 1, 2 }
                });

            migrationBuilder.InsertData(
                table: "ItemCategories",
                columns: new[] { "Id", "CategoryId", "ItemId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 1, 2 },
                    { 3, 2, 2 },
                    { 4, 2, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemCategories_ItemId",
                table: "ItemCategories",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_AuctionCategories_AuctionId",
                table: "AuctionCategories",
                column: "AuctionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemCategories",
                table: "ItemCategories");

            migrationBuilder.DropIndex(
                name: "IX_ItemCategories_ItemId",
                table: "ItemCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AuctionCategories",
                table: "AuctionCategories");

            migrationBuilder.DropIndex(
                name: "IX_AuctionCategories_AuctionId",
                table: "AuctionCategories");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ItemCategories");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "AuctionCategories");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemCategories",
                table: "ItemCategories",
                columns: new[] { "ItemId", "CategoryId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AuctionCategories",
                table: "AuctionCategories",
                columns: new[] { "AuctionId", "CategoryId" });
        }
    }
}
