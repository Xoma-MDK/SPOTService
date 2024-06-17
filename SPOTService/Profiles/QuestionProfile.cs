using AutoMapper;
using SPOTService.DataStorage.Entities;
using SPOTService.Dto.Questions;

namespace SPOTService.Profiles
{
    /// <summary>
    /// Профиль для маппинга сущности Question на DTO QuestionOutputDto и обратно.
    /// </summary>
    public class QuestionProfile : Profile
    {
        /// <summary>
        /// Конструктор профиля маппинга.
        /// </summary>
        public QuestionProfile()
        {
            CreateMap<Question, QuestionOutputDto>();
            CreateMap<QuestionIntputDto, Question>();
            CreateMap<QuestionUpdateDto, Question>();
        }
    }
}
