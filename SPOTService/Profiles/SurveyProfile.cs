using AutoMapper;
using SPOTService.DataStorage.Entities;
using SPOTService.Dto.Surveys;

namespace SPOTService.Profiles
{
    /// <summary>
    /// Профиль для маппинга сущности Survey на DTO SurveyOutputDto и обратно.
    /// </summary>
    public class SurveyProfile : Profile
    {
        /// <summary>
        /// Конструктор профиля маппинга.
        /// </summary>
        public SurveyProfile()
        {
            CreateMap<Survey, SurveyOutputDto>();
            CreateMap<SurveyInputDto, Survey>();
            CreateMap<SurveyUpdateDto, Survey>();
        }
    }
}
