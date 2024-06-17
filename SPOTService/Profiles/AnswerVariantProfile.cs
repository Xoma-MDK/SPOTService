using AutoMapper;
using SPOTService.DataStorage.Entities;
using SPOTService.Dto.AnswerVariants;

namespace SPOTService.Profiles
{
    /// <summary>
    /// Профиль AutoMapper для маппинга модели AnswerVariant на DTO AnswerVariantOutputDto и обратно.
    /// </summary>
    public class AnswerVariantProfile : Profile
    {
        /// <summary>
        /// Конструктор профиля маппинга AnswerVariantProfile.
        /// </summary>
        public AnswerVariantProfile()
        {
            CreateMap<AnswerVariant, AnswerVariantOutputDto>();
            CreateMap<AnswerVariantInputDto, AnswerVariant>();
            CreateMap<AnswerVariantUpdateDto, AnswerVariant>();
        }
    }
}
