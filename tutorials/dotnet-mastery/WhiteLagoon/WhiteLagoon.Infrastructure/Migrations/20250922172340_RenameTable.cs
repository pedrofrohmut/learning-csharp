using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WhiteLagoon.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_villas_numbers_villas_villa_id",
                table: "villas_numbers");

            migrationBuilder.DropPrimaryKey(
                name: "pk_villas_numbers",
                table: "villas_numbers");

            migrationBuilder.RenameTable(
                name: "villas_numbers",
                newName: "villa_numbers");

            migrationBuilder.RenameIndex(
                name: "ix_villas_numbers_villa_id",
                table: "villa_numbers",
                newName: "ix_villa_numbers_villa_id");

            migrationBuilder.RenameIndex(
                name: "ix_villas_numbers_number",
                table: "villa_numbers",
                newName: "ix_villa_numbers_number");

            migrationBuilder.AddPrimaryKey(
                name: "pk_villa_numbers",
                table: "villa_numbers",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_villa_numbers_villas_villa_id",
                table: "villa_numbers",
                column: "villa_id",
                principalTable: "villas",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_villa_numbers_villas_villa_id",
                table: "villa_numbers");

            migrationBuilder.DropPrimaryKey(
                name: "pk_villa_numbers",
                table: "villa_numbers");

            migrationBuilder.RenameTable(
                name: "villa_numbers",
                newName: "villas_numbers");

            migrationBuilder.RenameIndex(
                name: "ix_villa_numbers_villa_id",
                table: "villas_numbers",
                newName: "ix_villas_numbers_villa_id");

            migrationBuilder.RenameIndex(
                name: "ix_villa_numbers_number",
                table: "villas_numbers",
                newName: "ix_villas_numbers_number");

            migrationBuilder.AddPrimaryKey(
                name: "pk_villas_numbers",
                table: "villas_numbers",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_villas_numbers_villas_villa_id",
                table: "villas_numbers",
                column: "villa_id",
                principalTable: "villas",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
