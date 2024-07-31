namespace BlogBackend.Infrastructure.Blog.Handlers;

using MediatR;
using BlogBackend.Core.Blog.Models;
using BlogBackend.Core.Blog.Repositories.Base;
using BlogBackend.Infrastructure.Blog.Queries;

public class GetBlogByIdHandler : IRequestHandler<GetBlogByIdQuery, Blog?>
{
    private readonly IBlogRepository repository;
    public GetBlogByIdHandler(IBlogRepository repository)
    {
        this.repository = repository;
    }

    public async Task<Blog?> Handle(GetBlogByIdQuery request, CancellationToken cancellationToken)
    {
        if(request.Id is null || request.Id <= 0) {
            throw new ArgumentException("id is incorrect");
        }

        return await repository.GetByIdAsync(request.Id.Value);
    }
}