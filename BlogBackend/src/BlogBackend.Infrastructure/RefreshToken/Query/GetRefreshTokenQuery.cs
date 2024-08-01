namespace BlogBackend.Infrastructure.RefreshToken.Query;

using MediatR;
using BlogBackend.Core.RefreshToken.Entity;

public class GetRefreshTokenQuery : IRequest<RefreshToken>
{
    public Guid Token { get; set; }
    public int UserId { get; set; }
}
