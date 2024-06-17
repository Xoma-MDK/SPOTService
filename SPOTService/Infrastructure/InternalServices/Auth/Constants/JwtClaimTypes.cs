namespace SPOTService.Infrastructure.InternalServices.Auth.Enums
{
    /// <summary>
    /// Типы утверждений JWT.
    /// </summary>
    public class JwtClaimTypes
    {
        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public static string UserId { get; } = "uid";

        /// <summary>
        /// Область (scope).
        /// </summary>
        public static string Scope { get; } = "scope";

        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public static string Username { get; } = "username";
    }

}
