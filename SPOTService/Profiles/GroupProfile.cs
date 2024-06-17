using AutoMapper;
using SPOTService.DataStorage.Entities;
using SPOTService.Dto.Groups;

namespace SPOTService.Profiles
{
    /// <summary>
    /// Профиль для маппинга сущности Group на DTO GroupOutputDto.
    /// </summary>
    public class GroupProfile : Profile
    {
        /// <summary>
        /// Конструктор профиля маппинга.
        /// </summary>
        public GroupProfile()
        {
            CreateMap<Group, GroupOutputDto>();
        }
    }
}
