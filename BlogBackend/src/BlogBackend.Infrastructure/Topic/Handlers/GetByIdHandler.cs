namespace BlogBackend.Infrastructure.Topic.Handlers;

using MediatR;
using BlogBackend.Core.Topic.Models;
using BlogBackend.Core.Topic.Repositories.Base;
using BlogBackend.Infrastructure.Topic.Queries;

public class GetByIdHandler : IRequestHandler<GetTopicByIdQuery, Topic?>
{
    private readonly ITopicRepository repository;
    public GetByIdHandler(ITopicRepository repository)
    {
        this.repository = repository;
    }

    public async Task<Topic?> Handle(GetTopicByIdQuery request, CancellationToken cancellationToken)
    {
        if(request.Id is null || request.Id <= 0) {
            throw new ArgumentException("id is incorrect");
        }

        return await repository.GetByIdAsync(request.Id.Value);
    }
}