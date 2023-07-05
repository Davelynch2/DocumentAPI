using DocumentAPI.Db;
using Microsoft.EntityFrameworkCore;

namespace DocumentAPI.Extensions
{
	public static class ServiceExtensions
	{
		public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) =>
		   services.AddDbContext<FileUserDbContext>(opts => opts.UseSqlServer(configuration.GetConnectionString("sqlConnection")));
	}
}
