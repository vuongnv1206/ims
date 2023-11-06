using AutoMapper;
using IMS.Api.Models.Dtos.Users;
using IMS.Api.Models.Entities;

namespace IMS.Api.Common.Helpers.ProfilesAutoMap;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<AppUser, UserDto>();
    }
}
