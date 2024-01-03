using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SPOTService.DataStorage;
using SPOTService.DataStorage.Entities;
using SPOTService.Dto.User;
using SPOTService.Helpers;
using SPOTService.Infrastructure.InternalServices.Auth.ENums;
using SPOTService.Infrastructure.InternalServices.Auth.Models;
using System.IdentityModel.Tokens.Jwt;

namespace SPOTService.Infrastructure.InternalServices.Auth
{
    public class AuthService(MainContext mainContext) : IAuthService
    {
        private readonly MainContext _mainContext = mainContext;

        public async Task<TokensResponse> Login(UserLoginInputDto userLogin)
        {
            var user = await _mainContext.Users.Where(u => u.Login == userLogin.Login).FirstOrDefaultAsync() ?? throw new Exception("User not found");
            if (!HashUtil.VerifyPassword(userLogin.Password, user.PasswordHash))
                throw new Exception("Invalid login or password");

            string accessToken = JwtUtils.GetToken(user, AuthOptions.accessLifetime, JwtTypes.Access);
            string refreshToken = JwtUtils.GetToken(user, AuthOptions.refreshLifetime, JwtTypes.Refresh);

            user.RefreshTokenHash = HashUtil.HashToken(refreshToken);
            await _mainContext.SaveChangesAsync();

            return new TokensResponse
            {
                Access = accessToken,
                Refresh = refreshToken,
            };
        }

        public async Task Logout(long userId)
        {
            

            var user = await _mainContext.Users.Where(u => u.Id == userId).FirstOrDefaultAsync() ?? throw new Exception("User not found");
            user.RefreshTokenHash = null;

            await _mainContext.SaveChangesAsync();
        }
        
        public async Task<TokensResponse> Refresh(string refreshToken)
        {
            JwtSecurityTokenHandler jwtHandler = new();

            JwtSecurityToken jwtToken = jwtHandler.ReadJwtToken(refreshToken) ?? throw new Exception("Can not read token");
            var scope = (jwtToken.Claims.FirstOrDefault(claims => claims.Type == JwtClaimTypes.Scope)?.Value) ?? throw new Exception("Scope not found");
            
            if (scope != JwtTypes.Refresh)
                throw new Exception("Use the refresh token");
            
            var userId = (jwtToken.Claims.FirstOrDefault(claim => claim.Type == JwtClaimTypes.UserId)?.Value) ?? throw new Exception("Uid not found");
            
            var user = await _mainContext.Users.Where(u => u.Id == int.Parse(userId)).FirstOrDefaultAsync() ?? throw new Exception("User not found");
            
            if (user.RefreshTokenHash == null)
                throw new Exception("User does not authorize");

            if (!HashUtil.VerifyToken(refreshToken, user.RefreshTokenHash))
                throw new Exception("Token is not valid");

            string newAccessToken = JwtUtils.GetToken(user, AuthOptions.accessLifetime, JwtTypes.Access);
            string newRefreshToken = JwtUtils.GetToken(user, AuthOptions.refreshLifetime, JwtTypes.Refresh);

            user.RefreshTokenHash = HashUtil.HashToken(newRefreshToken);
            await _mainContext.SaveChangesAsync();

            return new TokensResponse
            {
                Access = newAccessToken,
                Refresh = newRefreshToken,
            };
        }

        public async Task<TokensResponse> Register(UserInputDto userInput)
        {
            var user = await _mainContext.Users.Where(u =>  u.Login == userInput.Login).FirstOrDefaultAsync();

            if (user != null)
                throw new Exception("User already exists");

            User newUser = new() {
                Surname = userInput.Surname,
                Login = userInput.Login,
                Name = userInput.Name,
                Patronomyc = userInput.Patronomyc,
                PasswordHash = HashUtil.HashPassowd(userInput.Password) 
            };

            await _mainContext.AddAsync(newUser);
            await _mainContext.SaveChangesAsync();

            string accessToken = JwtUtils.GetToken(newUser, AuthOptions.accessLifetime, JwtTypes.Access);
            string refreshToken = JwtUtils.GetToken(newUser, AuthOptions.refreshLifetime, JwtTypes.Refresh);

            newUser.RefreshTokenHash = HashUtil.HashToken(refreshToken);
            await _mainContext.SaveChangesAsync();

            return new TokensResponse
            {
                Access = accessToken,
                Refresh = refreshToken,
            };
        }

    }
}
