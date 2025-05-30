using Microsoft.EntityFrameworkCore.Migrations;

namespace CarAdsWebApp.DataAccess.Migrations
{
    public partial class UpdateMessage_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AdvertisementId",
                table: "Messages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Messages_AdvertisementId",
                table: "Messages",
                column: "AdvertisementId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Advertisements_AdvertisementId",
                table: "Messages",
                column: "AdvertisementId",
                principalTable: "Advertisements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Advertisements_AdvertisementId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_AdvertisementId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "AdvertisementId",
                table: "Messages");
        }
    }
}
