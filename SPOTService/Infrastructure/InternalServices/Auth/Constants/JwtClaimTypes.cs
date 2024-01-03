namespace SPOTService.Infrastructure.InternalServices.Auth.ENums
{
    public class JwtClaimTypes
    {
        public static string UserId { get; } = "uid";
        public static string Scope { get; } = "scope";
        public static string Username { get; } = "username";
    }
}
