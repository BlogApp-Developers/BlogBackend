namespace BlogBackend.Infrastructure.UserTopic.Commands;

using MediatR;

public class CreateListCommand : IRequest
{
    public IEnumerable<int>? TopicsIds { get; set; }
    public int UserId { get; set; }
}