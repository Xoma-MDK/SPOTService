using SPOTService.Dto.AnswerVariants;

namespace SPOTService.Dto.Questions
{
    public class QuestionIntputDto
    {
        public required string Title { get; set; }
        public bool IsOpen { get; set; }
        public IEnumerable<AnswerVariantInputDto>? AnswerVariants { get; set; }
        public override string ToString()
        {
            return $"\nTitle: {Title},\n" +
                $" IsOpen: {IsOpen}, \n" +
                $"AnswerVariants: {AnswerVariantsToString()}\n";
        }
        private string AnswerVariantsToString()
        {
            if (AnswerVariants == null)
                return "N/A";

            return "\n[" + string.Join(", \n", AnswerVariants.Select(a => a.ToString())) + "]\n";
        }
    }
}
