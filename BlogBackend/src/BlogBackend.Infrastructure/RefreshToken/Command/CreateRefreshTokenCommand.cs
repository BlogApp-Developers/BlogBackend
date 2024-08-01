namespace BlogBackend.Infrastructure.RefreshToken.Command;

using MediatR;

public class CreateRefreshTokenCommand : IRequest
{
    public Guid Token { get; set; }
    public int UserId { get; set; }
}
