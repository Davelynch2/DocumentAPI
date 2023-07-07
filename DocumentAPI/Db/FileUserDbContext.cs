using DocumentAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DocumentAPI.Db
{
	public class FileUserDbContext : DbContext
	{
		public FileUserDbContext(DbContextOptions<FileUserDbContext> options)
			: base(options)
		{
		}

		public DbSet<FileModel>? Files {get; set;}
		public DbSet<Event> Events { get; set; }
		public DbSet<User> Users { get; set; }
	}
}
