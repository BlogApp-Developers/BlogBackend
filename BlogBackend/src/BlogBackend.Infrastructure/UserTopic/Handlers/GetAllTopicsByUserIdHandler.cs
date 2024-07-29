namespace BlogBackend.Infrastructure.UserTopic.Handlers;

using MediatR;
using BlogBackend.Core.Topic.Models;
using BlogBackend.Core.UserTopic.Repositories.Base;
using BlogBackend.Infrastructure.UserTopic.Queries;

public class GetAllTopicsByUserIdHandler : IRequestHandler<GetAllTopicsByUserIdQuery, IEnumerable<Topic?>>
{
    private readonly IUserTopicRepository repository;
    public GetAllTopicsByUserIdHandler(IUserTopicRepository repository)
    {
        this.repository = repository;
    }

    public async Task<IEnumerable<Topic?>> Handle(GetAllTopicsByUserIdQuery request, CancellationToken cancellationToken)
    {
        if(request.UserId is null || request.UserId <= 0) {
            throw new ArgumentException("userId is incorrect");
        }

        return await repository.GetAllTopicsByUserIdAsync(request.UserId.Value);
    }
}
