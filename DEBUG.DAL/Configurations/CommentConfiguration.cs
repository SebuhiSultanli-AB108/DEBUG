using DEBUG.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DEBUG.DAL.Configurations;

public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.HasIndex(x => x.Id);
        builder.Property(x => x.CommentText)
            .HasMaxLength(256)
            .IsRequired();
        builder.HasOne(x => x.Answer)
            .WithMany(x => x.Comments)
            .HasForeignKey(x => x.AnswerId);
        builder.HasOne(x => x.User)
            .WithMany(x => x.Comments)
            .HasForeignKey(x => x.UserId);
    }
}