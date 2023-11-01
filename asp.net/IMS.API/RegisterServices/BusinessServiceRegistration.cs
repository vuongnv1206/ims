using IMS.Api.Helpers.Emails;
using IMS.Api.Services.UnitOfWorks;
using IMS.BusinessService.Common.Emails;
using IMS.BusinessService.Common.UnitOfWorks;
using IMS.BusinessService.Systems;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Reflection;

namespace IMS.Api.RegisterServices;

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

        //ClassSeervice
        services.AddScoped<IClassService, ClassService>();
        
		return services;
    }
}
