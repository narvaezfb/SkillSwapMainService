using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkillSwapMainService.Models;

public class SkillConfiguration: IEntityTypeConfiguration<Skill>
{
    public void Configure(EntityTypeBuilder<Skill> builder)
    {
        builder.HasKey(s => s.SkillID);
        builder.Property(s => s.Name).IsRequired().HasMaxLength(255);
        builder.Property(s => s.Description).IsRequired();
        builder.Property(s => s.CategoryID).IsRequired().HasMaxLength(255);
        builder.Property(s => s.Level).IsRequired().HasMaxLength(255);
        builder.Property(s => s.OwnerID).IsRequired();
        builder.Property(s => s.DateAdded);
        builder.Property(s => s.LastModified);
        builder.Property(s => s.IsVerified);

        builder.Property(s => s.SkillID)
           .ValueGeneratedOnAdd();

        builder.Property(s => s.DateAdded).HasColumnType("timestamp with time zone");
        builder.Property(s => s.LastModified).HasColumnType("timestamp with time zone");

        builder.Property(s => s.DateAdded).HasDefaultValueSql("CURRENT_TIMESTAMP");
        builder.Property(s => s.LastModified).HasDefaultValueSql("CURRENT_TIMESTAMP");

        //Foreign key definition
        builder.HasOne(s => s.Category)
            .WithMany(c => c.Skills)
            .HasForeignKey(s => s.CategoryID)
            .IsRequired().OnDelete(DeleteBehavior.Restrict);

    }
}


