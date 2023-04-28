using Microsoft.EntityFrameworkCore;
using AsparagusApp.Models;

namespace AsparagusApp
{
    public class ApplicationContext : DbContext
    {
        public DbSet<AsparagusLog> AsparagusLogs { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
