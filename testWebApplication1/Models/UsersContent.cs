using Microsoft.EntityFrameworkCore;

namespace testWebApplication1.Models
{
    public class UsersContext : DbContext
    {
        public DbSet<User?> Users { get; set; }
        public UsersContext(DbContextOptions<UsersContext> options)
            : base(options)
        { 
            Database.EnsureCreated();
        }
    }
}