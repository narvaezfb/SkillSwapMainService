using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkillSwapMainService.Models;

public class UserSkillConfiguration : IEntityTypeConfiguration<UserSkill>
{
    public void Configure(EntityTypeBuilder<UserSkill> builder)
    {
        builder.HasKey(u => u.UserSkillID);
        builder.Property(u => u.UserID).IsRequired();
        builder.Property(u => u.SkillID).IsRequired();
        builder.Property(u => u.DateAdded);

        //set time stamp data type 
        builder.Property(u => u.DateAdded)
            .HasColumnType("timestamp with time zone")
            .HasDefaultValueSql("CURRENT_TIMESTAMP");


        // Define the relationship with Skill entity
        builder.HasOne(us => us.Skill)
            .WithMany(s => s.UserSkills)
            .HasForeignKey(us => us.SkillID);
    }
}

