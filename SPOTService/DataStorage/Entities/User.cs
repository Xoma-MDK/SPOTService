namespace SPOTService.DataStorage.Entities
{
    public class User : IEntity
    {
        public int Id { get; set; }
        public required string Surname { get; set; }
        public required string Name { get; set; }
        public string? Patronomyc {  get; set; }
        public required string Login { get; set; }
        public required string PasswordHash { get; set; }
        public string? RefreshTokenHash { get; set; }
        public int RoleId { get; set; }

        public virtual IEnumerable<Survey>? Surveys { get; set; }
        public virtual Role? Role { get; set; }
    }
}
