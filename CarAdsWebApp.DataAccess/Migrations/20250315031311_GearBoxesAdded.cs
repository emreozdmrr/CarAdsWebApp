using Microsoft.EntityFrameworkCore.Migrations;

namespace CarAdsWebApp.DataAccess.Migrations
{
    public partial class GearBoxesAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "GearBoxes",
                columns: new[] { "Id", "Definition" },
                values: new object[] { 1, "Manuel" });

            migrationBuilder.InsertData(
                table: "GearBoxes",
                columns: new[] { "Id", "Definition" },
                values: new object[] { 2, "Otomatik" });

            migrationBuilder.InsertData(
                table: "GearBoxes",
                columns: new[] { "Id", "Definition" },
                values: new object[] { 3, "Yarı Otomatik" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "GearBoxes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "GearBoxes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "GearBoxes",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
