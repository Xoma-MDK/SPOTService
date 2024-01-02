using Newtonsoft.Json;

namespace SPOTService.DataStorage.Entities
{
    public class RoleRules : IEntity
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int RulesId { get; set; }

        [JsonIgnore]
        public virtual Role? Role { get; set; }
        [JsonIgnore]
        public virtual Rules? Rule { get; set; }
    }
}
