namespace BlogBackend.Infrastructure.UserTopic.Commands;

using MediatR;

public class DeleteUserTopicCommand : IRequest
{
    public int Id { get; set; }
}