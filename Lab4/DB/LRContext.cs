using Lab4.Model;
using Microsoft.EntityFrameworkCore;

namespace Lab4.DB;

public sealed class LrContext : DbContext
{
    public DbSet<WordModel>? WordModels { get; set; }

    public LrContext(DbContextOptions<LrContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
}