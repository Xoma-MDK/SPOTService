namespace SPOTService.Dto.AnswerVariants
{
    public class AnswerVariantInputDto
    {
        public required string Title { get; set; }
        public override string ToString()
        {
            return $"Title: {Title}";
        }
    }
}
