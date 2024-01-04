namespace SPOTService.Dto.User
{
    /// <summary>
    /// Модель входеых данных авторизации пользователя
    /// </summary>
    public class UserLoginInputDto
    {
        /// <summary>
        /// Логин
        /// </summary>
        public required string Login { get; set; }
        /// <summary>
        /// Пароль
        /// </summary>
        public required string Password { get; set; }
    }
}
