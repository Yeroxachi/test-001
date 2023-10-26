using Domain.Enums;

namespace Domain.Models;

public class User
{
    public Guid UserId { get; set; }
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public UserTypeId UserTypeId { get; set; }
}