namespace SPOTService.DataStorage.Entities
{
    public class Role : IEntity
    {
        public int Id { get; set; }

        public required string Title { get; set; }

        public virtual IEnumerable<Rules>? Rules { get; set; }
        public virtual IEnumerable<RoleRules>? RoleRules { get; set; }
        public virtual IEnumerable<User>? Users { get; set; }
    }
}
