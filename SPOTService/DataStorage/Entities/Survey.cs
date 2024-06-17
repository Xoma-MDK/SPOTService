using Newtonsoft.Json;

namespace SPOTService.DataStorage.Entities
{
    /// <summary>
    /// Класс, представляющий опрос в системе.
    /// </summary>
    public class Survey : IEntity
    {
        /// <summary>
        /// Уникальный идентификатор опроса.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название опроса.
        /// </summary>
        public required string Title { get; set; }

        /// <summary>
        /// Описание опроса.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Код доступа к опросу.
        /// </summary>
        public required string AccessCode { get; set; }

        /// <summary>
        /// Время начала опроса.
        /// </summary>
        public DateTimeOffset? StartTime { get; set; }

        /// <summary>
        /// Время окончания опроса.
        /// </summary>
        public DateTimeOffset? EndTime { get; set; }

        /// <summary>
        /// Активность опроса.
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// Идентификатор группы, к которой привязан опрос.
        /// </summary>
        public int GroupId { get; set; }

        /// <summary>
        /// Отделение, связанное с опросом.
        /// </summary>
        public string? Department { get; set; }

        /// <summary>
        /// Идентификатор пользователя, создавшего опрос.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Группа, к которой привязан опрос.
        /// </summary>
        [JsonIgnore]
        public virtual Group? Group { get; set; }

        /// <summary>
        /// Пользователь, создавший опрос.
        /// </summary>
        [JsonIgnore]
        public virtual User? User { get; set; }

        /// <summary>
        /// Список ответов на данный опрос.
        /// </summary>
        [JsonIgnore]
        public virtual IEnumerable<Answer>? Answers { get; set; }

        /// <summary>
        /// Список вопросов, входящих в опрос.
        /// </summary>
        [JsonIgnore]
        public virtual IEnumerable<Question>? Questions { get; set; }

        /// <summary>
        /// Связи между вопросами и опросом.
        /// </summary>
        [JsonIgnore]
        public virtual IEnumerable<SurveyQuestion>? SurveyQuestions { get; set; }
    }
}
