using AutoMapper;
using IMS.Contract.Systems.Roles;
using IMS.Contract.Systems.Users;
using IMS.Domain.Systems;

namespace IMS.BusinessService.ProfilesAutoMap;

public class MappingProfile : Profile 
{
	public MappingProfile()
	{	
		//User
		CreateMap<AppRole, RoleDto>().ReverseMap();
		CreateMap<AppUser, UserDto>().ReverseMap();


		//User
		CreateMap<CreateUserDto, AppUser>().ReverseMap();
		CreateMap<UpdateUserDto, AppUser>().ReverseMap()
			.ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
			.ForMember(dest => dest.Avatar, opt => opt.MapFrom(src => src.Avatar));

	}
}
