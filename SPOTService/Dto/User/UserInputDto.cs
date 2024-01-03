namespace SPOTService.Dto.User
{
    public class UserInputDto
    {
        public required string Surname { get; set; }
        public required string Name { get; set; }
        public string? Patronomyc { get; set; }
        public required string Login { get; set; }
        public required string Password { get; set; }
        public int RoleId { get; set; }
    }
}
