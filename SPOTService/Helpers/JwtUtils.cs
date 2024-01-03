using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using SPOTService.Infrastructure.InternalServices.Auth.ENums;
using SPOTService.Infrastructure.InternalServices.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using SPOTService.DataStorage.Entities;

namespace SPOTService.Helpers
{
    public class JwtUtils
    {
        public static JwtSecurityToken DecodeJwt(string token)
        {
            JwtSecurityTokenHandler jwtHandler = new();

            JwtSecurityToken jwtToken = jwtHandler.ReadJwtToken(token.Split(' ')[1]);

            return jwtToken ?? throw new Exception("Can not read token");
        }

        public static long GetUid(JwtSecurityToken jwt)
        {
            var userID = (jwt.Claims.FirstOrDefault(claims => claims.Type == JwtClaimTypes.UserId)?.Value) ?? throw new Exception("Uid not found");
            return long.Parse(userID);
        }


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
