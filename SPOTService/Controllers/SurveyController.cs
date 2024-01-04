using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SPOTService.DataStorage.Entities;
using SPOTService.DataStorage.Repositories;
using SPOTService.Dto.Surveys;

namespace SPOTService.Controllers
{
    [ApiController]
    [Route("survey")]
    public class SurveyController(
        ILogger<SurveyController> logger,
        IMapper mapper,
        SurveyRepository repository,
        QuestionRepository questionRepository,
        AnswerVariantRepository answerVariantRepository
        ) : ControllerBase
    {
        private readonly ILogger<SurveyController> _logger = logger;
        private readonly IMapper _mapper = mapper;
        private readonly SurveyRepository _repository = repository;
        private readonly QuestionRepository _questionRepository = questionRepository;
        private readonly AnswerVariantRepository _answerVariantRepository = answerVariantRepository;

        [ProducesResponseType(typeof(IEnumerable<SurveyOutputDto>), StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok((await _repository.GetAllAsync())
                .Select(x => _mapper.Map<Survey, SurveyOutputDto>(x))
                .ToList());
        }


        [ProducesResponseType(typeof(SurveyOutputDto), StatusCodes.Status200OK)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            Survey? entity = await _repository.GetAsync(id);
            return (entity == null) ? NoContent() : Ok(_mapper.Map<Survey, SurveyOutputDto>(entity));
        }

        [ProducesResponseType(typeof(SurveyOutputDto), StatusCodes.Status200OK)]
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
