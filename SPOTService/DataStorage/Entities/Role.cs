using Newtonsoft.Json;

namespace SPOTService.DataStorage.Entities
{
    /// <summary>
    /// Класс, представляющий роль в системе.
    /// </summary>
    public class Role : IEntity
    {
        /// <summary>
        /// Уникальный идентификатор роли.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название роли.
        /// </summary>
        public required string Title { get; set; }

        /// <summary>
        /// Список правил, связанных с этой ролью.
        /// </summary>
        [JsonIgnore]
        public virtual IEnumerable<Rules>? Rules { get; set; }

        /// <summary>
        /// Список связей между ролями и правилами.
        /// </summary>
        [JsonIgnore]
        public virtual IEnumerable<RoleRules>? RoleRules { get; set; }

        /// <summary>
        /// Список пользователей, имеющих эту роль.
        /// </summary>
        [JsonIgnore]
        public virtual IEnumerable<User>? Users { get; set; }
    }
}
