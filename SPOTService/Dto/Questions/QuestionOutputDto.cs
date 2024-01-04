using SPOTService.Dto.AnswerVariants;

namespace SPOTService.Dto.Questions
{
    public class QuestionOutputDto
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public bool IsOpen { get; set; }
        public IEnumerable<AnswerVariantOutputDto>? AnswerVariants { get; set; }
    }
}
