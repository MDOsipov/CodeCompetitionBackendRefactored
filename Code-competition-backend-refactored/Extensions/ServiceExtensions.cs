using Microsoft.EntityFrameworkCore;
using Entities;
using Contracts;
using Repository;
using LoggerService;

namespace Code_competition_backend_refactored.Extensions
{
	public static class ServiceExtensions
	{
		public static void ConfigureCors (this IServiceCollection services)
		{
			services.AddCors(policy =>
			{
				policy.AddPolicy("CorsPolicy", opt => opt
					.AllowAnyOrigin()
					.AllowAnyHeader()
					.AllowAnyMethod()
					.WithExposedHeaders("X-Pagination"));
			});
		}

		public static void ConfigureMsSqlContext(this IServiceCollection services, IConfiguration config)
		{
			string connectionString = config.GetSection("ConnectionStrings")["sqlConnection"];

			services.AddDbContext<CodeCompetitionContext>(opts => opts.UseSqlServer(connectionString));
		}

		public static void ConfigureRepositoryWrapper(this IServiceCollection services)
		{
			services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
		}

		public static void ConfigureIISIntegration(this IServiceCollection services)
		{
			services.Configure<IISOptions>(options =>
			{

			});
		}

		public static void ConfigureLogging (this IServiceCollection services)
		{
			services.AddSingleton<ILoggerManager, LoggerManager>();
		}
	}
}
