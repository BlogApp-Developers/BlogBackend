namespace BlogBackend.Infrastructure.Blog.Queries;

using BlogBackend.Core.Blog.Models;
using MediatR;

public class GetAllBlogsByNameQuery : IRequest<IEnumerable<Blog?>>
{
    public string? Name { get; set; }
}
