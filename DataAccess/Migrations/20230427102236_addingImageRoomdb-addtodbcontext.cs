using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class addingImageRoomdbaddtodbcontext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HotelRoomImage_HotelRooms_RoomId",
                table: "HotelRoomImage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HotelRoomImage",
                table: "HotelRoomImage");

            migrationBuilder.RenameTable(
                name: "HotelRoomImage",
                newName: "HotelRoomImages");

            migrationBuilder.RenameIndex(
                name: "IX_HotelRoomImage_RoomId",
                table: "HotelRoomImages",
                newName: "IX_HotelRoomImages_RoomId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HotelRoomImages",
                table: "HotelRoomImages",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HotelRoomImages_HotelRooms_RoomId",
                table: "HotelRoomImages",
                column: "RoomId",
                principalTable: "HotelRooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HotelRoomImages_HotelRooms_RoomId",
                table: "HotelRoomImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HotelRoomImages",
                table: "HotelRoomImages");

            migrationBuilder.RenameTable(
                name: "HotelRoomImages",
                newName: "HotelRoomImage");

            migrationBuilder.RenameIndex(
                name: "IX_HotelRoomImages_RoomId",
                table: "HotelRoomImage",
                newName: "IX_HotelRoomImage_RoomId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HotelRoomImage",
                table: "HotelRoomImage",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HotelRoomImage_HotelRooms_RoomId",
                table: "HotelRoomImage",
                column: "RoomId",
                principalTable: "HotelRooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
