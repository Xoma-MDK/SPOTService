namespace SPOTService.DataStorage.Entities
{
    public class QuestionGroup : IEntity
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public string Description { get; set; } = string.Empty;
        public int? ParentId { get; set; }
        public int SequenceNumber { get; set; }

        public virtual IEnumerable<Question> Questions { get; set; }
        public virtual QuestionGroup? ParentQuestionGroup { get; set; }
        public virtual QuestionGroup? ChildrenQuestionGroup { get; set; }
        public virtual IEnumerable<Survey> Surveys { get; set; }

    }
}
