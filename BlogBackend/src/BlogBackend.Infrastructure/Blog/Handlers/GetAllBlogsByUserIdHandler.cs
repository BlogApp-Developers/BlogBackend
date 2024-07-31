namespace BlogBackend.Infrastructure.Blog.Handlers;

using MediatR;
using BlogBackend.Core.Blog.Models;
using BlogBackend.Core.Blog.Repositories.Base;
using BlogBackend.Infrastructure.Blog.Queries;

public class GetAllBlogsByUserIdHandler : IRequestHandler<GetAllBlogsByUserIdQuery, IEnumerable<Blog?>>
{
    private readonly IBlogRepository repository;
    public GetAllBlogsByUserIdHandler(IBlogRepository repository)
    {
        this.repository = repository;
    }

    public async Task<IEnumerable<Blog?>> Handle(GetAllBlogsByUserIdQuery request, CancellationToken cancellationToken)
    {
        if(request.UserId is null || request.UserId <= 0) {
            throw new ArgumentException("userId is incorrect");
        }

        return await repository.GetAllByUserIdAsync(request.UserId.Value);
    }
}
