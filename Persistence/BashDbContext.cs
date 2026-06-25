using Microsoft.EntityFrameworkCore;
using bach_bash.Models;

namespace bach_bash.Persistence;

public class BashDbContext(DbContextOptions<BashDbContext> options) : DbContext(options)
{
    public DbSet<Bash> Bashes => Set<Bash>();
    public DbSet<Basher> Bashers => Set<Basher>();
    public DbSet<Challenge> Challenges => Set<Challenge>();
    public DbSet<Submission> Submissions => Set<Submission>();
    public DbSet<BashMember> BashMembers => Set<BashMember>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("app");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BashDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var devOwnerGuid = Guid.CreateVersion7();
        
        optionsBuilder
            .UseAsyncSeeding(async (context, _, cancellation) =>
            {
                var sampleBash = await context.Set<Bash>()
                    .FirstOrDefaultAsync(b => b.Title == "Bash 0.0 for testing purposes",
                        cancellationToken: cancellation);
                if (sampleBash == null)
                {
                    sampleBash = Bash.CreateBash("Bash 0.0 for testing purposes", devOwnerGuid);
                    await context.Set<Bash>().AddAsync(sampleBash, cancellation);
                    await context.SaveChangesAsync(cancellation);
                }
            }).UseSeeding((context, _) =>
            {
                var sampleBash = context.Set<Bash>().FirstOrDefault(b => b.OwnerId == devOwnerGuid);
                if (sampleBash == null)
                {
                    sampleBash = Bash.CreateBash("Bash 0.0 for testing purposes", devOwnerGuid);
                    context.Set<Bash>().Add(sampleBash);
                    context.SaveChanges();
                }
            });
    }
}