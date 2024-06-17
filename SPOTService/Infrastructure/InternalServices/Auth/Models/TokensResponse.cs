namespace SPOTService.Infrastructure.InternalServices.Auth.Models
{
    /// <summary>
    /// Модель для ответа с токенами.
    /// </summary>
    public class TokensResponse
    {
        /// <summary>
        /// Токен доступа.
        /// </summary>
        public string? Access { get; set; }

        /// <summary>
        /// Токен обновления.
        /// </summary>
        public string? Refresh { get; set; }
    }
}