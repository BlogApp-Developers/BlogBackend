namespace BlogBackend.Infrastructure.Topic.Handlers;

using MediatR;
using BlogBackend.Core.Topic.Models;
using BlogBackend.Core.Topic.Repositories.Base;
using BlogBackend.Infrastructure.Topic.Queries;

public class GetAllHandler : IRequestHandler<GetAllQuery, IEnumerable<Topic?>>
{
    private readonly ITopicRepository repository;
    public GetAllHandler(ITopicRepository repository)
    {
        this.repository = repository;
    }

    public async Task<IEnumerable<Topic?>> Handle(GetAllQuery request, CancellationToken cancellationToken)
    {
        return await repository.GetAllAsync();
    }
}