using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SPOTService.Migrations
{
    /// <inheritdoc />
    public partial class FixFKs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Respondent_GroupId",
                table: "Respondent");

            migrationBuilder.DropIndex(
                name: "IX_Answer_AnswerVariantId",
                table: "Answer");

            migrationBuilder.DropIndex(
                name: "IX_Answer_QuestionId",
                table: "Answer");

            migrationBuilder.DropIndex(
                name: "IX_Answer_RespondentId",
                table: "Answer");

            migrationBuilder.DropIndex(
                name: "IX_Answer_SurveyId",
                table: "Answer");

            migrationBuilder.CreateIndex(
                name: "IX_Respondent_GroupId",
                table: "Respondent",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Answer_AnswerVariantId",
                table: "Answer",
                column: "AnswerVariantId");

            migrationBuilder.CreateIndex(
                name: "IX_Answer_QuestionId",
                table: "Answer",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Answer_RespondentId",
                table: "Answer",
                column: "RespondentId");

            migrationBuilder.CreateIndex(
                name: "IX_Answer_SurveyId",
                table: "Answer",
                column: "SurveyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Respondent_GroupId",
                table: "Respondent");

            migrationBuilder.DropIndex(
                name: "IX_Answer_AnswerVariantId",
                table: "Answer");

            migrationBuilder.DropIndex(
                name: "IX_Answer_QuestionId",
                table: "Answer");

            migrationBuilder.DropIndex(
                name: "IX_Answer_RespondentId",
                table: "Answer");

            migrationBuilder.DropIndex(
                name: "IX_Answer_SurveyId",
                table: "Answer");

            migrationBuilder.CreateIndex(
                name: "IX_Respondent_GroupId",
                table: "Respondent",
                column: "GroupId",
                unique: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_Answer_RespondentId",
                table: "Answer",
                column: "RespondentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Answer_SurveyId",
                table: "Answer",
                column: "SurveyId",
                unique: true);
        }
    }
}
