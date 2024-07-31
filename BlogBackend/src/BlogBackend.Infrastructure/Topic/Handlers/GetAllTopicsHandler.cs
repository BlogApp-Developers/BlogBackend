namespace BlogBackend.Infrastructure.Topic.Handlers;

using MediatR;
using BlogBackend.Core.Topic.Models;
using BlogBackend.Core.Topic.Repositories.Base;
using BlogBackend.Infrastructure.Topic.Queries;

public class GetAllTopicsHandler : IRequestHandler<GetAllTopicsQuery, IEnumerable<Topic?>>
{
    private readonly ITopicRepository repository;
    public GetAllTopicsHandler(ITopicRepository repository)
    {
        this.repository = repository;
    }

    public async Task<IEnumerable<Topic?>> Handle(GetAllTopicsQuery request, CancellationToken cancellationToken)
    {
        return await repository.GetAllAsync();
    }
}