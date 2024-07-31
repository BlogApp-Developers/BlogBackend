namespace BlogBackend.Infrastructure.Topic.Queries;

using MediatR;
using BlogBackend.Core.Topic.Models;

public class GetTopicByIdQuery : IRequest<Topic?>
{
    public int? Id { get; set; }
}