using AutoMapper;
using SPOTService.DataStorage.Entities;
using SPOTService.Dto.User;

namespace SPOTService.Profiles
{
    /// <summary>
    /// Профиль для маппинга сущности User на DTO UserOutputDto и обратно.
    /// </summary>
    public class UserProfile : Profile
    {
        /// <summary>
        /// Конструктор профиля маппинга.
        /// </summary>
        public UserProfile()
        {
            CreateMap<UserInputDto, User>();
            CreateMap<User, UserOutputDto>();
        }
    }
}
