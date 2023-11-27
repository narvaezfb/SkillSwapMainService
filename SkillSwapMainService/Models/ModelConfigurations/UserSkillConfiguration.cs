using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkillSwapMainService.Models;

public class UserSkillConfiguration : IEntityTypeConfiguration<UserSkill>
{
    public void Configure(EntityTypeBuilder<UserSkill> builder)
    {
        builder.HasKey(us => us.UserSkillID);
        builder.Property(us => us.UserID).IsRequired();
        builder.Property(us => us.SkillID).IsRequired();
        builder.Property(us => us.DateAdded);

        //set time stamp data type 
        builder.Property(us => us.DateAdded)
            .HasColumnType("timestamp with time zone")
            .HasDefaultValueSql("CURRENT_TIMESTAMP");


        // Define the relationship with Skill entity
        builder.HasOne(us => us.Skill)
            .WithMany(s => s.UserSkills)
            .HasForeignKey(us => us.SkillID)
            .IsRequired();
    }
}

