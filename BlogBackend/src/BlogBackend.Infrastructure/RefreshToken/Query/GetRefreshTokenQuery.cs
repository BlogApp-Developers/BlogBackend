using MediatR;

namespace BlogBackend.Infrastructure.RefreshToken.Query;

public class GetRefreshTokenQuery : IRequest
{
    public Guid Token { get; set; }
    public int UserId { get; set; }
}
