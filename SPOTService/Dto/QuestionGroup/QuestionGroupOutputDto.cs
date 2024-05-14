
using SPOTService.Dto.Questions;

namespace SPOTService.Dto.QuestionGroup
{
    public class QuestionGroupOutputDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int? ParentId { get; set; }
        public int SequenceNumber { get; set; }
        public virtual QuestionGroupOutputDto? ParentQuestionGroup { get; set; }
        public virtual IEnumerable<QuestionOutputDto> Questions { get; set; }
    }
}