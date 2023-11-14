namespace Domain.Models;

public class RefreshToken
{
    public string Token { get; set; } = null!;
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
}