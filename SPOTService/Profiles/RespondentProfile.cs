using AutoMapper;
using SPOTService.DataStorage.Entities;
using SPOTService.Dto.Respondent;

namespace SPOTService.Profiles
{
    public class RespondentProfile : Profile
    {
        public RespondentProfile()
        {
            CreateMap<Respondent, RespondentOutputDto>();
        }
    }
}
