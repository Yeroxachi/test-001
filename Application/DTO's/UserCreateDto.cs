using Domain.Enums;

namespace Application.DTO_s;

public record UserCreateDto
{
    public string UserName { get; set; } = null!;
    public string Name { get; init; } = null!;
    public string Surname { get; init; } = null!;
    public string Password { get; init; } = null!;
    public UserTypeId UserTypeId { get; init; }
}