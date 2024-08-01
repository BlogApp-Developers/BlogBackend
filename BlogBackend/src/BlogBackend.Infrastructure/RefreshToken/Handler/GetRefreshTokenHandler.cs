namespace BlogBackend.Infrastructure.RefreshToken.Handler;

using BlogBackend.Core.RefreshToken.Repositories;
using BlogBackend.Core.RefreshToken.Entity;
using BlogBackend.Infrastructure.RefreshToken.Query;
using MediatR;

public class GetRefreshTokenHandler : IRequestHandler<GetRefreshTokenQuery, RefreshToken?>
{
    private readonly IRefreshTokenRepository repository;
    public GetRefreshTokenHandler(IRefreshTokenRepository repository)
    {
        this.repository = repository;
    }

    public async Task<RefreshToken?> Handle(GetRefreshTokenQuery request, CancellationToken cancellationToken)
    {
        if(request.UserId <= 0)
        {
            throw new ArgumentException("userId is not valid for token");
        }
        
        var refreshToken = new RefreshToken(){
            Token = request.Token,
            UserId = request.UserId
        };
        return await repository.GetRefreshTokenAsync(refreshToken);
    }
}