using Microsoft.EntityFrameworkCore;

namespace FileManagerAPI.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<FileEntity> Files { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
    }
}
