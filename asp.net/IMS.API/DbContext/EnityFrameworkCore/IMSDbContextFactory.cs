using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Api.EnityFrameworkCore
{
	public class IMSDbContextFactory : IDesignTimeDbContextFactory<IMSDbContext>
	{
		public IMSDbContext CreateDbContext(string[] args)
		{

			var configuration = BuildConfiguration();
            var connectionString = configuration.GetConnectionString("Default");
            var builder = new DbContextOptionsBuilder<IMSDbContext>()
				.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

			return new IMSDbContext(builder.Options);
		}

		private static IConfigurationRoot BuildConfiguration()
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
				.AddJsonFile("appsettings.json", optional: false);


			return builder.Build();

		}
	}
}
