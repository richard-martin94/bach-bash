using Microsoft.EntityFrameworkCore;
using bach_bash.Models;

namespace bach_bash.Persistence;

public class BashDbContext(DbContextOptions<BashDbContext> options) : DbContext(options)
{
    public DbSet<Bash> Bashes => Set<Bash>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("app");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BashDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}