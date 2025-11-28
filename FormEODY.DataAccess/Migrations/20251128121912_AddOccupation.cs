using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FormEODY.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddOccupation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Occupation",
                table: "Applications",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Occupation",
                table: "Applications");
        }
    }
}
