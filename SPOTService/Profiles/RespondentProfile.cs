using AutoMapper;
using SPOTService.DataStorage.Entities;
using SPOTService.Dto.Respondent;

namespace SPOTService.Profiles
{
    /// <summary>
    /// Профиль для маппинга сущности Respondent на DTO RespondentOutputDto.
    /// </summary>
    public class RespondentProfile : Profile
    {
        /// <summary>
        /// Конструктор профиля маппинга.
        /// </summary>
        public RespondentProfile()
        {
            CreateMap<Respondent, RespondentOutputDto>();
        }
    }
}
