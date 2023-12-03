using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkillSwapMainService.Models;

public class TransactionConfiguration: IEntityTypeConfiguration<Transaction>
{
	
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.HasKey(t => t.TransactionID);
        builder.Property(t => t.OffererUserID).IsRequired();
        builder.Property(t => t.ReceiverUserID).IsRequired();
        builder.Property(t => t.OfferedSkillID).IsRequired();
        builder.Property(t => t.ReceivedSkillID).IsRequired();
        builder.Property(t => t.TransactionDate).IsRequired();
        builder.Property(t => t.Duration).IsRequired();
        builder.Property(t => t.isComplete).IsRequired();

        builder.Property(t => t.TransactionDate).HasColumnType("timestamp with time zone");
        builder.Property(t => t.TransactionDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

        //Foreign key definitions
        builder.HasOne(t => t.OfferedSkill)
            .WithMany(s => s.OfferedSkill)
            .HasForeignKey(t => t.OfferedSkillID)
            .IsRequired().
            OnDelete(DeleteBehavior.Restrict);


        builder.HasOne(t => t.ReceivedSkill)
            .WithMany(s => s.ReceivedSkill)
            .HasForeignKey(t => t.ReceivedSkillID)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
    }
}


