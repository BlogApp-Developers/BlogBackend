namespace BlogBackend.Infrastructure.UserTopic.Queries;

using MediatR;
using BlogBackend.Core.Topic.Models;

public class GetAllTopicsByUserIdQuery : IRequest<IEnumerable<Topic?>>
{
    public int? UserId { get; set; }
}