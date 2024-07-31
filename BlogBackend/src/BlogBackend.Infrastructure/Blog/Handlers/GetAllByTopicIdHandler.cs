namespace BlogBackend.Infrastructure.Blog.Handlers;

using MediatR;
using BlogBackend.Core.Blog.Models;
using BlogBackend.Core.Blog.Repositories.Base;
using BlogBackend.Infrastructure.Blog.Queries;

public class GetAllByTopicIdHandler : IRequestHandler<GetAllByTopicIdQuery, IEnumerable<Blog?>>
{
    private readonly IBlogRepository repository;
    public GetAllByTopicIdHandler(IBlogRepository repository)
    {
        this.repository = repository;
    }

    public async Task<IEnumerable<Blog?>> Handle(GetAllByTopicIdQuery request, CancellationToken cancellationToken)
    {
        if(request.TopicId is null || request.TopicId <= 0) {
            throw new ArgumentException("topicId is incorrect");
        }

        return await repository.GetAllByTopicIdAsync(request.TopicId.Value);
    }
}