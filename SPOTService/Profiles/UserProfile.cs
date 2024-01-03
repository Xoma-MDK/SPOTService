using AutoMapper;
using SPOTService.DataStorage.Entities;
using SPOTService.Dto.User;

namespace SPOTService.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile() {
            CreateMap<User, UserOutputDto>();
        }
    }
}
