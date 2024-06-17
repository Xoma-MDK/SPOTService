using Newtonsoft.Json;

namespace SPOTService.DataStorage.Entities
{
    /// <summary>
    /// Класс, представляющий связь между вопросом и вариантом ответа.
    /// </summary>
    public class QuestionAnswerVariant : IEntity
    {
        /// <summary>
        /// Уникальный идентификатор связи.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Идентификатор вопроса.
        /// </summary>
        public int QuestionId { get; set; }

        /// <summary>
        /// Идентификатор варианта ответа.
        /// </summary>
        public int AnswerVariantId { get; set; }

        /// <summary>
        /// Связанный вопрос.
        /// </summary>
        [JsonIgnore]
        public virtual Question? Question { get; set; }

        /// <summary>
        /// Связанный вариант ответа.
        /// </summary>
        [JsonIgnore]
        public virtual AnswerVariant? AnswerVariant { get; set; }
    }
}
