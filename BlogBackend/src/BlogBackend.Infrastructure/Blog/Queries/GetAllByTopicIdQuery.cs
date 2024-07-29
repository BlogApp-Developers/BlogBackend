namespace BlogBackend.Infrastructure.Blog.Queries;

using MediatR;
using BlogBackend.Core.Blog.Models;

public class GetAllByTopicIdQuery : IRequest<IEnumerable<Blog?>>
{
    public int? TopicId { get; set; }
}
