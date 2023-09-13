using IMS.BusinessService.Common.UnitOfWorks;
using IMS.BusinessService.Systems;
using IMS.Contract.Common.UnitOfWorks;
using IMS.Contract.Systems.Authentications.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace IMS.BusinessService;

public static class BusinessServiceRegistration
{
    public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

		//AuthService
		services.AddTransient<IAuthService, AuthService>();
		services.AddTransient<IUserService, UserService>();

		//Generic Repo
		services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
		services.AddScoped<IUnitOfWork, UnitOfWork>();

		return services;
    }
}
