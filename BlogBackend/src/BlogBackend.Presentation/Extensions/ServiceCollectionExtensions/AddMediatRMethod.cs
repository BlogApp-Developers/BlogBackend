namespace BlogBackend.Presentation.Extensions.ServiceCollectionExtensions;

using BlogBackend.Infrastructure.Data.DbContext;

public static class AddMediatRMethod
{
    public static void AddMediatR(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddMediatR(configuration => {
            Type typeInReferencedAssembly = typeof(BlogDbContext);
            configuration.RegisterServicesFromAssembly( typeInReferencedAssembly.Assembly );
        });
    } 
}
