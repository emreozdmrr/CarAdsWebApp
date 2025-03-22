using Microsoft.EntityFrameworkCore.Migrations;

namespace CarAdsWebApp.DataAccess.Migrations
{
    public partial class BodyTypesAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "BodyTypes",
                columns: new[] { "Id", "Definition" },
                values: new object[,]
                {
                    { 1, "Cabrio" },
                    { 2, "Coupe" },
                    { 3, "Hatchback 3 kapı" },
                    { 4, "Hatchback 5 kapı" },
                    { 5, "Sedan" },
                    { 6, "MPV" },
                    { 7, "Roadster" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 7);
        }
    }
}
