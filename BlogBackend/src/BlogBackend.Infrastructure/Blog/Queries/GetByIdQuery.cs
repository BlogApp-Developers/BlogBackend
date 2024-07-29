namespace BlogBackend.Infrastructure.Blog.Queries;

using MediatR;
using BlogBackend.Core.Blog.Models;

public class GetByIdQuery : IRequest<Blog?>
{
    public int? Id { get; set; }
}

