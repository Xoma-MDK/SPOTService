namespace SPOTService.DataStorage.Entities
{
    public class Survey : IEntity
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public required string AccessCode { get; set; }
        public DateTimeOffset? StartTime { get; set; }
        public DateTimeOffset? EndTime { get; set; }
        public bool Active { get; set; }
        public int GroupId { get; set; }
        public string? Department {  get; set; }
        public int CreatorId { get; set; }
        public int MainQuestionGroupId { get; set; }

        public virtual Group? Group { get; set; }

        public virtual User? User { get; set; }

        public virtual QuestionGroup MainQuestionGroup { get; set; }
    }
}
