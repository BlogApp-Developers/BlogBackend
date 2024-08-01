namespace BlogBackend.Presentation.Extensions.ServiceCollectionExtensions;

public static class InitCorsMethod
{
    public static void InitCors(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddCors(options =>
        {
            options.AddPolicy("BlogApp", policyBuilder =>
            {
                policyBuilder
                    .WithOrigins("http://localhost:5058")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });
    }
}
