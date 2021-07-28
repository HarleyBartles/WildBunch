using Microsoft.EntityFrameworkCore.Migrations;

namespace WildBunch.Data.Migrations
{
    public partial class inventoryObjectSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CharacterName",
                table: "tblGame");

            migrationBuilder.AddColumn<string>(
                name: "CharacterId",
                table: "tblGame",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "tblCharacter",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "tblInventoryObject",
                columns: new[] { "InventoryObjectId", "BasePrice", "Name" },
                values: new object[,]
                {
                    { 1, 55.0, "Horse" },
                    { 2, 28.0, "Saddle" },
                    { 3, 35.0, "Colt .44" },
                    { 4, 5.0, "Colt .44 Bullets" },
                    { 5, 35.0, "Colt .45" },
                    { 6, 10.0, "Colt .45 Bullets" },
                    { 7, 40.0, "Winchester Rifle" },
                    { 8, 15.0, "Winchester Bullets" },
                    { 9, 10.0, "Knife" },
                    { 10, 2.0, "Food for 1 day" },
                    { 11, 3.0, "Horse Food for 1 day" },
                    { 12, 3.0, "Canteen of Water" },
                    { 13, 15.0, "Great Coat" },
                    { 14, 2.0, "Whiskey" },
                    { 15, 8.0, "Blanket" },
                    { 16, 4.0, "Snakebite Medicine" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblGame_CharacterId",
                table: "tblGame",
                column: "CharacterId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblGame_tblCharacter_CharacterId",
                table: "tblGame",
                column: "CharacterId",
                principalTable: "tblCharacter",
                principalColumn: "CharacterId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblGame_tblCharacter_CharacterId",
                table: "tblGame");

            migrationBuilder.DropIndex(
                name: "IX_tblGame_CharacterId",
                table: "tblGame");

            migrationBuilder.DeleteData(
                table: "tblInventoryObject",
                keyColumn: "InventoryObjectId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "tblInventoryObject",
                keyColumn: "InventoryObjectId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "tblInventoryObject",
                keyColumn: "InventoryObjectId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "tblInventoryObject",
                keyColumn: "InventoryObjectId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "tblInventoryObject",
                keyColumn: "InventoryObjectId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "tblInventoryObject",
                keyColumn: "InventoryObjectId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "tblInventoryObject",
                keyColumn: "InventoryObjectId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "tblInventoryObject",
                keyColumn: "InventoryObjectId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "tblInventoryObject",
                keyColumn: "InventoryObjectId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "tblInventoryObject",
                keyColumn: "InventoryObjectId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "tblInventoryObject",
                keyColumn: "InventoryObjectId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "tblInventoryObject",
                keyColumn: "InventoryObjectId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "tblInventoryObject",
                keyColumn: "InventoryObjectId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "tblInventoryObject",
                keyColumn: "InventoryObjectId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "tblInventoryObject",
                keyColumn: "InventoryObjectId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "tblInventoryObject",
                keyColumn: "InventoryObjectId",
                keyValue: 16);

            migrationBuilder.DropColumn(
                name: "CharacterId",
                table: "tblGame");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "tblCharacter");

            migrationBuilder.AddColumn<string>(
                name: "CharacterName",
                table: "tblGame",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
