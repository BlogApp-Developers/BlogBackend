namespace BlogBackend.Infrastructure.RefreshToken.Command;

public class CreateRefreshTokenCommand
{
    public Guid Token { get; set; }
    public int UserId { get; set; }
}
