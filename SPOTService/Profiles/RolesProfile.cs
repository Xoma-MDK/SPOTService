using AutoMapper;
using SPOTService.DataStorage.Entities;
using SPOTService.Dto.Roles;

namespace SPOTService.Profiles
{
    /// <summary>
    /// Профиль для маппинга сущности Role на DTO RoleOutputDto.
    /// </summary>
    public class RolesProfile : Profile
    {
        /// <summary>
        /// Конструктор профиля маппинга.
        /// </summary>
        public RolesProfile()
        {
            CreateMap<Role, RoleOutputDto>();
        }
    }
}
