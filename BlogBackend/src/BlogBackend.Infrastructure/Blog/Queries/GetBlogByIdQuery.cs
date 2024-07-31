namespace BlogBackend.Infrastructure.Blog.Queries;

using MediatR;
using BlogBackend.Core.Blog.Models;

public class GetBlogByIdQuery : IRequest<Blog?>
{
    public int? Id { get; set; }
}

