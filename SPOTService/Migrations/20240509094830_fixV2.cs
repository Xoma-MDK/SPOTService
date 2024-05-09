using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SPOTService.Migrations
{
    /// <inheritdoc />
    public partial class fixV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answer_Survey_SurveyId",
                table: "Answer");

            migrationBuilder.DropTable(
                name: "QuestionSurvey");

            migrationBuilder.DropIndex(
                name: "IX_Answer_SurveyId",
                table: "Answer");

            migrationBuilder.DropColumn(
                name: "SurveyId",
                table: "Answer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SurveyId",
                table: "Answer",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "QuestionSurvey",
                columns: table => new
                {
                    QuestionsId = table.Column<int>(type: "integer", nullable: false),
                    SurveysId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionSurvey", x => new { x.QuestionsId, x.SurveysId });
                    table.ForeignKey(
                        name: "FK_QuestionSurvey_Question_QuestionsId",
                        column: x => x.QuestionsId,
                        principalTable: "Question",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestionSurvey_Survey_SurveysId",
                        column: x => x.SurveysId,
                        principalTable: "Survey",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Answer_SurveyId",
                table: "Answer",
                column: "SurveyId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionSurvey_SurveysId",
                table: "QuestionSurvey",
                column: "SurveysId");

            migrationBuilder.AddForeignKey(
                name: "FK_Answer_Survey_SurveyId",
                table: "Answer",
                column: "SurveyId",
                principalTable: "Survey",
                principalColumn: "Id");
        }
    }
}
