using Newtonsoft.Json;

namespace SPOTService.DataStorage.Entities
{
    /// <summary>
    /// Класс, представляющий ответ на вопрос в опросе.
    /// </summary>
    public class Answer : IEntity
    {
        /// <summary>
        /// Уникальный идентификатор ответа.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Идентификатор связанного вопроса.
        /// </summary>
        public int QuestionId { get; set; }

        /// <summary>
        /// Идентификатор связанного опроса.
        /// </summary>
        public int SurveyId { get; set; }

        /// <summary>
        /// Идентификатор респондента, который дал ответ.
        /// </summary>
        public int RespondentId { get; set; }

        /// <summary>
        /// Идентификатор варианта ответа (если применимо).
        /// </summary>
        public int? AnswerVariantId { get; set; }

        /// <summary>
        /// Текст открытого ответа (если применимо).
        /// </summary>
        public string? OpenAnswer { get; set; }

        /// <summary>
        /// Связанный опрос.
        /// </summary>
        [JsonIgnore]
        public virtual Survey? Survey { get; set; }

        /// <summary>
        /// Связанный вопрос.
        /// </summary>
        [JsonIgnore]
        public virtual Question? Question { get; set; }

        /// <summary>
        /// Связанный респондент.
        /// </summary>
        [JsonIgnore]
        public virtual Respondent? Respondent { get; set; }

        /// <summary>
        /// Связанный вариант ответа.
        /// </summary>
        [JsonIgnore]
        public virtual AnswerVariant? AnswerVariant { get; set; }
    }
}
