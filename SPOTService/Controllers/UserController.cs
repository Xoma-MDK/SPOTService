using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SPOTService.DataStorage;
using SPOTService.DataStorage.Entities;
using SPOTService.DataStorage.Repositories;
using SPOTService.Dto.Roles;
using SPOTService.Dto.User;
using SPOTService.Infrastructure.InternalServices.Auth;
using SPOTService.Infrastructure.InternalServices.Auth.Constants;
using SPOTService.Infrastructure.InternalServices.Auth.Models;

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
    /// <param name="roleRepository"></param>
    [ApiController]
    [Route("users")]
    public class UserController(
        ILogger<UserController> logger,
        IAuthService authService,
        MainContext mainContext,
        IMapper mapper,
        UserRepository repository,
        RoleRepository roleRepository
        ) : ControllerBase
    {
        private readonly ILogger<UserController> _logger = logger;
        private readonly IAuthService _authService = authService;
        private readonly MainContext _mainContext = mainContext;
        private readonly IMapper _mapper = mapper;
        private readonly UserRepository _repository = repository;
        private readonly RoleRepository _roleRepository = roleRepository;

        // GET <UserController>/{id}
        /// <summary>
        /// Получить все роли
        /// </summary>
        /// <remarks>Запрос для получения всех ролей</remarks>
        /// <response code="200">Успешно получены роли</response>
        [ProducesResponseType(typeof(IEnumerable<RoleOutputDto>), 200)]
        [Authorize(AuthPolicy.AccessPolicy)]
        [HttpGet("roles")]
        public async Task<IActionResult> GetRoles()
        {
            var entity = await _roleRepository.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<Role>, IEnumerable<RoleOutputDto>>(entity));
        }

        // GET <UserController>/{id}
        /// <summary>
        /// Получить всех пользователей
        /// </summary>
        /// <remarks>Запрос для получения всех пользователей</remarks>
        /// <response code="200">Успешно получены пользователи</response>
        [ProducesResponseType(typeof(IEnumerable<UserOutputDto>), 200)]
        [Authorize(AuthPolicy.AccessPolicy)]
        [HttpGet()]
        public async Task<IActionResult> GetUsers()
        {
            var entity = await _repository.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<User>, IEnumerable<UserOutputDto>>(entity));
        }
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
                userOutputDto!.Tokens = tokens;
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
        // Put <SurveyController>/register
        /// <summary>
        /// Обновить пользователя
        /// </summary>
        /// <remarks>Запрос для обновления пользователя</remarks>
        /// <response code="200">Успешно обновлен пользователь</response>
        /// <response code="400">Ошибка при создании пользователя</response>
        [ProducesResponseType(typeof(UserOutputDto), 200)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UserInputDto userInput, [FromHeader] int? userId)
        {
            try
            {
                var staff = await _mainContext.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Id == userId);
                if (staff == null || staff.RoleId != 1)
                    return Forbid();
                var user = await _mainContext.Users.FirstOrDefaultAsync(u => u.Id == id);
                if (user == null)
                    return BadRequest();
                user.Surname = userInput.Surname;
                user.Name = userInput.Name;
                user.Patronomyc = userInput.Patronomyc;
                user.Login = userInput.Login;
                user.RoleId = userInput.RoleId;
                user.Role = null;
                _mainContext.Update(user);
                await _mainContext.SaveChangesAsync();
                var userOutputDto = _mapper.Map<User, UserOutputDto>(user!);
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

        // POST <SurveyController>/logout
        /// <summary>
        /// Выйти из системы
        /// </summary>
        /// <remarks>Запрос для выхода из системы</remarks>
        /// <response code="200">Успешно выполнен выход из системы</response>
        /// <response code="400">Ошибка при выходе из системы</response>
        [Authorize(AuthPolicy.AccessPolicy)]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                if (Request.Headers.TryGetValue("authorization", out var token))
                {

                    await _authService.Logout(token.ToString().Split(' ')[1]);
                }
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error: {}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        // DELETE <SurveyController>/<Id>
        /// <summary>
        /// Удалить пользователя
        /// </summary>
        /// <remarks>Запрос для удаления пользователя</remarks>
        /// <response code="200">Успешно удален пользователь</response>
        /// <response code="400">Ошибка при удалении пользователя</response>
        [Authorize(AuthPolicy.AccessPolicy)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id, [FromHeader] int? UserId)
        {
            try
            {
                var staff = await _mainContext.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Id == UserId);
                if (staff == null || staff.RoleId != 1 || staff.Id == id)
                    return Forbid();
                var user = await _repository.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error: {}", ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
