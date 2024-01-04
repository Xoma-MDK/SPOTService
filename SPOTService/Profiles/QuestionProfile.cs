using AutoMapper;
using SPOTService.DataStorage.Entities;
using SPOTService.Dto.Questions;

namespace SPOTService.Profiles
{
    public class QuestionProfile : Profile
    {
        public QuestionProfile() { 
            CreateMap<Question, QuestionOutputDto>();
            CreateMap<QuestionIntputDto, Question>();
        }
    }
}
