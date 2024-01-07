using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SPOTService.Migrations
{
    /// <inheritdoc />
    public partial class AddStateId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StateId",
                table: "Respondent",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StateId",
                table: "Respondent");
        }
    }
}
