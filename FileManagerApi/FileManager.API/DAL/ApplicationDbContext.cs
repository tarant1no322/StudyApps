using FileManagerApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FileManagerAPI.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<FileModel> Files { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
           // Database.EnsureCreated();
        }
    }
}
