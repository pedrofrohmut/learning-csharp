using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WhiteLagoon.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedVillaNumbers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "villas_numbers",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    number = table.Column<int>(type: "integer", nullable: false),
                    special_details = table.Column<string>(type: "text", nullable: true),
                    villa_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_villas_numbers", x => x.id);
                    table.ForeignKey(
                        name: "fk_villas_numbers_villas_villa_id",
                        column: x => x.villa_id,
                        principalTable: "villas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "villas_numbers",
                columns: new[] { "id", "number", "special_details", "villa_id" },
                values: new object[,]
                {
                    { 1, 101, null, 1 },
                    { 2, 102, null, 1 },
                    { 3, 103, null, 1 },
                    { 4, 104, null, 1 },
                    { 5, 201, null, 2 },
                    { 6, 202, null, 2 },
                    { 7, 203, null, 2 },
                    { 8, 301, null, 3 },
                    { 9, 302, null, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "ix_villas_numbers_number",
                table: "villas_numbers",
                column: "number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_villas_numbers_villa_id",
                table: "villas_numbers",
                column: "villa_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "villas_numbers");
        }
    }
}
