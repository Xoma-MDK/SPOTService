using AutoMapper;
using SPOTService.DataStorage.Entities;
using SPOTService.Dto.AnswerVariants;

namespace SPOTService.Profiles
{
    public class AnswerVariantProfile : Profile
    {
        public AnswerVariantProfile() 
        {
            CreateMap<AnswerVariant, AnswerVariantOutputDto>();
            CreateMap<AnswerVariantInputDto, AnswerVariant>();
            CreateMap<AnswerVariantUpdateDto, AnswerVariant>();
        }
    }
}
