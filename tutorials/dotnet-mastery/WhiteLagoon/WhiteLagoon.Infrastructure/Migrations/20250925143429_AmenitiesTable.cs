using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WhiteLagoon.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AmenitiesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "amenities",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    villa_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_amenities", x => x.id);
                    table.ForeignKey(
                        name: "fk_amenities_villas_villa_id",
                        column: x => x.villa_id,
                        principalTable: "villas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "amenities",
                columns: new[] { "id", "description", "name", "villa_id" },
                values: new object[,]
                {
                    { 1, null, "Private Pool", 1 },
                    { 2, null, "Microwave", 1 },
                    { 3, null, "Private Balcony", 1 },
                    { 4, null, "1 king bed and 1 sofa bed", 1 },
                    { 5, null, "Private Plunge Pool", 2 },
                    { 6, null, "Microwave and Mini Refrigerator", 2 },
                    { 7, null, "Private Balcony", 2 },
                    { 8, null, "king bed or 2 double beds", 2 },
                    { 9, null, "Private Pool", 3 },
                    { 10, null, "Jacuzzi", 3 },
                    { 11, null, "Private Balcony", 3 }
                });

            migrationBuilder.CreateIndex(
                name: "ix_amenities_villa_id",
                table: "amenities",
                column: "villa_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "amenities");
        }
    }
}
