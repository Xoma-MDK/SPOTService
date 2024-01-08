using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SPOTService.Migrations
{
    /// <inheritdoc />
    public partial class FixFKAnswerVariant3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answer_AnswerVariant_AnswerVariantId",
                table: "Answer");

            migrationBuilder.AddForeignKey(
                name: "FK_Answer_AnswerVariant_AnswerVariantId",
                table: "Answer",
                column: "AnswerVariantId",
                principalTable: "AnswerVariant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answer_AnswerVariant_AnswerVariantId",
                table: "Answer");

            migrationBuilder.AddForeignKey(
                name: "FK_Answer_AnswerVariant_AnswerVariantId",
                table: "Answer",
                column: "AnswerVariantId",
                principalTable: "AnswerVariant",
                principalColumn: "Id");
        }
    }
}
