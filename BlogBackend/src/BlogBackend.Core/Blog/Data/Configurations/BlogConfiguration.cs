namespace BlogBackend.Core.Blog.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BlogBackend.Core.Blog.Models;

public class BlogConfiguration : IEntityTypeConfiguration<Blog>
{
    public void Configure(EntityTypeBuilder<Blog> builder)
    {
        builder
            .HasKey(b => b.Id);

        builder
            .HasOne(b => b.User)
            .WithMany()
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder
            .HasOne(b => b.Topic)
            .WithMany()
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder
            .HasIndex(b => b.Title)
            .IsUnique();

        builder
            .Property(b => b.Title)
            .IsRequired();

        builder
            .Property(b => b.Text)
            .IsRequired();


        builder
            .Property(b => b.CreationDate)
            .IsRequired();
    }
}
