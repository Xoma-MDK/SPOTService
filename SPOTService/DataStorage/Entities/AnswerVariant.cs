namespace SPOTService.DataStorage.Entities
{
    public class AnswerVariant : IEntity
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public int QuestionId { get; set; }

        public virtual IEnumerable<Answer>? Answers { get; set; }
        public virtual Question? Question { get; set; }
        public override string ToString()
        {
            return $"Title: {Title}";
        }
    }
}
