using SPOTService.Dto.User;
using SPOTService.Infrastructure.InternalServices.Auth.Models;

namespace SPOTService.Infrastructure.InternalServices.Auth
{
    /// <summary>
    /// Интерфейс сервиса аутентификации.
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Метод для входа пользователя.
        /// </summary>
        /// <param name="userLogin">Данные для входа пользователя.</param>
        /// <returns>Ответ с токенами доступа и обновления.</returns>
        Task<TokensResponse> Login(UserLoginInputDto userLogin);

        /// <summary>
        /// Метод для регистрации нового пользователя.
        /// </summary>
        /// <param name="userInput">Данные нового пользователя.</param>
        /// <returns>Ответ с токенами доступа и обновления для нового пользователя.</returns>
        Task<TokensResponse> Register(UserInputDto userInput);

        /// <summary>
        /// Метод для обновления токена доступа на основе токена обновления.
        /// </summary>
        /// <param name="refreshToken">Токен обновления.</param>
        /// <returns>Ответ с новыми токенами доступа и обновления.</returns>
        Task<TokensResponse> Refresh(string refreshToken);

        /// <summary>
        /// Метод для выхода пользователя (отключения токена).
        /// </summary>
        /// <param name="token">Токен, который нужно отключить.</param>
        /// <returns>Задача асинхронного выполнения.</returns>
        Task Logout(string token);
    }
}
