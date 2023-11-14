using System.Collections.ObjectModel;
using Domain.Enums;

namespace Domain.Models;

public class User
{
    public Guid UserId { get; set; }
    public string Username { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public UserTypeId UserTypeId { get; set; }
    public string PasswordHash { get; set; } = null!;

    public ICollection<RefreshToken> RefreshTokens = new Collection<RefreshToken>();
}