using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SPOTService.Migrations
{
    /// <inheritdoc />
#pragma warning disable IDE1006 // Стили именования
    public partial class fixSurvey : Migration
#pragma warning restore IDE1006 // Стили именования
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
