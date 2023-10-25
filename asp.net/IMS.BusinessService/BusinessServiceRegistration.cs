using IMS.BusinessService.Common.Emails;
using IMS.BusinessService.Common.UnitOfWorks;
using IMS.BusinessService.Systems;
using IMS.Contract.Common.UnitOfWorks;
using IMS.Contract.Contents.Assignments;
using IMS.Contract.Contents.Milestones;
using IMS.Contract.Contents.Projects;
using IMS.Contract.Contents.Settings;
using IMS.Contract.Contents.Subjects;
using IMS.Contract.Systems.Authentications;
using IMS.Contract.Systems.Firebase;
using IMS.Contract.Systems.Roles;
using IMS.Contract.Systems.Users;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace IMS.BusinessService;

public static class BusinessServiceRegistration
{
    public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

		//
		services.AddScoped<Common.JwtSetting>();

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

        return services;
    }
}
