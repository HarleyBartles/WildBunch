using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WildBunch.Data.Migrations
{
    public partial class characterBag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblCharacter",
                columns: table => new
                {
                    CharacterId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    HealthPoints = table.Column<int>(type: "int", nullable: false),
                    Dollars = table.Column<double>(type: "double", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblCharacter", x => x.CharacterId);
                    table.ForeignKey(
                        name: "FK_tblCharacter_tblUser_UserId",
                        column: x => x.UserId,
                        principalTable: "tblUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tblInventoryObject",
                columns: table => new
                {
                    InventoryObjectId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BasePrice = table.Column<double>(type: "double", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblInventoryObject", x => x.InventoryObjectId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tblCharacterBag",
                columns: table => new
                {
                    CharacterBagId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CharacterId = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblCharacterBag", x => x.CharacterBagId);
                    table.ForeignKey(
                        name: "FK_tblCharacterBag_tblCharacter_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "tblCharacter",
                        principalColumn: "CharacterId",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tblBagItem",
                columns: table => new
                {
                    BagItemId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CharacterBagId = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    InventoryObjectId = table.Column<int>(type: "int", nullable: false),
                    QuantityCarried = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblBagItem", x => x.BagItemId);
                    table.ForeignKey(
                        name: "FK_tblBagItem_tblCharacterBag_CharacterBagId",
                        column: x => x.CharacterBagId,
                        principalTable: "tblCharacterBag",
                        principalColumn: "CharacterBagId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblBagItem_tblInventoryObject_InventoryObjectId",
                        column: x => x.InventoryObjectId,
                        principalTable: "tblInventoryObject",
                        principalColumn: "InventoryObjectId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_tblBagItem_CharacterBagId",
                table: "tblBagItem",
                column: "CharacterBagId");

            migrationBuilder.CreateIndex(
                name: "IX_tblBagItem_InventoryObjectId",
                table: "tblBagItem",
                column: "InventoryObjectId");

            migrationBuilder.CreateIndex(
                name: "IX_tblCharacter_UserId",
                table: "tblCharacter",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_tblCharacterBag_CharacterId",
                table: "tblCharacterBag",
                column: "CharacterId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblBagItem");

            migrationBuilder.DropTable(
                name: "tblCharacterBag");

            migrationBuilder.DropTable(
                name: "tblInventoryObject");

            migrationBuilder.DropTable(
                name: "tblCharacter");
        }
    }
}
