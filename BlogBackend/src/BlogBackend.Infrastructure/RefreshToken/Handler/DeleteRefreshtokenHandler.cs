namespace BlogBackend.Infrastructure.RefreshToken.Handler;

using BlogBackend.Core.RefreshToken.Repositories;
using BlogBackend.Core.RefreshToken.Entity;
using BlogBackend.Infrastructure.RefreshToken.Command;
using MediatR;

public class DeleteRefreshtokenHandler : IRequestHandler<DeleteRefreshTokenCommand>
{
    private readonly IRefreshTokenRepository repository;
    public DeleteRefreshtokenHandler(IRefreshTokenRepository repository)
    {
        this.repository = repository;
    }

    public async Task Handle(DeleteRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        if(request.UserId <= 0)
        {
            throw new ArgumentException("userId is not valid for token");
        }
        
        var refreshToken = new RefreshToken(){
            Token = request.Token,
            UserId = request.UserId
        };
        await repository.DeleteAsync(refreshToken);
    }
}
