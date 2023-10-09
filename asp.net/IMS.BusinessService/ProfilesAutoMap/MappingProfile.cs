﻿using AutoMapper;
using IMS.Contract.Contents.Assignments;
using IMS.Contract.Contents.Labels;
using IMS.Contract.Systems.Roles;
using IMS.Contract.Systems.Users;
using IMS.Domain.Contents;
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
			.ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber));

		//Assignment
		CreateMap<CreateUpdateAssignmentDTO ,Assignment>().ReverseMap();
		CreateMap<AssignmentDTO , Assignment>().ReverseMap();

        //Label
        CreateMap<CreateUpdateLabelDTO, Label>().ReverseMap();
        CreateMap<LabelDTO, Label>().ReverseMap();

    }
}
