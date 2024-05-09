namespace SPOTService.DataStorage.Entities
{
    public class Answer : IEntity
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public int RespondentId { get; set; }
        public int? AnswerVariantId { get; set; }
        public string? OpenAnswer { get; set; }

        public virtual Question? Question { get; set; }

        public virtual Respondent? Respondent { get; set; }

        public virtual AnswerVariant? AnswerVariant { get; set; }

    }
}
