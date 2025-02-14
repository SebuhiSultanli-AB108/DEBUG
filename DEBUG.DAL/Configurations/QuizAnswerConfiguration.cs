using DEBUG.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DEBUG.DAL.Configurations;

public class QuizAnswerConfiguration : IEntityTypeConfiguration<QuizAnswer>
{
    public void Configure(EntityTypeBuilder<QuizAnswer> builder)
    {
        builder.HasIndex(x => x.Id);
        builder.Property(x => x.Content)
            .HasMaxLength(128)
            .IsRequired();
        builder.HasOne(x => x.QuizQuestion)
            .WithMany(x => x.QuizAnswers)
            .HasForeignKey(x => x.QuizQuestionId);
    }
}