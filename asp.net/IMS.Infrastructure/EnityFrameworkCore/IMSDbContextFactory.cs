using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Infrastructure.EnityFrameworkCore
{
	public class IMSDbContextFactory : IDesignTimeDbContextFactory<IMSDbContext>
	{
		public IMSDbContext CreateDbContext(string[] args)
		{

			var configuration = BuildConfiguration();

			var builder = new DbContextOptionsBuilder<IMSDbContext>()
				.UseSqlServer(configuration.GetConnectionString("Default"));

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
