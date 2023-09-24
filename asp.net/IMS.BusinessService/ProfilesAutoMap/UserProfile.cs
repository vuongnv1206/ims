using AutoMapper;
using IMS.Contract.Systems.Users;
using IMS.Domain.Systems;

namespace IMS.BusinessService.ProfilesAutoMap;

public class UserProfile : Profile
{
	public UserProfile()
	{
		CreateMap<AppUser, UserDto>();
	}
}
