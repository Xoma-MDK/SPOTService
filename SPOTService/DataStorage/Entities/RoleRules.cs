using Newtonsoft.Json;

namespace SPOTService.DataStorage.Entities
{
    /// <summary>
    /// Класс, представляющий связь между ролью и правилом в системе.
    /// </summary>
    public class RoleRules : IEntity
    {
        /// <summary>
        /// Уникальный идентификатор связи между ролью и правилом.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Идентификатор роли.
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// Идентификатор правила.
        /// </summary>
        public int RulesId { get; set; }

        /// <summary>
        /// Связанная роль.
        /// </summary>
        [JsonIgnore]
        public virtual Role? Role { get; set; }

        /// <summary>
        /// Связанное правило.
        /// </summary>
        [JsonIgnore]
        public virtual Rules? Rule { get; set; }
    }
}
