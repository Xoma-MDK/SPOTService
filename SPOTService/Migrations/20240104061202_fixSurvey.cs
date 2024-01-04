using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SPOTService.Migrations
{
    /// <inheritdoc />
    public partial class fixSurvey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Survey_GroupId",
                table: "Survey");

            migrationBuilder.DropIndex(
                name: "IX_Survey_UserId",
                table: "Survey");

            migrationBuilder.CreateIndex(
                name: "IX_Survey_GroupId",
                table: "Survey",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Survey_UserId",
                table: "Survey",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Survey_GroupId",
                table: "Survey");

            migrationBuilder.DropIndex(
                name: "IX_Survey_UserId",
                table: "Survey");

            migrationBuilder.CreateIndex(
                name: "IX_Survey_GroupId",
                table: "Survey",
                column: "GroupId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Survey_UserId",
                table: "Survey",
                column: "UserId",
                unique: true);
        }
    }
}
