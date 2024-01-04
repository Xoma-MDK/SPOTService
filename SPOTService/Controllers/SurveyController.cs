using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SPOTService.DataStorage.Entities;
using SPOTService.DataStorage.Repositories;
using SPOTService.Dto.Surveys;
using SPOTService.Infrastructure.InternalServices.Auth.Constants;

namespace SPOTService.Controllers
{
    /// <summary>
    /// Контроллер запросов к сущности "опросы"
    /// </summary>
    /// <param name="logger">Логгер</param>
    /// <param name="mapper">Маппер</param>
    /// <param name="repository">Репозиторий "опросов"</param>
    [ApiController]
    [Route("survey")]
    public class SurveyController(
        ILogger<SurveyController> logger,
        IMapper mapper,
        SurveyRepository repository
        ) : ControllerBase
    {
        private readonly ILogger<SurveyController> _logger = logger;
        private readonly IMapper _mapper = mapper;
        private readonly SurveyRepository _repository = repository;

        // GET: <SurveyController>
        /// <summary>
        /// Получить все опросы
        /// </summary>
        /// <remarks>Запрос для получения всех опросов</remarks>
        /// <response code="200">Успешно получены все опросы</response>
        [ProducesResponseType(typeof(IEnumerable<SurveyOutputDto>), StatusCodes.Status200OK)]
        [Authorize(AuthPolicy.AccessPolicy)]
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok((await _repository.GetAllAsync())
                .Select(x => _mapper.Map<Survey, SurveyOutputDto>(x))
                .ToList());
        }

        // GET <SurveyController>/{id}
        /// <summary>
        /// Получить опрос по идентификатору
        /// </summary>
        /// <remarks>Запрос для получения опроса по идентификатору</remarks>
        /// <param name="id">Идентификатор опроса</param>
        /// <response code="200">Успешно получен опрос по идентификатору</response>
        /// <response code="204">Опрос с указанным идентификатором отсутствует</response>
        [ProducesResponseType(typeof(SurveyOutputDto), StatusCodes.Status200OK)]
        [Authorize(AuthPolicy.AccessPolicy)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            Survey? entity = await _repository.GetAsync(id);
            return (entity == null) ? NoContent() : Ok(_mapper.Map<Survey, SurveyOutputDto>(entity));
        }

        // POST <SurveyController>
        /// <summary>
        /// Создать опрос
        /// </summary>
        /// <remarks>Запрос для создания опрос</remarks>
        /// <response code="200">Успешно создан опрос</response>
        /// <response code="400">Ошибка при добавлении опроса</response>
        [ProducesResponseType(typeof(SurveyOutputDto), StatusCodes.Status200OK)]
        [Authorize(AuthPolicy.AccessPolicy)]
        [HttpPost]
        public async Task<IActionResult> PostAsync(
            [FromBody] SurveyInputDto surveyInput)
        {
            _logger.LogInformation("Try map SurveyInputDto to Survey");
            _logger.LogDebug("SurveyInputDto: {}", surveyInput.ToString());
            
            var survey = _mapper.Map<SurveyInputDto,Survey>(surveyInput);
            if (survey == null)
            {
                _logger.LogWarning("Fail map SurveyInputDto to Survey");
                return BadRequest("Fail map SurveyInputDto to Survey");
            }

            var surveyEntity = await _repository.AddAsync(survey!);
            if (surveyEntity == null)
            {
                _logger.LogError("Fail add Survey to DB");
                return BadRequest("Fail add Survey to DB");
            }

            var surveyOutputDto = _mapper.Map<Survey, SurveyOutputDto>(surveyEntity);
            
            return Ok(surveyOutputDto);
        }
    }
}
