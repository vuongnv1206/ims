using AutoMapper;
using IMS.Api.Dtos.Users;
using IMS.Api.Models.Entities;

namespace IMS.Api.Helpers.ProfilesAutoMap;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<AppUser, UserDto>();
    }
}
