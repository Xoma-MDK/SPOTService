using SPOTService.Dto.Roles;
using SPOTService.Infrastructure.InternalServices.Auth.Models;

namespace SPOTService.Dto.User
{
    public class UserOutputDto
    {
        public int Id { get; set; }
        public required string Surname { get; set; }
        public required string Name { get; set; }
        public string? Patronomyc { get; set; }
        public required string Login { get; set; }
        public int RoleId { get; set; }
        public RoleOutputDto? Role { get; set; }
        public TokensResponse? Tokens { get; set; }
    }
}
