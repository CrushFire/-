using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Congratulations.Migrations
{
    /// <inheritdoc />
    public partial class AgeAndConstraintsAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "birthdays",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddCheckConstraint(
                name: "ValidDate",
                table: "birthdays",
                sql: "[date] BETWEEN '1900-01-01' AND '2025-01-01'");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "ValidDate",
                table: "birthdays");

            migrationBuilder.DropColumn(
                name: "Age",
                table: "birthdays");
        }
    }
}
