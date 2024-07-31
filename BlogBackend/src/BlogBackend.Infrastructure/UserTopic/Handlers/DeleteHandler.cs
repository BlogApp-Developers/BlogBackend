namespace BlogBackend.Infrastructure.UserTopic.Handlers;

using MediatR;
using BlogBackend.Infrastructure.UserTopic.Commands;
using System.Threading.Tasks;
using System.Threading;
using BlogBackend.Core.UserTopic.Repositories.Base;
using BlogBackend.Core.UserTopic.Models;

public class DeleteHandler : IRequestHandler<DeleteUserTopicCommand>
{
    private readonly IUserTopicRepository repository;
    public DeleteHandler(IUserTopicRepository repository)
    {
        this.repository = repository;
    }
    public async Task Handle(DeleteUserTopicCommand request, CancellationToken cancellationToken)
    {
        if(request.Id <= 0) {
            throw new ArgumentException("id is incorrect");
        }

        var userTopic = new UserTopic()
        {
            Id = request.Id,
        };

        await repository.DeleteAsync(userTopic);
    }
}
