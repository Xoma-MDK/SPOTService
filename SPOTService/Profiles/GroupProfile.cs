using AutoMapper;
using SPOTService.DataStorage.Entities;
using SPOTService.Dto.Groups;

namespace SPOTService.Profiles
{
    public class GroupProfile: Profile
    {
        public GroupProfile() {
            CreateMap<Group, GroupOutputDto>();
        }
    }
}
