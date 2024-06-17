using Microsoft.IdentityModel.Tokens;
using SPOTService.DataStorage.Entities;
using SPOTService.Infrastructure.InternalServices.Auth;
using SPOTService.Infrastructure.InternalServices.Auth.Enums;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SPOTService.Helpers
{
    /// <summary>
    /// Утилитарный класс для работы с JWT токенами.
    /// </summary>
    public class JwtUtils
    {
        /// <summary>
        /// Декодирует JWT токен.
        /// </summary>
        /// <param name="token">JWT токен для декодирования.</param>
        /// <returns>Декодированный объект JwtSecurityToken.</returns>
        public static JwtSecurityToken DecodeJwt(string token)
        {
            JwtSecurityTokenHandler jwtHandler = new();

            JwtSecurityToken jwtToken = jwtHandler.ReadJwtToken(token.Split(' ')[1]);

            return jwtToken ?? throw new Exception("Can not read token");
        }

        /// <summary>
        /// Получает идентификатор пользователя (UID) из JWT токена.
        /// </summary>
        /// <param name="jwt">Декодированный объект JwtSecurityToken.</param>
        /// <returns>Идентификатор пользователя (UID).</returns>
        public static long GetUid(JwtSecurityToken jwt)
        {
            var userID = (jwt.Claims.FirstOrDefault(claims => claims.Type == JwtClaimTypes.UserId)?.Value) ?? throw new Exception("Uid not found");
            return long.Parse(userID);
        }


        /// <summary>
        /// Создает объект ClaimsIdentity на основе пользователя и области токена.
        /// </summary>
        /// <param name="user">Пользователь.</param>
        /// <param name="scope">Область токена (Access или Refresh).</param>
        /// <returns>Объект ClaimsIdentity.</returns>
        private static ClaimsIdentity GetIdentity(User user, string scope)
        {
            var claims = new List<Claim>
            {
                new(JwtClaimTypes.UserId, user.Id.ToString()),
                new(JwtClaimTypes.Scope, scope),
                new(JwtClaimTypes.Username, user.Login),
            };

            ClaimsIdentity claimsIdentity = new(claims, "Token");

            return claimsIdentity;
        }

        /// <summary>
        /// Создает JWT токен для пользователя.
        /// </summary>
        /// <param name="user">Пользователь, для которого создается токен.</param>
        /// <param name="lifetime">Срок действия токена.</param>
        /// <param name="scope">Область токена (Access или Refresh).</param>
        /// <returns>JWT токен в виде строки.</returns>
        public static string GetToken(User user, TimeSpan lifetime, string scope)
        {
            var now = DateTime.Now;

            ClaimsIdentity identity = GetIdentity(user, scope);

            JwtSecurityToken token = new(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                notBefore: now,
                claims: identity.Claims,
                expires: now.Add(lifetime),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
