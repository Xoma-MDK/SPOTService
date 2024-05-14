namespace SPOTService.Dto.QuestionGroup
{
    public class QuestionGroupInputDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int? ParentId { get; set; }
        public int SequenceNumber { get; set; }
    }
}
