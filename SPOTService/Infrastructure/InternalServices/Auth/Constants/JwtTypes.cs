namespace SPOTService.Infrastructure.InternalServices.Auth.Enums
{
    /// <summary>
    /// Типы JWT токенов.
    /// </summary>
    public class JwtTypes
    {
        /// <summary>
        /// Тип доступного токена.
        /// </summary>
        public static string Access { get; } = "Access";

        /// <summary>
        /// Тип обновляемого токена.
        /// </summary>
        public static string Refresh { get; } = "Refresh";
    }
}
