namespace BlogBackend.Infrastructure.RefreshToken.Command;

using MediatR;

public class DeleteRangeRefreshTokenCommand : IRequest
{
    public int UserId { get; set; }
}
