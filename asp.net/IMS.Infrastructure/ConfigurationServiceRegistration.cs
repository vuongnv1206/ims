using IMS.Contract.Systems.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IMS.Infrastructure;

public static class ConfigurationServiceRegistration
{
    public static IServiceCollection ConfigureSettingServices(this IServiceCollection services, IConfiguration configuration)
    {
        // using options pattern
        services.Configure<GitlabSetting>
            (configuration.GetSection(GitlabSetting.Gitlab));
        services.Configure<AppSetting>
            (configuration.GetSection(AppSetting.AppSettings));

        return services;
    }
}
