namespace BlogBackend.Infrastructure.UserTopic.Commands;

using MediatR;

public class CreateUserTopicCommand : IRequest
{
    public int TopicId { get; set; }
    public int UserId { get; set; }
}