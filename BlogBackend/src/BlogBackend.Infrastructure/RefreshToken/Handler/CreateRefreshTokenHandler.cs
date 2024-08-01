namespace BlogBackend.Infrastructure.RefreshToken.Handler;

using System.Threading;
using System.Threading.Tasks;
using BlogBackend.Core.RefreshToken.Entity;
using BlogBackend.Core.RefreshToken.Repositories;
using BlogBackend.Infrastructure.RefreshToken.Command;
using MediatR;

public class CreateRefreshTokenHandler : IRequestHandler<CreateRefreshTokenCommand>
{
    private readonly IRefreshTokenRepository repository;
    public CreateRefreshTokenHandler(IRefreshTokenRepository repository)
    {
        this.repository = repository;
    }

    public async Task Handle(CreateRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        if(request.UserId <= 0)
        {
            throw new ArgumentException("userId is not valid for token");
        }
        
        var refreshToken = new RefreshToken(){
            Token = request.Token,
            UserId = request.UserId
        };
        await repository.CreateAsync(refreshToken);
    }
}
