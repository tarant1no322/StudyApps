using Microsoft.EntityFrameworkCore;
using WebApiEmployees.Domain.Models; 

namespace WebApiEmployees.Service
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}