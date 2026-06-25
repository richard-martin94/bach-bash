using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using bach_bash.Models;

namespace bach_bash.Persistence.Configurations;

public class BashMemberConfiguration : IEntityTypeConfiguration<BashMember>
{
    public void Configure(EntityTypeBuilder<BashMember> builder)
    {
        //table name
        builder.ToTable("BashMembers");

        //primary key
        builder.HasKey(b => b.Id);
        
        //properties
        //foreign key
        builder.HasOne(b => b.Bash)
            .WithMany(m => m.BashMmebers)
            .HasForeignKey(b => b.BashId)
            .IsRequired();
        //foreign key
        builder.HasOne(b => b.Bash)
            .WithMany(m => m.BashMmebers)
            .HasForeignKey(m => m.BasherId)
            .IsRequired();

        //immutable properties
        builder.Property(b => b.Created)
            .IsRequired()
            .ValueGeneratedOnAdd();
        builder.Property(b => b.LastModified)
            .IsRequired()
            .ValueGeneratedOnUpdate();

        //query performance boost
        builder.HasIndex(b => b.BashId);
    }
}