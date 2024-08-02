namespace BlogBackend.Infrastructure.RefreshToken.Handler;

using BlogBackend.Core.RefreshToken.Repositories;
using BlogBackend.Core.RefreshToken.Entity;
using BlogBackend.Infrastructure.RefreshToken.Command;
using MediatR;

public class DeleteRangeRefreshTokenHandler: IRequestHandler<DeleteRangeRefreshTokenCommand>
{
    private readonly IRefreshTokenRepository repository;
    public DeleteRangeRefreshTokenHandler(IRefreshTokenRepository repository)
    {
        this.repository = repository;
    }

    public async Task Handle(DeleteRangeRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        if(request.UserId <= 0)
        {
            throw new ArgumentException("userId is not valid for token");
        }
        
        var refreshToken = new RefreshToken(){
            UserId = request.UserId
        };
        await repository.DeleteRangeRefreshTokensAsync(refreshToken);
    }
}
