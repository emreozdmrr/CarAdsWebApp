using Microsoft.EntityFrameworkCore.Migrations;

namespace CarAdsWebApp.DataAccess.Migrations
{
    public partial class MessageUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_AppUsers_ReceivedId",
                table: "Messages");

            migrationBuilder.RenameColumn(
                name: "ReceivedId",
                table: "Messages",
                newName: "ReceiverId");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_ReceivedId",
                table: "Messages",
                newName: "IX_Messages_ReceiverId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_AppUsers_ReceiverId",
                table: "Messages",
                column: "ReceiverId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_AppUsers_ReceiverId",
                table: "Messages");

            migrationBuilder.RenameColumn(
                name: "ReceiverId",
                table: "Messages",
                newName: "ReceivedId");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_ReceiverId",
                table: "Messages",
                newName: "IX_Messages_ReceivedId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_AppUsers_ReceivedId",
                table: "Messages",
                column: "ReceivedId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
