using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class UpdatedUsersPasswordsAndItemStatuses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 7,
                column: "StartSaleDate",
                value: new DateTime(2023, 8, 9, 11, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "OnApproval");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$JWv4B7tq4YQiTaAI1dz47.kMnqCcJPL9d1R1TlWlsXyPOn6271PNe");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "$2a$11$ki6XQ7oAs4XqO.MoWdv6He9iCtYfsa.YutDzV/lgHxQSUmHkrsLUa");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 6,
                column: "Password",
                value: "$2a$11$xQ0LSLPRNqoGkE4cD0.o..XKrhgIvICD1PxyYhypFfMhmOnLjG7s6");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 11,
                column: "Password",
                value: "$2a$11$9LHSXUXlE5sGwIhfzTrnx.dJ0zcJEJBXMhbofOAJ0XewcWCSy6byy");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 7,
                column: "StartSaleDate",
                value: new DateTime(2022, 8, 9, 11, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "On Approval");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$J03pGeqUE6CaD5YnAgH2/.a5YTtAzutzJj8WHBzu7wpe4a9iOswl6");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "$2a$11$aNSJVlkVUD.Pey9VMhjWa.nvu2xrWWyHEG8.u00rE//FuChtVQoZO");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 6,
                column: "Password",
                value: "$2a$11$MGdcEAp/9uHaOzXq7ytxU.2QUYiZviot2cqOaCo.glRWO9wYQo/UC");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 11,
                column: "Password",
                value: "$2a$11$YHH96qbt92vvatfgjvt2huT2oAx7KazrnylNXzbeAT50issPy7HLa");
        }
    }
}
