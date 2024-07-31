namespace BlogBackend.Infrastructure.Blog.Queries;

using MediatR;
using BlogBackend.Core.Blog.Models;

public class GetAllBlogsByUserIdQuery : IRequest<IEnumerable<Blog?>>
{
    public int? UserId { get; set; }
}