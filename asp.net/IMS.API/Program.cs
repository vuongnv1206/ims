using IMS.Api.Common.Helpers.RegisterServices;
using IMS.Api.Common.Helpers.Settings;
using Microsoft.OpenApi.Models;

namespace IMS.Api
{
    public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
			var configuration = builder.Configuration;
			// Add services to the container.
			builder.Services.AddHttpContextAccessor();

			builder.Services.ConfigureInfrastructureServices(configuration);
			builder.Services.ConfigureIdentityServices(configuration);
			builder.Services.ConfigureApplicationServices();
			builder.Services.ConfigureInfrastructureServices(configuration);

			builder.Services.Configure<JwtSetting>(configuration.GetSection("JwtSettings"));

            var IMSCorsPolicy = "IMSCorsPolicy";
            builder.Services.AddCors(o => o.AddPolicy(IMSCorsPolicy, builder =>
            {
                builder.AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithOrigins(configuration["AllowedOrigins"])
                    .AllowCredentials();
            }));
            builder.Services.ConfigureSettingServices(configuration);
            builder.Services.AddHttpClient();
            builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

            //

            builder.Services.AddAuthentication().AddGoogle(options =>
            {
                IConfigurationSection authenticationSection = configuration.GetSection("Authentication");
                string googleClientId = authenticationSection["Google:ClientId"];
                string googleClientSecret = authenticationSection["Google:ClientSecret"];
                options.ClientId = googleClientId;
                options.ClientSecret = googleClientSecret;
                //options.CallbackPath = "/Auth/Signin-google";
            });


            ////Them doan nay ->
            builder.Services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "IMS API", Version = "V1" });

                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type=ReferenceType.SecurityScheme,
                                    Id="Bearer"
                                },
                                  Scheme = "oauth2",
                                  Name = "Bearer",
                                  In = ParameterLocation.Header,
                            },
                            new List<string>()
                            }
                        });
            });


            var app = builder.Build();

			// Configure the HTTP request pipeline.

			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
				
			}


            app.UseCors(IMSCorsPolicy);
            app.UseHttpsRedirection();
			app.UseAuthentication();
			app.UseAuthorization();
			

			app.MapControllers();

			app.Run();
		}
	}
}