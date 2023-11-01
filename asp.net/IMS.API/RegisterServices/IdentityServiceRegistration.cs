using IMS.Api.EnityFrameworkCore;
using IMS.Api.Models.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace IMS.Api.RegisterServices
{
	public static class IdentityServicesRegistration
	{
		public static IServiceCollection ConfigureIdentityServices(this IServiceCollection services, IConfiguration configuration)
		{


			services.AddIdentity<AppUser, AppRole>(options => options.SignIn.RequireConfirmedAccount = false)
				.AddEntityFrameworkStores<IMSDbContext>()
				.AddDefaultTokenProviders();
			//Add config for required Email
			services.Configure<IdentityOptions>(options => options.SignIn.RequireConfirmedEmail = true);

			//For reset password
			services.Configure<DataProtectionTokenProviderOptions>(options => options.TokenLifespan = TimeSpan.FromDays(1));

			//Adding Authentication
			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				//options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(options =>
			{
				options.SaveToken = true;
				//options.RequireHttpsMetadata = false;
				options.TokenValidationParameters = new TokenValidationParameters()
				{
					ValidateIssuer = true,
					ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidAudience = configuration["JwtSettings:Audience"],
					ValidIssuer = configuration["JwtSettings:Issuer"],
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]))
				};
			});







			return services;
		}
	}
}
