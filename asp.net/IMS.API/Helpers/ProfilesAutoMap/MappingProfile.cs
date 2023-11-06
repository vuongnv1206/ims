using AutoMapper;
using IMS.Api.Dto.Assignments;
using IMS.Api.Dtos.Assignments;
using IMS.Api.Dtos.Classes;
using IMS.Api.Dtos.Issues;
using IMS.Api.Dtos.Milestones;
using IMS.Api.Dtos.Projects;
using IMS.Api.Dtos.Roles;
using IMS.Api.Dtos.Settings;
using IMS.Api.Dtos.Subjects;
using IMS.Api.Dtos.Users;
using IMS.Api.Models.Entities;

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

        //Issue
        CreateMap<CreateUpdateIssueDto, Class>().ReverseMap();
        CreateMap<IssueDto, Issue>().ReverseMap();

    }
}
