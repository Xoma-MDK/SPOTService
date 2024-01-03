using SPOTService.DataStorage.Entities;
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
        public Role? Role { get; set; }
        public TokensResponse? Tokens { get; set; }
    }
}
