using SkillSwapMainService.Models;
using Microsoft.EntityFrameworkCore;

public class SkillSwapDbContext: DbContext
{
	public SkillSwapDbContext(DbContextOptions<SkillSwapDbContext> options): base(options)
	{
	}

	public DbSet<Skill> Skills { get; set; }
    public DbSet<Listing> Listings { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Apply entity configurations from separate configuration classes
        modelBuilder.ApplyConfiguration(new SkillConfiguration());
        modelBuilder.ApplyConfiguration(new ListingConfiguration());
        modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        modelBuilder.ApplyConfiguration(new TransactionConfiguration());
        // Other configurations
    }

}


