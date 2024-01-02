using Newtonsoft.Json;

namespace SPOTService.DataStorage.Entities
{
    public class Answer : IEntity
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public int SurveyId {  get; set; }
        public int RespondentId { get; set; }
        public int AnswerVariantId { get; set; }
        public string? OpenAnswer { get; set; }

        [JsonIgnore]
        public virtual Survey? Survey { get; set; }
        [JsonIgnore]
        public virtual Question? Question { get; set; }
        [JsonIgnore]
        public virtual Respondent? Respondent { get; set; }
        [JsonIgnore]
        public virtual AnswerVariant? AnswerVariant { get; set; }

    }
}
