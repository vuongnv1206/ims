using IMS.Api.Common.Helpers.Emails;
using IMS.Api.Common.Helpers.Firebase;
using IMS.Api.Common.Helpers.Settings;
using IMS.Api.Common.UnitOfWorks;
using IMS.Api.Interfaces;
using IMS.Api.Services;
using IMS.Api.Services.UnitOfWorks;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Reflection;

namespace IMS.Api.Common.Helpers.RegisterServices;

public static class BusinessServiceRegistration
{
    public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        //
        services.AddScoped<JwtSetting>();

        //AuthService
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IRoleService, RoleService>();

        //AssignmentService
        services.AddScoped<IAssignmentService, AssignmentService>();


        //SubjectService
        services.AddScoped<ISubjectService, SubjectService>();

        //Generic Repo
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        //Milestone
        services.AddScoped<IMilestoneService, MilestoneService>();
        // firebase
        services.AddScoped<IFirebaseService, FirebaseService>();

        //email
        services.AddSingleton<IEmailSender, EmailService>();

        // setting
        services.AddScoped<ISettingService, SettingService>();

        //ProjectService
        services.AddScoped<IProjectService, ProjectService>();

        //ClassService
        services.AddScoped<IClassService, ClassService>();

        //IssueService
        services.AddScoped<IIssueService, IssueService>();

        //ProjectMemberService
        services.AddScoped<IProjectMemberService, ProjectMemberService>();

        //ClassStudents
        services.AddScoped<IClassStudentService, ClassStudentService>();
        return services;
    }
}
