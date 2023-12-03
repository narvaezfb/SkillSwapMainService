using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkillSwapMainService.Models;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(c => c.CategoryID);
        builder.Property(c => c.Name).IsRequired();
        builder.Property(c => c.Description).IsRequired();
        builder.Property(c => c.DateCreated).IsRequired();
        builder.Property(c => c.LastModified);

        builder.Property(c => c.DateCreated)
            .HasColumnType("timestamp with time zone")
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(c => c.DateCreated).HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(c => c.LastModified)
            .HasColumnType("timestamp with time zone")
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(c => c.LastModified).HasDefaultValueSql("CURRENT_TIMESTAMP");
    }
}

