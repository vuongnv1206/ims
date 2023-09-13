using IMS.Domain.Systems;
using IMS.Infrastructure.EnityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Infrastructure
{
	public static class IdentityServicesRegistration
	{
		public static IServiceCollection ConfigureIdentityServices(this IServiceCollection services, IConfiguration configuration)
		{

			services.AddIdentityCore<AppUser>().AddRoles<AppRole>()
				.AddEntityFrameworkStores<IMSDbContext>()
				.AddDefaultTokenProviders();
			//Add config for required Email
			services.Configure<IdentityOptions>(options => options.SignIn.RequireConfirmedEmail = true);

			//For reset password
			services.Configure<DataProtectionTokenProviderOptions>(options => options.TokenLifespan = TimeSpan.FromHours(1));

			//Adding Authentication
			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(options =>
			{
				options.SaveToken = true;
				options.RequireHttpsMetadata = false;
				options.TokenValidationParameters = new TokenValidationParameters()
				{
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidAudience = configuration["JWT:ValidAudience"],
					ValidIssuer = configuration["JWT:ValidIssuer"],
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
				};
			});

			//Singin-Google
			


			return services;
		}
	}
}
