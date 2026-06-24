using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using bach_bash.Models;

namespace bach_bash.Persistence.Configurations;

public class BasherConfiguration : IEntityTypeConfiguration<Basher>
{
    public void Configure(EntityTypeBuilder<Basher> builder)
    {
        //table name
        builder.ToTable("Bashers");

        //primary key
        builder.HasKey(b => b.Id);

        //properties
        builder.Property(b => b.Username)
            .IsRequired()
            .HasMaxLength(50);

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