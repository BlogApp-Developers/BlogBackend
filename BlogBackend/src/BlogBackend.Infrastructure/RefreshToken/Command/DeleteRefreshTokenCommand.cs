using MediatR;

namespace BlogBackend.Infrastructure.RefreshToken.Command;

public class DeleteRefreshTokenCommand : IRequest
{
    public Guid Token { get; set; }
    public int UserId { get; set; }
}
