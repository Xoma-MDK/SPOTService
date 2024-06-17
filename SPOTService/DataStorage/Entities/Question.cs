using Newtonsoft.Json;

namespace SPOTService.DataStorage.Entities
{
    /// <summary>
    /// Класс, представляющий вопрос в опросе.
    /// </summary>
    public class Question : IEntity
    {
        /// <summary>
        /// Уникальный идентификатор вопроса.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Заголовок вопроса.
        /// </summary>
        public required string Title { get; set; }

        /// <summary>
        /// Определяет, является ли вопрос открытым.
        /// </summary>
        public bool IsOpen { get; set; }

        /// <summary>
        /// Связанные ответы на этот вопрос.
        /// </summary>
        [JsonIgnore]
        public virtual IEnumerable<Answer>? Answers { get; set; }

        /// <summary>
        /// Связанные варианты ответов на этот вопрос.
        /// </summary>
        [JsonIgnore]
        public virtual IEnumerable<AnswerVariant>? AnswerVariants { get; set; }

        /// <summary>
        /// Связанные варианты вопросов и ответов.
        /// </summary>
        [JsonIgnore]
        public virtual IEnumerable<QuestionAnswerVariant>? QuestionAnswerVariants { get; set; }

        /// <summary>
        /// Связанные опросы, содержащие этот вопрос.
        /// </summary>
        [JsonIgnore]
        public virtual IEnumerable<Survey>? Surveys { get; set; }

        /// <summary>
        /// Связанные вопросы в опросах.
        /// </summary>
        [JsonIgnore]
        public virtual IEnumerable<SurveyQuestion>? SurveyQuestions { get; set; }

        /// <summary>
        /// Возвращает строковое представление вопроса.
        /// </summary>
        /// <returns>Строковое представление вопроса, включающее заголовок и тип вопроса.</returns>
        public override string ToString()
        {
            return $"\nTitle: {Title},\n" +
                   $"IsOpen: {IsOpen}, \n";
        }
    }
}
