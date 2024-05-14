using AutoMapper;
using SPOTService.DataStorage.Entities;
using SPOTService.Dto.QuestionGroup;

namespace SPOTService.Profiles
{
    public class QuestionGroupProfile : Profile
    {
        public QuestionGroupProfile()
        {
            CreateMap<QuestionGroup, QuestionGroupOutputDto>();

            CreateMap<QuestionGroupInputDto, QuestionGroup>();
        }
    }
}
