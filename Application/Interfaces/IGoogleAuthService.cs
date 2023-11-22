using Application.Responses;

namespace Application.Interfaces;

public interface IGoogleAuthService
{
    Task<BaseResponse> GetAuthUriAsync();
    Task<BaseResponse> GetAccessTokenAsync(string authorizationCode);
}