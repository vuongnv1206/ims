using AutoMapper;
using IMS.Api.Models.Dtos.Assignments;
using IMS.Api.Models.Dtos.Classes;
using IMS.Api.Models.Dtos.Issues;
using IMS.Api.Models.Dtos.Milestones;
using IMS.Api.Models.Dtos.Projects;
using IMS.Api.Models.Dtos.Roles;
using IMS.Api.Models.Dtos.Settings;
using IMS.Api.Models.Dtos.Subjects;
using IMS.Api.Models.Dtos.Users;
using IMS.Api.Models.Entities;

namespace IMS.Api.Common.Helpers.ProfilesAutoMap;

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
        CreateMap<CreateUpdateIssueDto, Issue>().ReverseMap();
        CreateMap<IssueDto, Issue>().ReverseMap();

    }
}
