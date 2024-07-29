namespace BlogBackend.Infrastructure.UserTopic.Handlers;

using MediatR;
using BlogBackend.Infrastructure.UserTopic.Commands;
using System.Threading.Tasks;
using System.Threading;
using BlogBackend.Core.UserTopic.Repositories.Base;
using BlogBackend.Core.UserTopic.Models;

public class CreateHandler : IRequestHandler<CreateCommand>
{
    private readonly IUserTopicRepository repository;
    public CreateHandler(IUserTopicRepository repository)
    {
        this.repository = repository;
    }
    public async Task Handle(CreateCommand request, CancellationToken cancellationToken)
    {
        if(request.TopicId <= 0) {
            throw new ArgumentException("topicId is incorrect");
        }
        else if(request.UserId <= 0) {
            throw new ArgumentException("userId is incorrect");
        }

        var userTopic = new UserTopic {
            TopicId = request.TopicId,
            UserId = request.UserId,
        };

        await repository.CreateAsync(userTopic);
    }
}
