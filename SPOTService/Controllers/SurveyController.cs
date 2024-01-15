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
            try
            {
                _logger.LogDebug("Try map SurveyInputDto to Survey");
                _logger.LogDebug("SurveyInputDto: {}", surveyInput.ToString());

                var survey = _mapper.Map<SurveyInputDto, Survey>(surveyInput);
                if (survey == null)
                {
                    _logger.LogError("Fail map SurveyInputDto to Survey");
                    return BadRequest("Fail map SurveyInputDto to Survey");
                }
                if (survey.UserId == 0) return BadRequest("User id can't be 0");
                if (survey.GroupId == 0) return BadRequest("Group id can't be 0");
                var surveyEntity = await _repository.AddAsync(survey!);
                if (surveyEntity == null)
                {
                    _logger.LogError("Fail add Survey to DB");
                    return BadRequest("Fail add Survey to DB");
                }

                var surveyOutputDto = _mapper.Map<Survey, SurveyOutputDto>(surveyEntity);

                return Ok(surveyOutputDto);
            }
            catch (Exception ex)
            {
                _logger.LogCritical("Crit: {}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        // PUT <SurveyController>/{id}
        /// <summary>
        /// Обновить опрос по идентификатору
        /// </summary>
        /// <remarks>Запрос для обновления опроса по идентификатору</remarks>
        /// <param name="id">Идентификатор опрос</param>
        /// <param name="surveyUpdate">Опрос</param>
        /// <response code="200">Успешно обновлен опрос по идентификатору</response>
        /// <response code="204">Опрос с указанным идентификатором отсутствует</response>
        [ProducesResponseType(typeof(SurveyOutputDto), StatusCodes.Status200OK)]
        [Authorize(AuthPolicy.AccessPolicy)]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id,
            [FromBody] SurveyUpdateDto surveyUpdate)
        {
            try { 
            Survey survey = _mapper.Map<SurveyUpdateDto, Survey>(surveyUpdate);
            if (survey == null)
            {
                    _logger.LogError("Fail map SurveyUpdateDto to Survey");
                    return BadRequest("Fail map SurveyUpdateDto to Survey");
            }
            var updatedSurvey = await _repository.UpdateAsync(id, survey);
            if (updatedSurvey == null)
            {
                    _logger.LogError("Fail update Survey to DB");
                    return BadRequest("Fail update Survey to DB");
            }
            SurveyOutputDto surveyOutput = _mapper.Map<Survey, SurveyOutputDto>(updatedSurvey);
            return Ok(surveyOutput);
            }
            catch (Exception ex)
            {
                _logger.LogCritical("Crit: {}", ex.Message);
                return BadRequest(ex.Message);
            }
        }
        // PUT <SurveyController>/{id}
        /// <summary>
        /// Обновить активность опроса по идентификатору
        /// </summary>
        /// <remarks>Запрос для обновления активности опроса по идентификатору</remarks>
        /// <param name="id">Идентификатор опроса</param>
        /// <param name="active">Флаг активности</param>
        /// <response code="200">Успешно обновлена активность опроса по идентификатору</response>
        /// <response code="204">Опрос с указанным идентификатором отсутствует</response>
        [ProducesResponseType(typeof(SurveyOutputDto), StatusCodes.Status200OK)]
        [Authorize(AuthPolicy.AccessPolicy)]
        [HttpPut("active/{id}")]
        public async Task<IActionResult> PutActiveAsync(int id, [FromQuery] bool active)
        {
            try
            {
                var surveyOld = await _repository.GetAsync(id); 
                if (surveyOld == null)
                {
                    return NotFound();
                }
                surveyOld.Active = active;
                var survey = await _repository.UpdateActiveAsync(id, surveyOld);
                if (survey == null)
                {
                    return BadRequest("Error: can't update Active!");
                }
                SurveyOutputDto surveyOutput = _mapper.Map<Survey, SurveyOutputDto>(survey);
                return Ok(surveyOutput);
            }
            catch (Exception ex)
            {
                _logger.LogCritical("Crit: {}", ex.Message);
                return BadRequest(ex.Message);
            }
        }
        // DELETE <SurveyController>/{id}
        /// <summary>
        /// Удалить опрос по идентификатору
        /// </summary>
        /// <remarks>Метод для удаления опроса по идентификатору</remarks>
        /// <param name="id">Идентификатор опроса</param>
        /// <response code="200">Успешно удален опрос по идентификатору</response>
        /// <response code="204">Опрос с указанным идентификатором отсутствует</response>
        [ProducesResponseType(typeof(IEnumerable<SurveyOutputDto>), StatusCodes.Status200OK)]
        [Authorize(AuthPolicy.AccessPolicy)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            Survey? survey = await _repository.GetAsync(id);
            if (survey == null)
            {
                return NoContent();
            }
            await _repository.DeleteAsync(id);
            return Ok((await _repository.GetAllAsync())
                .Select(_mapper.Map<Survey, SurveyOutputDto>)
                .ToList());
        }
    }
}
