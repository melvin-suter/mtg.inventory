using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace mtginventorybackend.Migrations
{
    /// <inheritdoc />
    public partial class Users : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    username = table.Column<string>(type: "text", nullable: false),
                    password = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.id);
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
                columns: new[] { "id", "folderId", "name", "quantity", "MetadataCardId", "MetadataID" },
                values: new object[] { 1, 1, "Backup Agent", 2, null, "2a46af75-3880-4141-b26e-19834d67e7a8" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DeleteData(
                table: "Card",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Folder",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Collection",
                keyColumn: "id",
                keyValue: 1);
        }
    }
}
