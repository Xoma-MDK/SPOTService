namespace SPOTService.DataStorage.Entities
{
    public class Question : IEntity
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public bool IsOpen { get; set; } = false;
        public int QuestionGroupId { get; set; }
        public int SequenceNumber { get; set; }

        public virtual QuestionGroup QuestionGroup { get; set; }

        public virtual IEnumerable<Answer>? Answers { get; set; }

        public virtual IEnumerable<AnswerVariant>? AnswerVariants { get; set; }


        public override string ToString()
        {
            return $"\nTitle: {Title},\n" +
                $" IsOpen: {IsOpen}, \n";
        }
    }
}
