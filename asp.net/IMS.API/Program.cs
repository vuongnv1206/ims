

using IMS.BusinessService;
using IMS.Infrastructure;
using Microsoft.Extensions.Configuration;

namespace IMS.Api
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			var configuration = builder.Configuration;
			builder.Services.AddHttpContextAccessor();
	
			builder.Services.ConfigureApplicationServices();
			builder.Services.ConfigureInfrastructureServices(configuration);
			builder.Services.ConfigureIdentityServices(configuration);

			builder.Services.AddCors(o =>
			{
				o.AddPolicy("CorsPolicy",
					builder => builder.AllowAnyOrigin()
					.AllowAnyMethod()
					.AllowAnyHeader());
			});

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();
			app.UseAuthentication();
			app.UseAuthorization();
			app.UseCors("CorsPolicy");

			app.MapControllers();

			app.Run();
		}
	}
}