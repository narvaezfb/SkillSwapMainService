using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkillSwapMainService.Models;

public class ListingConfiguration : IEntityTypeConfiguration<Listing>
{
    public void Configure(EntityTypeBuilder<Listing> builder)
    {
        builder.HasKey(l => l.ListingID);
        builder.Property(l => l.UserID).IsRequired();
        builder.Property(l => l.SkillID).IsRequired();
        builder.Property(l => l.Description).IsRequired();
        builder.Property(l => l.DateAdded).IsRequired();
        builder.Property(l => l.Location).IsRequired();
        builder.Property(l => l.Availability).IsRequired();

        builder.Property(us => us.DateAdded)
            .HasColumnType("timestamp with time zone")
            .HasDefaultValueSql("CURRENT_TIMESTAMP");


        // Foreign key definition
        builder.HasOne(l => l.Skill)
            .WithMany(s => s.Listings)
            .HasForeignKey(l => l.SkillID)
            .IsRequired();
    }
}

