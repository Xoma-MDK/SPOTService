using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SPOTService.DataStorage;
using SPOTService.DataStorage.Entities;
using SPOTService.DataStorage.Repositories;
using SPOTService.Dto.User;
using SPOTService.Infrastructure.InternalServices.Auth;
using SPOTService.Infrastructure.InternalServices.Auth.Models;
using System.Net;

namespace SPOTService.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserController(
        ILogger<UserController> logger, 
        IAuthService authService, 
        MainContext mainContext, 
        IMapper mapper,
        UserRepository repository
        ) : ControllerBase
    {
        private readonly ILogger<UserController> _logger = logger;
        private readonly IAuthService _authService = authService;
        private readonly MainContext _mainContext = mainContext;
        private readonly IMapper _mapper = mapper;
        private readonly UserRepository _repository = repository;
        
        [ProducesResponseType(typeof(UserOutputDto), 200)]
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {

            try
            {
                _logger.LogInformation("Try get user by id: {}", id);
                var user = await _repository.GetAsync(id);
                if (user == null) { NotFound(); }
                var userOutputDto = _mapper.Map<User, UserOutputDto>(user!);
                return Ok(userOutputDto);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error: {}", ex.Message);
                return BadRequest(ex.Message);
            }

        }
        
        [ProducesResponseType(typeof(UserOutputDto), 200)]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginInputDto userLogin)
        {
            try
            {
                var tokens = await _authService.Login(userLogin);
                var user = await _mainContext.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Login == userLogin.Login);
                var userOutputDto = _mapper.Map<User, UserOutputDto>(user!);
                userOutputDto.Tokens = tokens;
                return Ok(userOutputDto);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error: {}", ex.Message);
                return BadRequest(ex.Message);
            }
        }
        
        [ProducesResponseType(typeof(UserOutputDto), 200)]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserInputDto userInput)
        {
            try
            {
                var tokens = await _authService.Register(userInput);
                var user = await _mainContext.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Login == userInput.Login);
                var userOutputDto = _mapper.Map<User, UserOutputDto>(user!);
                userOutputDto.Tokens = tokens;
                userOutputDto.Role = userOutputDto.Role;
                return Ok(userOutputDto);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error: {}", ex.Message);
                return BadRequest(ex.Message);
            }
        }
        
        [ProducesResponseType(typeof(TokensResponse), 200)]
        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh()
        {
            try
            {
                if (Request.Headers.TryGetValue("authorization", out var token))
                {
                    if (!string.IsNullOrEmpty(token))
                    {
                        _logger.LogInformation("Token {}", token!);
                        var tokens = await _authService.Refresh(token.ToString().Split(' ')[1]);
                        return Ok(tokens);
                    }
                    else
                    {
                        _logger.LogWarning("Refresh token not found or empty");
                        return BadRequest("Refresh token not found or empty");
                    }
                }
                else
                {
                    _logger.LogWarning("Refresh token not found or empty");
                    return BadRequest("Refresh token not found or empty");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error: {}", ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
