using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace IMS.BusinessService;

public static class BusinessServiceRegistration
{
    public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        return services;
    }
}
