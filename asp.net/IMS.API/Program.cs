

using IMS.BusinessService;
using IMS.BusinessService.Common;
using IMS.Infrastructure;
using IMS.Api.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

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
				o.AddPolicy("CorsPolicy",builder => builder.AllowAnyOrigin()
					.AllowAnyMethod()
					.AllowAnyHeader());
			});
			builder.Services.ConfigureSettingServices(configuration);
            builder.Services.AddHttpClient();
            builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			//builder.Services.AddSwaggerGen();
			builder.Services.AddSwaggerGen(c =>
			{
				c.CustomOperationIds(apiDesc =>
				{
					return apiDesc.TryGetMethodInfo(out MethodInfo methodInfo) ? methodInfo.Name : null;
				});
				c.SwaggerDoc("api", new Microsoft.OpenApi.Models.OpenApiInfo
				{
					Version = "v1",
					Title = "API",
					Description = "API for core domain. This domain keeps track of campaigns, campaign rules, and campaign execution."
				});
				c.ParameterFilter<SwaggerNullableParameterFilter>();
			});

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			//if (app.Environment.IsDevelopment())
			//{
			//	app.UseSwagger();
			//	app.UseSwaggerUI();
			//}
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				//app.UseSwaggerUI();
				app.UseSwaggerUI(c =>
				{
					c.SwaggerEndpoint("api/swagger.json", " API");
					c.DisplayOperationId();
					c.DisplayRequestDuration();
				});
			}

			app.UseCors("CorsPolicy");
			app.UseHttpsRedirection();
			app.UseAuthentication();
			app.UseAuthorization();
			

			app.MapControllers();

			app.Run();
		}
	}
}