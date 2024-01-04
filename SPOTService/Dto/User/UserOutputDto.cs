using SPOTService.Dto.Roles;
using SPOTService.Infrastructure.InternalServices.Auth.Models;

namespace SPOTService.Dto.User
{
    /// <summary>
    /// Модель выходных данных пользователя
    /// </summary>
    public class UserOutputDto
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Фамилия
        /// </summary>
        public required string Surname { get; set; }
        /// <summary>
        /// Имя
        /// </summary>
        public required string Name { get; set; }
        /// <summary>
        /// Отчество
        /// </summary>
        public string? Patronomyc { get; set; }
        /// <summary>
        /// Логин
        /// </summary>
        public required string Login { get; set; }
        /// <summary>
        /// Идентификатор роли
        /// </summary>
        public int RoleId { get; set; }
        /// <summary>
        /// Роль
        /// </summary>
        public RoleOutputDto? Role { get; set; }
        /// <summary>
        /// Токены авторизации
        /// </summary>
        public TokensResponse? Tokens { get; set; }
    }
}
