namespace BlogBackend.Infrastructure.Blog.Queries;

using BlogBackend.Core.Blog.Models;
using MediatR;

public class GetAllByNameQuery : IRequest<IEnumerable<Blog?>>
{
    public string? Name { get; set; }
}
