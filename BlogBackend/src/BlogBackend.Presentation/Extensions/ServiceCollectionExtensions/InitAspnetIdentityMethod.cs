namespace BlogBackend.Presentation.Extensions.ServiceCollectionExtensions;

using BlogBackend.Core.Role.Models;
using BlogBackend.Core.User.Models;
using BlogBackend.Infrastructure.Data.DbContext;
using Microsoft.EntityFrameworkCore;

public static class InitAspnetIdentityMethod
{
    public static void InitAspnetIdentity(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddDbContext<BlogDbContext>(options =>
        {
            var connectinoString = configuration.GetConnectionString("PostgreSqlDev");
            options.UseNpgsql(connectinoString);
        });

        serviceCollection.AddIdentity<User, Role>( (options) => {
            options.User.RequireUniqueEmail = true;
        })
            .AddEntityFrameworkStores<BlogDbContext>();
    }
}
