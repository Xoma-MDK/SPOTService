using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SPOTService.DataStorage.Repositories;
using SPOTService.DataStorage;
using SPOTService.Infrastructure.InternalServices.Auth;
using Microsoft.AspNetCore.Authorization;
using SPOTService.Dto.User;
using SPOTService.Infrastructure.InternalServices.Auth.Constants;
using SPOTService.Dto.Groups;
using SPOTService.DataStorage.Entities;
using Telegram.Bot.Types;

namespace SPOTService.Controllers
{
    /// <summary>
    /// Контроллер запросов к сущности "группы"
    /// </summary>
    /// <param name="logger">Логгер</param>
    /// <param name="mapper">Маппер</param>
    /// <param name="repository">Репозиторий "групп"</param>
    [ApiController]
    [Route("groups")]
    public class GroupController(
        ILogger<GroupController> logger,
        IMapper mapper,
        GroupRepository repository
        ) : ControllerBase
    {
        private readonly ILogger<GroupController> _logger = logger;
        private readonly IMapper _mapper = mapper;
        private readonly GroupRepository _repository = repository;

        // GET <GroupController>/{id}
        /// <summary>
        /// Получить группу по идентификатору
        /// </summary>
        /// <remarks>Запрос для получения группы по идентификатору</remarks>
        /// <param name="id">Идентификатор группы</param>
        /// <response code="200">Успешно получена группа по идентификатору</response>
        /// <response code="204">Группа с указанным идентификатором отсутствует</response>
        [ProducesResponseType(typeof(GroupOutputDto), 200)]
        [Authorize(AuthPolicy.AccessPolicy)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGroupById(int id)
        {

            try
            {
                _logger.LogInformation("Try get group by id: {}", id);
                var group = await _repository.GetAsync(id);
                if (group == null) { NotFound(); }
                var groupOutputDto = _mapper.Map<Group, GroupOutputDto>(group!);
                return Ok(groupOutputDto);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error: {}", ex.Message);
                return BadRequest(ex.Message);
            }

        }
        // GET: <GroupController>
        /// <summary>
        /// Получить все группы
        /// </summary>
        /// <remarks>Запрос для получения всех групп</remarks>
        /// <response code="200">Успешно получены все группы</response>
        [ProducesResponseType(typeof(IEnumerable<GroupOutputDto>), 200)]
        [Authorize(AuthPolicy.AccessPolicy)]
        [HttpGet]
        public async Task<IActionResult> GetGroups()
        {
            
            try
            {
                _logger.LogInformation("Try get groups");
                return Ok( (await _repository.GetAllAsync())
                    .Select(_mapper.Map<Group, GroupOutputDto>)
                    .ToList());
            }
            catch (Exception ex)
            {
                _logger.LogError("Error: {}", ex.Message);
                return BadRequest(ex.Message);
            }

        }
    }
}
