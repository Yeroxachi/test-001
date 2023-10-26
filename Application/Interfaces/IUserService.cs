using Application.DTO_s;
using Application.Responses;

namespace Application.Interfaces;

public interface IUserService
{
    Task<BaseResponse> CreateUserAsync(UserCreateDto dto);
    Task<BaseResponse> GetUserByIdAsync(Guid userId);
    Task<BaseResponse> GetAllUsersAsync();
    Task<BaseResponse> DeleteUserAsync(Guid userId);
}