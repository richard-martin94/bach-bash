using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using bach_bash.Models;

namespace bach_bash.Persistence.Configurations;

public class SubmissionConfiguration : IEntityTypeConfiguration<Submission>
{
    public void Configure(EntityTypeBuilder<Submission> builder)
    {
        //table name
        builder.ToTable("Submissions");

        //primary key
        builder.HasKey(b => b.Id);

        //properties
        //foreign key
        builder.HasOne(c => c.Challenge)
            .WithMany(s => s.Submissions)
            .HasForeignKey(c => c.ChallengeId)
            .IsRequired();
        //foreign key
        builder.HasOne(c => c.Basher)
            .WithMany(s => s.Submissions)
            .HasForeignKey(c => c.BasherId)
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