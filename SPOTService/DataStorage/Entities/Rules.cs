using Newtonsoft.Json;

namespace SPOTService.DataStorage.Entities
{
    public class Rules : IEntity
    {
        public int Id { get; set; }
        public required string Title { get; set; }

        [JsonIgnore]
        public virtual IEnumerable<Role>? Roles { get; set; }

        [JsonIgnore]
        public virtual IEnumerable<RoleRules>? RoleRules { get; set; }
    }
}
