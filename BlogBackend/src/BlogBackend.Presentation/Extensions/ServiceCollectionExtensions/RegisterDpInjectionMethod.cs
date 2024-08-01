namespace BlogBackend.Presentation.Extensions.ServiceCollectionExtensions;

using BlogBackend.Core.Blog.Repositories.Base;
using BlogBackend.Core.Topic.Repositories.Base;
using BlogBackend.Core.UserTopic.Repositories.Base;
using BlogBackend.Infrastructure.Blog.Repositories.Ef_Core;
using BlogBackend.Infrastructure.Topic.Repositories.Ef_Core;
using BlogBackend.Infrastructure.UserTopic.Repositories.Ef_Core;
using BlogBackend.Presentation.Verification;
using BlogBackend.Presentation.Verification.Base;

public static class RegisterDpInjectionMethod
{
    public static void RegisterDpInjection(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IEmailService, EmailService>();

        serviceCollection.AddScoped<IUserTopicRepository, UserTopicEfCoreRepository>();
        serviceCollection.AddScoped<ITopicRepository, TopicEfCoreRepository>();
        serviceCollection.AddScoped<IBlogRepository, BlogEfCoreRepository>();
    } 
}
