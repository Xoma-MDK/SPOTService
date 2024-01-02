using Newtonsoft.Json;

namespace SPOTService.DataStorage.Entities
{
    public class QuestionAnswerVariant : IEntity
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public int AnswerVariantId { get; set; }

        [JsonIgnore]
        public virtual Question? Question { get; set; }
        [JsonIgnore]
        public virtual AnswerVariant? AnswerVariant { get; set; }

    }
}
