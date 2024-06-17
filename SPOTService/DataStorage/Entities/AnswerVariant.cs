using Newtonsoft.Json;

namespace SPOTService.DataStorage.Entities
{
    /// <summary>
    /// Класс, представляющий вариант ответа на вопрос.
    /// </summary>
    public class AnswerVariant : IEntity
    {
        /// <summary>
        /// Уникальный идентификатор варианта ответа.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название варианта ответа.
        /// </summary>
        public required string Title { get; set; }

        /// <summary>
        /// Связанные ответы.
        /// </summary>
        [JsonIgnore]
        public virtual IEnumerable<Answer>? Answers { get; set; }

        /// <summary>
        /// Связанные вопросы.
        /// </summary>
        [JsonIgnore]
        public virtual IEnumerable<Question>? Questions { get; set; }

        /// <summary>
        /// Связанные варианты ответа на вопросы.
        /// </summary>
        [JsonIgnore]
        public virtual IEnumerable<QuestionAnswerVariant>? QuestionAnswerVariants { get; set; }

        /// <summary>
        /// Возвращает строку, представляющую текущий объект.
        /// </summary>
        /// <returns>Строка, представляющая текущий объект.</returns>
        public override string ToString()
        {
            return $"Title: {Title}";
        }
    }

}
