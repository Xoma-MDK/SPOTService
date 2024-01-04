using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace SPOTService.Infrastructure.InternalServices.Auth
{
    public class AuthOptions
    {
        public const string ISSUER = "";
        public const string AUDIENCE = "";

        const string KEY = "mysupersecret_keymysupersecret_keymysupersecret_key";

        public static readonly TimeSpan accessLifetime = TimeSpan.FromMinutes(10);
        public static readonly TimeSpan refreshLifetime = TimeSpan.FromHours(4);

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
