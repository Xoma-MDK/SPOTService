namespace SPOTService.Infrastructure.InternalServices.Auth.Models
{
    public class TokensResponse
    {
        public string? Access { get; set; }
        public string? Refresh { get; set; }
    }
}