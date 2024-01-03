using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.DB;

public class WAContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public WAContext(DbContextOptions<WAContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
}