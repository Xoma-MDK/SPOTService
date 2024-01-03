using AutoMapper;
using SPOTService.DataStorage.Entities;
using SPOTService.Dto.Roles;

namespace SPOTService.Profiles
{
    public class RolesProfile: Profile
    {
        public RolesProfile()
        {
            CreateMap<Role, RoleOutputDto>();
        }
    }
}
