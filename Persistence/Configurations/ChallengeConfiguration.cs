
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using bach_bash.Models;namespace bach_bash.Persistence.Configurations;

public class ChallengeConfiguration : IEntityTypeConfiguration<Challenge>
{
    public void Configure(EntityTypeBuilder<Challenge> builder)
    {
        
            //table name
            builder.ToTable("Challenges");
        
            //primary key
            builder.HasKey(c => c.Id);
        
            //properties
            builder.Property(c => c.Title)
                .IsRequired()
                .HasMaxLength(200);
            builder.Property(c => c.Description)
                .IsRequired()
                .HasMaxLength(500);
            builder.Property(c => c.Points)
                .IsRequired();
            builder.Property(c => c.BashId)
                .IsRequired();
        
            //immutable properties
            builder.Property(b => b.Created)
                .IsRequired()
                .ValueGeneratedOnAdd();
            builder.Property(b => b.LastModified)
                .IsRequired()
                .ValueGeneratedOnUpdate();
        
            //query performance boost
            builder.HasIndex(c => c.BashId);

        
    }
}