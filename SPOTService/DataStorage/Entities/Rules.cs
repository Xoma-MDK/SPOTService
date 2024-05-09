namespace SPOTService.DataStorage.Entities
{
    public class Rules : IEntity
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Alias { get; set; }
        public required string Action { get; set; }

        public virtual IEnumerable<Role>? Roles { get; set; }

        public virtual IEnumerable<RoleRules>? RoleRules { get; set; }
    }
}
