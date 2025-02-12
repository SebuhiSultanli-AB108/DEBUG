using DEBUG.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DEBUG.DAL.Configurations;

public class QuestionConfiguration : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        builder.HasIndex(x => x.Id);
        builder.Property(x => x.Title)
            .HasMaxLength(128)
            .IsRequired();
        builder.Property(x => x.Content)
            .HasMaxLength(4096)
            .IsRequired();
        builder.HasOne(x => x.User)
            .WithMany(x => x.Questions)
            .HasForeignKey(x => x.UserId);
        builder.HasOne(x => x.Category)
            .WithMany(x => x.Questions)
            .HasForeignKey(x => x.CategoryId)
            .OnDelete(DeleteBehavior.NoAction);
        builder.HasMany(x => x.Tags)
            .WithMany(x => x.Questions)
            .UsingEntity("QuestionTag");
    }
}