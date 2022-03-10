using AirportDistanceCalculator.Application;
using AirportDistanceCalculator.Core;
using AirportDistanceCalculator.External.AirportService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AirportDistanceCalculator.WebApi
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers();
			services.AddHttpClient();
			services.AddSwaggerGen();

			services.Configure<AppSettings>(Configuration.GetSection(AppSettings.SectionName));

			services.AddScoped<IAirportClient, AirportClient>();
			services.AddScoped<IAirportEngine, AirportEngine>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			app.UseExceptionHandler("/error");

			app.UseSwagger();
			app.UseSwaggerUI();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
