using DEBUG.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DEBUG.DAL.Configurations;

public class ReportItemConfiguration : IEntityTypeConfiguration<ReportItem>
{
    public void Configure(EntityTypeBuilder<ReportItem> builder)
    {
        builder.HasIndex(x => x.Id);
        builder.Property(x => x.ReporterUserId)
            .IsRequired();
        builder.Property(x => x.ReportedUserId)
            .IsRequired();
        builder.Property(x => x.QuestionId)
            .IsRequired();
        builder.Property(x => x.AnswerId)
            .IsRequired();
        builder.Property(x => x.CommentId)
            .IsRequired();
    }
}
