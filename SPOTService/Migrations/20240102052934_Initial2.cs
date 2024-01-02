using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SPOTService.Migrations
{
    /// <inheritdoc />
    public partial class Initial2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Answer_AnswerVariantId",
                table: "Answer");

            migrationBuilder.DropIndex(
                name: "IX_Answer_QuestionId",
                table: "Answer");

            migrationBuilder.CreateIndex(
                name: "IX_Answer_AnswerVariantId",
                table: "Answer",
                column: "AnswerVariantId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Answer_QuestionId",
                table: "Answer",
                column: "QuestionId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Answer_AnswerVariantId",
                table: "Answer");

            migrationBuilder.DropIndex(
                name: "IX_Answer_QuestionId",
                table: "Answer");

            migrationBuilder.CreateIndex(
                name: "IX_Answer_AnswerVariantId",
                table: "Answer",
                column: "AnswerVariantId");

            migrationBuilder.CreateIndex(
                name: "IX_Answer_QuestionId",
                table: "Answer",
                column: "QuestionId");
        }
    }
}
