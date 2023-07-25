using AutoMapper;
using DocumentAPI.Db;
using Microsoft.EntityFrameworkCore;

namespace DocumentAPI.Extensions
{
	public static class ServiceExtensions
	{
		public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) =>
		   services.AddDbContext<FileUserDbContext>(opts => opts.UseNpgsql(configuration.GetConnectionString("sqlConnection")));

		public static void ConfigureServices(this IServiceCollection services)
		{
			var mappingConfig = new MapperConfiguration(mc =>
			{
				mc.AddProfile(new MappingProfile());
			});

			IMapper mapper = mappingConfig.CreateMapper();
			services.AddSingleton(mapper);
		}
	}
}
