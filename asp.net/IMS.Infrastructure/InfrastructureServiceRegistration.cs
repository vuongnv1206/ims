using IMS.Infrastructure.EnityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Infrastructure
{
	public static class InfrastructureServiceRegistration
	{
		public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
		{

			services.AddDbContext<IMSDbContext>(options =>
			options.UseSqlServer(configuration.GetConnectionString("Default")));

			return services;

		}
	}
}
