namespace BlogBackend.Infrastructure.Blog.Handlers;

using MediatR;
using BlogBackend.Core.Blog.Models;
using BlogBackend.Core.Blog.Repositories.Base;
using BlogBackend.Infrastructure.Blog.Queries;

public class GetAllBlogsByTopicIdHandler : IRequestHandler<GetAllBlogsByTopicIdQuery, IEnumerable<Blog?>>
{
    private readonly IBlogRepository repository;
    public GetAllBlogsByTopicIdHandler(IBlogRepository repository)
    {
        this.repository = repository;
    }

    public async Task<IEnumerable<Blog?>> Handle(GetAllBlogsByTopicIdQuery request, CancellationToken cancellationToken)
    {
        if(request.TopicId is null || request.TopicId <= 0) {
            throw new ArgumentException("topicId is incorrect");
        }

        return await repository.GetAllByTopicIdAsync(request.TopicId.Value);
    }
}
