using AutoMapper;
using IMS.Contract.Systems.Users;
using IMS.Domain.Systems;

namespace IMS.Api.Helpers.ProfilesAutoMap;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<AppUser, UserDto>();
    }
}
