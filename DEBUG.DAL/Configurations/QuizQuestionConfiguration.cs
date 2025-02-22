using DEBUG.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DEBUG.DAL.Configurations;

public class QuizQuestionConfiguration : IEntityTypeConfiguration<QuizQuestion>
{
    public void Configure(EntityTypeBuilder<QuizQuestion> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Content)
            .HasMaxLength(1024)
            .IsRequired();
        builder.HasOne(x => x.Tag)
            .WithMany(x => x.QuizQuestions)
            .HasForeignKey(x => x.TagId);
    }
}