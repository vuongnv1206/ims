

using IMS.BusinessService;
using IMS.BusinessService.Common;
using IMS.Infrastructure;
using Microsoft.Extensions.Configuration;
using IMS.Infrastructure;

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


			builder.Services.AddCors(o =>
			{
				o.AddPolicy("CorsPolicy",
					builder => builder.AllowAnyOrigin()
					.AllowAnyMethod()
					.AllowAnyHeader());
			});
			builder.Services.ConfigureSettingServices(configuration);

            builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			var app = builder.Build();

			app.UsePathBase("/api");

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseCors("CorsPolicy");
			//app.UseRouting();

			app.UseHttpsRedirection();
			app.UseAuthentication();
			app.UseAuthorization();
			

			app.MapControllers();

			app.Run();
		}
	}
}