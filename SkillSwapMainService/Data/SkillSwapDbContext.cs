﻿using SkillSwapMainService.Models;
using Microsoft.EntityFrameworkCore;

public class SkillSwapDbContext: DbContext
{
	public SkillSwapDbContext(DbContextOptions<SkillSwapDbContext> options): base(options)
	{
	}
	public DbSet<Skill> Skill { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Apply entity configurations from separate configuration classes
        modelBuilder.ApplyConfiguration(new SkillConfiguration());
        // Other configurations
    }
}


