using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SPOTService.Migrations
{
    /// <inheritdoc />
    public partial class v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answer_Survey_SurveyId",
                table: "Answer");

            migrationBuilder.DropForeignKey(
                name: "FK_Survey_User_UserId",
                table: "Survey");

            migrationBuilder.DropTable(
                name: "QuestionAnswerVariants");

            migrationBuilder.DropTable(
                name: "SurveyQuestions");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Survey",
                newName: "MainQuestionGroupId");

            migrationBuilder.RenameIndex(
                name: "IX_Survey_UserId",
                table: "Survey",
                newName: "IX_Survey_MainQuestionGroupId");

            migrationBuilder.AddColumn<int>(
                name: "CreatorId",
                table: "Survey",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Action",
                table: "Rules",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Alias",
                table: "Rules",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "QuestionGroupId",
                table: "Question",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SequenceNumber",
                table: "Question",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "QuestionId",
                table: "AnswerVariant",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "SurveyId",
                table: "Answer",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateTable(
                name: "QuestionGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    ParentId = table.Column<int>(type: "integer", nullable: true),
                    SequenceNumber = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionGroups_QuestionGroups_ParentId",
                        column: x => x.ParentId,
                        principalTable: "QuestionGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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
                name: "IX_Survey_CreatorId",
                table: "Survey",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Question_QuestionGroupId",
                table: "Question",
                column: "QuestionGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_AnswerVariant_QuestionId",
                table: "AnswerVariant",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionGroups_ParentId",
                table: "QuestionGroups",
                column: "ParentId",
                unique: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_AnswerVariant_Question_QuestionId",
                table: "AnswerVariant",
                column: "QuestionId",
                principalTable: "Question",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Question_QuestionGroups_QuestionGroupId",
                table: "Question",
                column: "QuestionGroupId",
                principalTable: "QuestionGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Survey_QuestionGroups_MainQuestionGroupId",
                table: "Survey",
                column: "MainQuestionGroupId",
                principalTable: "QuestionGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Survey_User_CreatorId",
                table: "Survey",
                column: "CreatorId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answer_Survey_SurveyId",
                table: "Answer");

            migrationBuilder.DropForeignKey(
                name: "FK_AnswerVariant_Question_QuestionId",
                table: "AnswerVariant");

            migrationBuilder.DropForeignKey(
                name: "FK_Question_QuestionGroups_QuestionGroupId",
                table: "Question");

            migrationBuilder.DropForeignKey(
                name: "FK_Survey_QuestionGroups_MainQuestionGroupId",
                table: "Survey");

            migrationBuilder.DropForeignKey(
                name: "FK_Survey_User_CreatorId",
                table: "Survey");

            migrationBuilder.DropTable(
                name: "QuestionGroups");

            migrationBuilder.DropTable(
                name: "QuestionSurvey");

            migrationBuilder.DropIndex(
                name: "IX_Survey_CreatorId",
                table: "Survey");

            migrationBuilder.DropIndex(
                name: "IX_Question_QuestionGroupId",
                table: "Question");

            migrationBuilder.DropIndex(
                name: "IX_AnswerVariant_QuestionId",
                table: "AnswerVariant");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Survey");

            migrationBuilder.DropColumn(
                name: "Action",
                table: "Rules");

            migrationBuilder.DropColumn(
                name: "Alias",
                table: "Rules");

            migrationBuilder.DropColumn(
                name: "QuestionGroupId",
                table: "Question");

            migrationBuilder.DropColumn(
                name: "SequenceNumber",
                table: "Question");

            migrationBuilder.DropColumn(
                name: "QuestionId",
                table: "AnswerVariant");

            migrationBuilder.RenameColumn(
                name: "MainQuestionGroupId",
                table: "Survey",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Survey_MainQuestionGroupId",
                table: "Survey",
                newName: "IX_Survey_UserId");

            migrationBuilder.AlterColumn<int>(
                name: "SurveyId",
                table: "Answer",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "QuestionAnswerVariants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AnswerVariantId = table.Column<int>(type: "integer", nullable: false),
                    QuestionId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionAnswerVariants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionAnswerVariants_AnswerVariant_AnswerVariantId",
                        column: x => x.AnswerVariantId,
                        principalTable: "AnswerVariant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestionAnswerVariants_Question_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Question",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SurveyQuestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    QuestionId = table.Column<int>(type: "integer", nullable: false),
                    SurveyId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SurveyQuestions_Question_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Question",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SurveyQuestions_Survey_SurveyId",
                        column: x => x.SurveyId,
                        principalTable: "Survey",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuestionAnswerVariants_AnswerVariantId",
                table: "QuestionAnswerVariants",
                column: "AnswerVariantId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionAnswerVariants_QuestionId",
                table: "QuestionAnswerVariants",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyQuestions_QuestionId",
                table: "SurveyQuestions",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyQuestions_SurveyId",
                table: "SurveyQuestions",
                column: "SurveyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Answer_Survey_SurveyId",
                table: "Answer",
                column: "SurveyId",
                principalTable: "Survey",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Survey_User_UserId",
                table: "Survey",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
