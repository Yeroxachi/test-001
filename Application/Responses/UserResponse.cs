using Domain.Enums;

namespace Application.Responses;

public record UserResponse
{
    public Guid UserId { get; init; }
    public string Name { get; init; } = null!;
    public string Surname { get; init; } = null!;
    public UserTypeId UserTypeId { get; init; }
}