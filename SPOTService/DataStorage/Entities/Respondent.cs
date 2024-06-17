using Newtonsoft.Json;

namespace SPOTService.DataStorage.Entities
{
    /// <summary>
    /// Класс, представляющий респондента.
    /// </summary>
    public class Respondent : IEntity
    {
        /// <summary>
        /// Уникальный идентификатор респондента.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Фамилия респондента.
        /// </summary>
        public string? Surname { get; set; }

        /// <summary>
        /// Имя респондента.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Отчество респондента.
        /// </summary>
        public string? Patronymic { get; set; }

        /// <summary>
        /// Идентификатор группы, к которой относится респондент.
        /// </summary>
        public int? GroupId { get; set; }

        /// <summary>
        /// Идентификатор Telegram респондента.
        /// </summary>
        public long TelegramId { get; set; }

        /// <summary>
        /// Идентификатор чата в Telegram респондента.
        /// </summary>
        public long TelegramChatId { get; set; }

        /// <summary>
        /// Идентификатор состояния респондента.
        /// </summary>
        public int? StateId { get; set; }

        /// <summary>
        /// Связанная группа респондента.
        /// </summary>
        [JsonIgnore]
        public virtual Group? Group { get; set; }

        /// <summary>
        /// Список ответов, данных респондентом.
        /// </summary>
        [JsonIgnore]
        public virtual IEnumerable<Answer>? Answers { get; set; }
    }
}
