using Newtonsoft.Json;

namespace SPOTService.DataStorage.Entities
{
    /// <summary>
    /// Класс, представляющий группу респондентов.
    /// </summary>
    public class Group : IEntity
    {
        /// <summary>
        /// Уникальный идентификатор группы.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название группы.
        /// </summary>
        public required string Title { get; set; }

        /// <summary>
        /// Связанные респонденты.
        /// </summary>
        [JsonIgnore]
        public virtual IEnumerable<Respondent>? Respondents { get; set; }

        /// <summary>
        /// Связанные опросы.
        /// </summary>
        [JsonIgnore]
        public virtual IEnumerable<Survey>? Surveys { get; set; }
    }
}
