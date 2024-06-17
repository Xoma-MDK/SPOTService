using AutoMapper;
using SPOTService.DataStorage.Entities;
using SPOTService.Dto.Answers;

namespace SPOTService.Profiles
{
    /// <summary>
    /// Профиль AutoMapper для маппинга модели Answer на DTO AnswerOutputDto.
    /// </summary>
    public class AnswerProfile : Profile
    {
        /// <summary>
        /// Конструктор профиля маппинга AnswerProfile.
        /// </summary>
        public AnswerProfile()
        {
            CreateMap<Answer, AnswerOutputDto>();
        }
    }
}
