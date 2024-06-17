using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace SPOTService.Infrastructure.InternalServices.Auth
{
    /// <summary>
    /// Настройки аутентификации.
    /// </summary>
    public class AuthOptions
    {
        /// <summary>
        /// Издатель токена (issuer).
        /// </summary>
        public const string ISSUER = "";

        /// <summary>
        /// Аудитория токена (audience).
        /// </summary>
        public const string AUDIENCE = "";

        /// <summary>
        /// Ключ для создания и проверки токена.
        /// </summary>
        private const string KEY = "mysupersecret_keymysupersecret_keymysupersecret_key";

        /// <summary>
        /// Время жизни токена доступа.
        /// </summary>
        public static readonly TimeSpan accessLifetime = TimeSpan.FromMinutes(10);

        /// <summary>
        /// Время жизни токена обновления.
        /// </summary>
        public static readonly TimeSpan refreshLifetime = TimeSpan.FromHours(1);

        /// <summary>
        /// Возвращает симметричный ключ безопасности на основе заданного ключа.
        /// </summary>
        /// <returns>Симметричный ключ безопасности.</returns>
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
