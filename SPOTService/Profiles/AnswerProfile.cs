using AutoMapper;
using SPOTService.DataStorage.Entities;
using SPOTService.Dto.Answers;

namespace SPOTService.Profiles
{
    public class AnswerProfile : Profile
    {
        public AnswerProfile() {
            CreateMap<Answer, AnswerOutputDto>();
        }
    }
}
