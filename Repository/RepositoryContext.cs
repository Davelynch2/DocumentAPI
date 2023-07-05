using DocumentAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class RepositoryContext : DbContext
    {
        public DbSet<FileModel>? Files { get; set; }
    }
}