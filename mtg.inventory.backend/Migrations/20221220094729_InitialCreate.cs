using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace mtginventorybackend.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Collection",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collection", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Deck",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deck", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ScryfallCard",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    lang = table.Column<string>(type: "text", nullable: true),
                    layout = table.Column<string>(type: "text", nullable: true),
                    imageUrlSmall = table.Column<string>(name: "imageUrl_Small", type: "text", nullable: false),
                    imageUrlBig = table.Column<string>(name: "imageUrl_Big", type: "text", nullable: false),
                    type = table.Column<string>(type: "text", nullable: true),
                    colors = table.Column<List<string>>(type: "text[]", nullable: true),
                    keywords = table.Column<List<string>>(type: "text[]", nullable: true),
                    coloridentity = table.Column<string>(name: "color_identity", type: "text", nullable: true),
                    manacost = table.Column<string>(name: "mana_cost", type: "text", nullable: true),
                    oracletext = table.Column<string>(name: "oracle_text", type: "text", nullable: true),
                    power = table.Column<int>(type: "integer", nullable: true),
                    toughness = table.Column<int>(type: "integer", nullable: true),
                    manacosttotal = table.Column<int>(name: "mana_cost_total", type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScryfallCard", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Folder",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    collectionId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Folder", x => x.id);
                    table.ForeignKey(
                        name: "FK_Folder_Collection_collectionId",
                        column: x => x.collectionId,
                        principalTable: "Collection",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Card",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    scryfallID = table.Column<string>(type: "text", nullable: false),
                    scryfallCardId = table.Column<string>(type: "text", nullable: true),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    folderId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Card", x => x.id);
                    table.ForeignKey(
                        name: "FK_Card_Folder_folderId",
                        column: x => x.folderId,
                        principalTable: "Folder",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CardDeck",
                columns: table => new
                {
                    cardsid = table.Column<int>(type: "integer", nullable: false),
                    decksid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardDeck", x => new { x.cardsid, x.decksid });
                    table.ForeignKey(
                        name: "FK_CardDeck_Card_cardsid",
                        column: x => x.cardsid,
                        principalTable: "Card",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CardDeck_Deck_decksid",
                        column: x => x.decksid,
                        principalTable: "Deck",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Card_folderId",
                table: "Card",
                column: "folderId");

            migrationBuilder.CreateIndex(
                name: "IX_CardDeck_decksid",
                table: "CardDeck",
                column: "decksid");

            migrationBuilder.CreateIndex(
                name: "IX_Folder_collectionId",
                table: "Folder",
                column: "collectionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CardDeck");

            migrationBuilder.DropTable(
                name: "ScryfallCard");

            migrationBuilder.DropTable(
                name: "Card");

            migrationBuilder.DropTable(
                name: "Deck");

            migrationBuilder.DropTable(
                name: "Folder");

            migrationBuilder.DropTable(
                name: "Collection");
        }
    }
}
