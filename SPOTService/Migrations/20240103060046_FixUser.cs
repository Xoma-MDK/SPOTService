using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SPOTService.Migrations
{
    /// <inheritdoc />
    public partial class FixUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionAnswerVariant_AnswerVariant_AnswerVariantId",
                table: "QuestionAnswerVariant");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionAnswerVariant_Question_QuestionId",
                table: "QuestionAnswerVariant");

            migrationBuilder.DropForeignKey(
                name: "FK_SurveyQuestion_Question_QuestionId",
                table: "SurveyQuestion");

            migrationBuilder.DropForeignKey(
                name: "FK_SurveyQuestion_Survey_SurveyId",
                table: "SurveyQuestion");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SurveyQuestion",
                table: "SurveyQuestion");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuestionAnswerVariant",
                table: "QuestionAnswerVariant");

            migrationBuilder.RenameTable(
                name: "SurveyQuestion",
                newName: "SurveyQuestions");

            migrationBuilder.RenameTable(
                name: "QuestionAnswerVariant",
                newName: "QuestionAnswerVariants");

            migrationBuilder.RenameColumn(
                name: "SurName",
                table: "User",
                newName: "Surname");

            migrationBuilder.RenameIndex(
                name: "IX_SurveyQuestion_SurveyId",
                table: "SurveyQuestions",
                newName: "IX_SurveyQuestions_SurveyId");

            migrationBuilder.RenameIndex(
                name: "IX_SurveyQuestion_QuestionId",
                table: "SurveyQuestions",
                newName: "IX_SurveyQuestions_QuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_QuestionAnswerVariant_QuestionId",
                table: "QuestionAnswerVariants",
                newName: "IX_QuestionAnswerVariants_QuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_QuestionAnswerVariant_AnswerVariantId",
                table: "QuestionAnswerVariants",
                newName: "IX_QuestionAnswerVariants_AnswerVariantId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SurveyQuestions",
                table: "SurveyQuestions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuestionAnswerVariants",
                table: "QuestionAnswerVariants",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionAnswerVariants_AnswerVariant_AnswerVariantId",
                table: "QuestionAnswerVariants",
                column: "AnswerVariantId",
                principalTable: "AnswerVariant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionAnswerVariants_Question_QuestionId",
                table: "QuestionAnswerVariants",
                column: "QuestionId",
                principalTable: "Question",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SurveyQuestions_Question_QuestionId",
                table: "SurveyQuestions",
                column: "QuestionId",
                principalTable: "Question",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SurveyQuestions_Survey_SurveyId",
                table: "SurveyQuestions",
                column: "SurveyId",
                principalTable: "Survey",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionAnswerVariants_AnswerVariant_AnswerVariantId",
                table: "QuestionAnswerVariants");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionAnswerVariants_Question_QuestionId",
                table: "QuestionAnswerVariants");

            migrationBuilder.DropForeignKey(
                name: "FK_SurveyQuestions_Question_QuestionId",
                table: "SurveyQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_SurveyQuestions_Survey_SurveyId",
                table: "SurveyQuestions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SurveyQuestions",
                table: "SurveyQuestions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuestionAnswerVariants",
                table: "QuestionAnswerVariants");

            migrationBuilder.RenameTable(
                name: "SurveyQuestions",
                newName: "SurveyQuestion");

            migrationBuilder.RenameTable(
                name: "QuestionAnswerVariants",
                newName: "QuestionAnswerVariant");

            migrationBuilder.RenameColumn(
                name: "Surname",
                table: "User",
                newName: "SurName");

            migrationBuilder.RenameIndex(
                name: "IX_SurveyQuestions_SurveyId",
                table: "SurveyQuestion",
                newName: "IX_SurveyQuestion_SurveyId");

            migrationBuilder.RenameIndex(
                name: "IX_SurveyQuestions_QuestionId",
                table: "SurveyQuestion",
                newName: "IX_SurveyQuestion_QuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_QuestionAnswerVariants_QuestionId",
                table: "QuestionAnswerVariant",
                newName: "IX_QuestionAnswerVariant_QuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_QuestionAnswerVariants_AnswerVariantId",
                table: "QuestionAnswerVariant",
                newName: "IX_QuestionAnswerVariant_AnswerVariantId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SurveyQuestion",
                table: "SurveyQuestion",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuestionAnswerVariant",
                table: "QuestionAnswerVariant",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionAnswerVariant_AnswerVariant_AnswerVariantId",
                table: "QuestionAnswerVariant",
                column: "AnswerVariantId",
                principalTable: "AnswerVariant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionAnswerVariant_Question_QuestionId",
                table: "QuestionAnswerVariant",
                column: "QuestionId",
                principalTable: "Question",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SurveyQuestion_Question_QuestionId",
                table: "SurveyQuestion",
                column: "QuestionId",
                principalTable: "Question",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SurveyQuestion_Survey_SurveyId",
                table: "SurveyQuestion",
                column: "SurveyId",
                principalTable: "Survey",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
