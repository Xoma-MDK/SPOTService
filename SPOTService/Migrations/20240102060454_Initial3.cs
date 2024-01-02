using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SPOTService.Migrations
{
    /// <inheritdoc />
    public partial class Initial3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answer_Respondent_RespondentId",
                table: "Answer");

            migrationBuilder.DropIndex(
                name: "IX_Answer_RespondentId",
                table: "Answer");

            migrationBuilder.DropColumn(
                name: "RepondentId",
                table: "Answer");

            migrationBuilder.AlterColumn<string>(
                name: "Surname",
                table: "Respondent",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Patronomic",
                table: "Respondent",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Respondent",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "RespondentId",
                table: "Answer",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "QuestionAnswerVariant",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    QuestionId = table.Column<int>(type: "integer", nullable: false),
                    AnswerVariantId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionAnswerVariant", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionAnswerVariant_AnswerVariant_AnswerVariantId",
                        column: x => x.AnswerVariantId,
                        principalTable: "AnswerVariant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestionAnswerVariant_Question_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Question",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Answer_RespondentId",
                table: "Answer",
                column: "RespondentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_QuestionAnswerVariant_AnswerVariantId",
                table: "QuestionAnswerVariant",
                column: "AnswerVariantId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionAnswerVariant_QuestionId",
                table: "QuestionAnswerVariant",
                column: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Answer_Respondent_RespondentId",
                table: "Answer",
                column: "RespondentId",
                principalTable: "Respondent",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answer_Respondent_RespondentId",
                table: "Answer");

            migrationBuilder.DropTable(
                name: "QuestionAnswerVariant");

            migrationBuilder.DropIndex(
                name: "IX_Answer_RespondentId",
                table: "Answer");

            migrationBuilder.AlterColumn<string>(
                name: "Surname",
                table: "Respondent",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Patronomic",
                table: "Respondent",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Respondent",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RespondentId",
                table: "Answer",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "RepondentId",
                table: "Answer",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Answer_RespondentId",
                table: "Answer",
                column: "RespondentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Answer_Respondent_RespondentId",
                table: "Answer",
                column: "RespondentId",
                principalTable: "Respondent",
                principalColumn: "Id");
        }
    }
}
