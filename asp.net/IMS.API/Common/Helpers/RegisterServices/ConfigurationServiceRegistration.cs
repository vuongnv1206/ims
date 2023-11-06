using IMS.Api.Common.Helpers.Settings;

namespace IMS.Api.Common.Helpers.RegisterServices;

public static class ConfigurationServiceRegistration
{
    public static IServiceCollection ConfigureSettingServices(this IServiceCollection services, IConfiguration configuration)
    {
        // using options pattern
        services.Configure<GitlabSetting>
            (configuration.GetSection(GitlabSetting.Gitlab));
        services.Configure<AppSetting>
            (configuration.GetSection(AppSetting.AppSettings));
        services.Configure<FirebaseSetting>
            (configuration.GetSection(FirebaseSetting.Firebase));
        services.Configure<EmailSetting>
            (configuration.GetSection(EmailSetting.EmailConfig));
        services.Configure<GoogleSetting>
            (configuration.GetSection(GoogleSetting.Google));

        return services;
    }
}
