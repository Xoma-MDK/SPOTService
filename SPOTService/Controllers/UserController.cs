using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SPOTService.DataStorage;
using SPOTService.DataStorage.Entities;
using SPOTService.DataStorage.Repositories;
using SPOTService.Dto.Surveys;
using SPOTService.Dto.User;
using SPOTService.Infrastructure.InternalServices.Auth;
using SPOTService.Infrastructure.InternalServices.Auth.Constants;
using SPOTService.Infrastructure.InternalServices.Auth.Models;
using System.Net;

namespace SPOTService.Controllers
{
    /// <summary>
    /// Контроллер запросов к сущности "пользователи"
    /// </summary>
    /// <param name="logger">Логгер</param>
    /// <param name="authService">Сервис авторизации</param>
    /// <param name="mainContext">Контекст базы данных</param>
    /// <param name="mapper">Маппер</param>
    /// <param name="repository">Репозиторий "пользователи"</param>
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

        // GET <UserController>/{id}
        /// <summary>
        /// Получить пользователя по идентификатору
        /// </summary>
        /// <remarks>Запрос для получения пользователя по идентификатору</remarks>
        /// <param name="id">Идентификатор пользователя</param>
        /// <response code="200">Успешно получен пользователь по идентификатору</response>
        /// <response code="204">Пользователь с указанным идентификатором отсутствует</response>
        [ProducesResponseType(typeof(UserOutputDto), 200)]
        [Authorize(AuthPolicy.AccessPolicy)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            User? entity = await _repository.GetAsync(id);
            return (entity == null) ? NoContent() : Ok(_mapper.Map<User, UserOutputDto>(entity));
        }

        // POST <SurveyController>/login
        /// <summary>
        /// Создать токены авторизации пользователя
        /// </summary>
        /// <remarks>Запрос для создания токенов авторизации пользователя</remarks>
        /// <response code="200">Успешно созданы токены авторизации пользователя</response>
        /// <response code="400">Ошибка при создании токенов авторизации пользователя</response>
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
        // POST <SurveyController>/register
        /// <summary>
        /// Создать пользователя
        /// </summary>
        /// <remarks>Запрос для создания пользователя</remarks>
        /// <response code="200">Успешно создан пользователь</response>
        /// <response code="400">Ошибка при создании пользователя</response>
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
        // POST <SurveyController>/login
        /// <summary>
        /// Создать токены авторизации пользователя по Refresh токену
        /// </summary>
        /// <remarks>Запрос для создания токенов авторизации пользователя по Refresh токену</remarks>
        /// <response code="200">Успешно созданы токены авторизации пользователя по Refresh токену</response>
        /// <response code="400">Ошибка при создании токенов авторизации пользователя по Refresh токену</response>
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
