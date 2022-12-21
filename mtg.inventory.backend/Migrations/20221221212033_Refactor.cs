using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace mtginventorybackend.Migrations
{
    /// <inheritdoc />
    public partial class Refactor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Folder_Collection_collectionId",
                table: "Folder");

            migrationBuilder.DropTable(
                name: "CardDeck");

            migrationBuilder.DropTable(
                name: "MetadataCard");

            migrationBuilder.DropTable(
                name: "Card");

            migrationBuilder.DeleteData(
                table: "Folder",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Collection",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.RenameColumn(
                name: "password",
                table: "User",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "User",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "username",
                table: "User",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Folder",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "Folder",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "collectionId",
                table: "Folder",
                newName: "CollectionId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Folder",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Folder_collectionId",
                table: "Folder",
                newName: "IX_Folder_CollectionId");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Deck",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "Deck",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Deck",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Collection",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "Collection",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Collection",
                newName: "Id");

            migrationBuilder.CreateTable(
                name: "CardMetadata",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Lang = table.Column<string>(type: "text", nullable: true),
                    Layout = table.Column<string>(type: "text", nullable: true),
                    ImageUrlSmall = table.Column<string>(name: "ImageUrl_Small", type: "text", nullable: false),
                    ImageUrlBig = table.Column<string>(name: "ImageUrl_Big", type: "text", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: true),
                    Colors = table.Column<List<string>>(type: "text[]", nullable: true),
                    Keywords = table.Column<List<string>>(type: "text[]", nullable: true),
                    Coloridentity = table.Column<List<string>>(name: "Color_identity", type: "text[]", nullable: true),
                    Manacost = table.Column<string>(name: "Mana_cost", type: "text", nullable: true),
                    Oracletext = table.Column<string>(name: "Oracle_text", type: "text", nullable: true),
                    Power = table.Column<int>(type: "integer", nullable: true),
                    Toughness = table.Column<int>(type: "integer", nullable: true),
                    Manacosttotal = table.Column<int>(name: "Mana_cost_total", type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardMetadata", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FolderCard",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CardMetadataId = table.Column<string>(type: "text", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    FolderId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FolderCard", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Folder",
                        column: x => x.FolderId,
                        principalTable: "Folder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeckCards",
                columns: table => new
                {
                    CardsId = table.Column<int>(type: "integer", nullable: false),
                    DecksId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeckCards", x => new { x.CardsId, x.DecksId });
                    table.ForeignKey(
                        name: "FK_DeckCards_Deck_DecksId",
                        column: x => x.DecksId,
                        principalTable: "Deck",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeckCards_FolderCard_CardsId",
                        column: x => x.CardsId,
                        principalTable: "FolderCard",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeckCards_DecksId",
                table: "DeckCards",
                column: "DecksId");

            migrationBuilder.CreateIndex(
                name: "IX_FolderCard_FolderId",
                table: "FolderCard",
                column: "FolderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Collection",
                table: "Folder",
                column: "CollectionId",
                principalTable: "Collection",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collection",
                table: "Folder");

            migrationBuilder.DropTable(
                name: "CardMetadata");

            migrationBuilder.DropTable(
                name: "DeckCards");

            migrationBuilder.DropTable(
                name: "FolderCard");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "User",
                newName: "password");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "User",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "User",
                newName: "username");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Folder",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Folder",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "CollectionId",
                table: "Folder",
                newName: "collectionId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Folder",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_Folder_CollectionId",
                table: "Folder",
                newName: "IX_Folder_collectionId");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Deck",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Deck",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Deck",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Collection",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Collection",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Collection",
                newName: "id");

            migrationBuilder.CreateTable(
                name: "Card",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MetadataCardId = table.Column<string>(type: "text", nullable: true),
                    MetadataID = table.Column<string>(type: "text", nullable: false),
                    folderId = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false)
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
                name: "MetadataCard",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    coloridentity = table.Column<string>(name: "color_identity", type: "text", nullable: true),
                    colors = table.Column<List<string>>(type: "text[]", nullable: true),
                    imageUrlBig = table.Column<string>(name: "imageUrl_Big", type: "text", nullable: false),
                    imageUrlSmall = table.Column<string>(name: "imageUrl_Small", type: "text", nullable: false),
                    keywords = table.Column<List<string>>(type: "text[]", nullable: true),
                    lang = table.Column<string>(type: "text", nullable: true),
                    layout = table.Column<string>(type: "text", nullable: true),
                    manacost = table.Column<string>(name: "mana_cost", type: "text", nullable: true),
                    manacosttotal = table.Column<int>(name: "mana_cost_total", type: "integer", nullable: true),
                    name = table.Column<string>(type: "text", nullable: false),
                    oracletext = table.Column<string>(name: "oracle_text", type: "text", nullable: true),
                    power = table.Column<int>(type: "integer", nullable: true),
                    toughness = table.Column<int>(type: "integer", nullable: true),
                    type = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MetadataCard", x => x.id);
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

            migrationBuilder.InsertData(
                table: "Collection",
                columns: new[] { "id", "description", "name" },
                values: new object[] { 1, "This is an automatically created default collection", "Default Collection" });

            migrationBuilder.InsertData(
                table: "Folder",
                columns: new[] { "id", "collectionId", "description", "name" },
                values: new object[] { 1, 1, "A default folder", "Default Folder" });

            migrationBuilder.InsertData(
                table: "Card",
                columns: new[] { "id", "MetadataCardId", "MetadataID", "folderId", "name", "quantity" },
                values: new object[] { 1, null, "2a46af75-3880-4141-b26e-19834d67e7a8", 1, "Backup Agent", 2 });

            migrationBuilder.CreateIndex(
                name: "IX_Card_folderId",
                table: "Card",
                column: "folderId");

            migrationBuilder.CreateIndex(
                name: "IX_CardDeck_decksid",
                table: "CardDeck",
                column: "decksid");

            migrationBuilder.AddForeignKey(
                name: "FK_Folder_Collection_collectionId",
                table: "Folder",
                column: "collectionId",
                principalTable: "Collection",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
