namespace SPOTService.Dto.Answers
{
    public class AnswerOutputDto
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public int SurveyId { get; set; }
        public int RespondentId { get; set; }
        public int AnswerVariantId { get; set; }
        public string? OpenAnswer { get; set; }
    }
}
