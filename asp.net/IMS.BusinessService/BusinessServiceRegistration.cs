using IMS.BusinessService.Common.UnitOfWorks;
using IMS.BusinessService.Systems;
using IMS.Contract.Common.UnitOfWorks;
using IMS.Contract.Contents.Subjects;
using IMS.Contract.Systems.Authentications;
using IMS.Contract.Systems.Firebase;
using IMS.Contract.Systems.Roles;
using IMS.Contract.Systems.Users;
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
		services.AddScoped<ISubjectService,SubjectService>();
		//Generic Repo
		services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
		services.AddScoped<IUnitOfWork, UnitOfWork>();


		// firebase
		services.AddScoped<IFirebaseService, FirebaseService>();

        return services;
    }
}
