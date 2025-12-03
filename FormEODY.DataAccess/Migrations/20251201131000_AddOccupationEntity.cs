using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FormEODY.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddOccupationEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Occupations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Occupations", x => x.Id);
                });

            migrationBuilder.AddColumn<int>(
                name: "OccupationId",
                table: "Applications",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Applications_OccupationId",
                table: "Applications",
                column: "OccupationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_Occupations_OccupationId",
                table: "Applications",
                column: "OccupationId",
                principalTable: "Occupations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_Occupations_OccupationId",
                table: "Applications");

            migrationBuilder.DropIndex(
                name: "IX_Applications_OccupationId",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "OccupationId",
                table: "Applications");

            migrationBuilder.DropTable(
                name: "Occupations");
        }
    }
}
