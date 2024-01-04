namespace SPOTService.Dto.User
{
    /// <summary>
    /// Модель входных данных пользователя 
    /// </summary>
    public class UserInputDto
    {
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
        /// Пароль
        /// </summary>
        public required string Password { get; set; }
        /// <summary>
        /// Идентификатор роли
        /// </summary>
        public int RoleId { get; set; }
    }
}
