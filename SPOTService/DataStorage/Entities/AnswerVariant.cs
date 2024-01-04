using Newtonsoft.Json;

namespace SPOTService.DataStorage.Entities
{
    public class AnswerVariant : IEntity
    {
        public int Id { get; set; }
        public required string Title { get; set; }

        [JsonIgnore]
        public virtual IEnumerable<Answer>? Answers { get; set; }
        [JsonIgnore]
        public virtual IEnumerable<Question>? Questions { get; set; }
        [JsonIgnore]
        public virtual IEnumerable<QuestionAnswerVariant>? QuestionAnswerVariants { get; set;}
        public override string ToString()
        {
            return $"Title: {Title}";
        }
    }
}
