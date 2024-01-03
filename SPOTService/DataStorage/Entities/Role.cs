using Newtonsoft.Json;

namespace SPOTService.DataStorage.Entities
{
    public class Role : IEntity
    {
        public int Id { get; set; }

        public required string Title { get; set; }

        [JsonIgnore]
        public virtual IEnumerable<Rules>? Rules { get; set; }
        [JsonIgnore]
        public virtual IEnumerable<RoleRules>? RoleRules { get; set; }
        [JsonIgnore]
        public virtual IEnumerable<User>? Users { get; set; }
    }
}
