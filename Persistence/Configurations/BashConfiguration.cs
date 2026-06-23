using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using bach_bash.Models;

namespace bach_bash.Persistence.Configurations;

public class BashConfiguration : IEntityTypeConfiguration<Bash>
{
    public void Configure(EntityTypeBuilder<Bash> builder)
    {
        //table name
        builder.ToTable("Bashes");
        
        //primary key
        builder.HasKey(b => b.Id);
        
        //properties
        builder.Property(b => b.Title)
            .IsRequired()
            .HasMaxLength(200);
        builder.Property(b => b.OwnerId)
            .IsRequired();
        
        //immutable properties
        builder.Property(b => b.Created)
            .IsRequired()
            .ValueGeneratedOnAdd();
        builder.Property(b => b.LastModified)
            .IsRequired()
            .ValueGeneratedOnUpdate();
        
        //query performance boost
        builder.HasIndex(b => b.Id);


    }
}