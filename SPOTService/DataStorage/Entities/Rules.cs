using Newtonsoft.Json;

namespace SPOTService.DataStorage.Entities
{
    /// <summary>
    /// Класс, представляющий правило в системе.
    /// </summary>
    public class Rules : IEntity
    {
        /// <summary>
        /// Уникальный идентификатор правила.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название правила.
        /// </summary>
        public required string Title { get; set; }

        /// <summary>
        /// Список ролей, связанных с этим правилом.
        /// </summary>
        [JsonIgnore]
        public virtual IEnumerable<Role>? Roles { get; set; }

        /// <summary>
        /// Список связей между правилом и ролями.
        /// </summary>
        [JsonIgnore]
        public virtual IEnumerable<RoleRules>? RoleRules { get; set; }
    }
}
