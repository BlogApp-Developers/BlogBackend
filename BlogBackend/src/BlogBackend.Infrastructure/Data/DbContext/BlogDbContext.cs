namespace BlogBackend.Infrastructure.Data.DbContext;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using BlogBackend.Core.Role.Models;
using BlogBackend.Core.User.Models;
using BlogBackend.Core.Blog.Models;
using BlogBackend.Core.Topic.Models;
using BlogBackend.Core.UserTopic.Models;
using BlogBackend.Core.RefreshToken.Entity;

using BlogBackend.Core.User.Data.Configurations;
using BlogBackend.Core.Blog.Data.Configurations;
using BlogBackend.Core.RefreshToken.Data.Configurations;
using BlogBackend.Core.Topic.Data.Configurations;
using BlogBackend.Core.UserTopic.Data.Configurations;
using Microsoft.Extensions.Configuration;

public class BlogDbContext : IdentityDbContext<User, Role, int>
{
    public DbSet<Topic> Topics { get; set; }
    public DbSet<UserTopic> UserTopics { get; set; }
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public BlogDbContext(DbContextOptions options) : base(options)
    {
        
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new BlogConfiguration());
        modelBuilder.ApplyConfiguration(new UserTopicConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new RefreshTokenConfiguration());
        
        base.OnModelCreating(modelBuilder);
    }
}