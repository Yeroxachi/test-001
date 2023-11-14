using Application.DTO_s;
using Application.Responses;

namespace Application.Interfaces;

public interface IAuthService
{
    Task<BaseResponse> LoginAsync(LoginDto login);
    Task<BaseResponse> RefreshAsync(string refreshToken);
}