using AutoMapper;
using SPOTService.DataStorage.Entities;
using SPOTService.Dto.Surveys;

namespace SPOTService.Profiles
{
    public class SurveyProfile : Profile
    {
        public SurveyProfile() 
        {
            CreateMap<Survey, SurveyOutputDto>();
            CreateMap<SurveyInputDto, Survey>();
        }
    }
}
