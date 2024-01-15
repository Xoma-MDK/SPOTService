using System.IdentityModel.Tokens.Jwt;
using SPOTService.Dto.User;
using SPOTService.Infrastructure.InternalServices.Auth.Models;

namespace SPOTService.Infrastructure.InternalServices.Auth
{
    public interface IAuthService
    {
        Task<TokensResponse> Login(UserLoginInputDto userLogin);
        Task<TokensResponse> Register(UserInputDto userInput);
        Task<TokensResponse> Refresh(string refreshToken);
        Task Logout(string token);
    }
}
