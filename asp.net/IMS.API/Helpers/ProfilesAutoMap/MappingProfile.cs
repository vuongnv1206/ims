using AutoMapper;
using IMS.Api.Dtos.Roles;
using IMS.Contract.Contents.Classes;

using IMS.Contract.Contents.Subjects;

namespace IMS.Api.Helpers.ProfilesAutoMap;

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
        CreateMap<CreateUpdateAssignmentDTO, Assignment>().ReverseMap();
        CreateMap<AssignmentDTO, Assignment>().ReverseMap();


        //Subject 
        CreateMap<CreateUpdateSubjectDto, Subject>().ReverseMap();
        CreateMap<SubjectDto, Subject>().ReverseMap();

        // setting
        CreateMap<SettingDto, Setting>().ReverseMap();
        CreateMap<CreateUpdateSetting, Setting>().ReverseMap();

        // milestone
        CreateMap<MilestoneDto, Milestone>().ReverseMap();
        CreateMap<CreateMilestoneDto, Milestone>().ReverseMap();
        CreateMap<UpdateMilestoneDto, Milestone>().ReverseMap();

        //Project 
        CreateMap<CreateAndUpdateProjectDto, Project>().ReverseMap();
        CreateMap<ProjectDto, Project>().ReverseMap();

        //Class
        CreateMap<CreateAndUpdateClassDto, Class>().ReverseMap();
        CreateMap<ClassDto, Class>().ReverseMap();

    }
}
