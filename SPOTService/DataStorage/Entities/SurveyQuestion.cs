using Newtonsoft.Json;

namespace SPOTService.DataStorage.Entities
{
    /// <summary>
    /// Связь между вопросом и опросом.
    /// </summary>
    public class SurveyQuestion : IEntity
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
        /// Идентификатор опроса.
        /// </summary>
        public int SurveyId { get; set; }

        /// <summary>
        /// Вопрос, связанный с данной связью.
        /// </summary>
        [JsonIgnore]
        public virtual Question? Question { get; set; }

        /// <summary>
        /// Опрос, связанный с данной связью.
        /// </summary>
        [JsonIgnore]
        public virtual Survey? Survey { get; set; }
    }
}
