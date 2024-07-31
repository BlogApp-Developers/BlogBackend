#pragma warning disable CS8602

namespace BlogBackend.Infrastructure.UserTopic.Handlers;

using MediatR;
using BlogBackend.Infrastructure.UserTopic.Commands;
using System.Threading.Tasks;
using System.Threading;
using BlogBackend.Core.UserTopic.Repositories.Base;
using BlogBackend.Core.UserTopic.Models;

public class CreateUserTopicsListHandler : IRequestHandler<CreateUserTopicsListCommand>
{
    private readonly IUserTopicRepository repository;
    public CreateUserTopicsListHandler(IUserTopicRepository repository)
    {
        this.repository = repository;
    }

    public async Task Handle(CreateUserTopicsListCommand request, CancellationToken cancellationToken)
    {
        if(request.UserId <= 0) {
            throw new ArgumentException("userId is incorrect");
        }

        var areValid = request?.TopicsIds?.Any(topic => topic is 0 || topic <= 0) ?? false;

        if(areValid)
        {
            throw new ArgumentException("topics are incorrect");
        }

        var userTopics = request?.TopicsIds?.Select(topic => new UserTopic()
        {
            UserId = request.UserId,
            TopicId = topic
        });

        await repository.CreateListAsync(userTopics!);
    }
}
