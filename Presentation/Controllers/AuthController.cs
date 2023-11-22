using Application.DTO_s;
using Application.Interfaces;
using Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

public class AuthController : BaseController
{
    private readonly IAuthService _authService;
    private readonly IGoogleAuthService _googleAuthService;

    public AuthController(IAuthService authService, IGoogleAuthService googleAuthService)
    {
        _authService = authService;
        _googleAuthService = googleAuthService;
    }

    [HttpPost("Login")]
    public async Task<ActionResult<BaseResponse<TokenResponse>>> LoginAsync(LoginDto dto)
    {
        var response = await _authService.LoginAsync(dto);
        return HandleRequest(response);
    }

    [HttpGet("GetGoogleAuthUri")]
    public async Task<ActionResult<BaseResponse<GoogleUriResponse>>> GetGoogleAuthUriAsync()
    {
        var response = await _googleAuthService.GetAuthUriAsync();
        return HandleRequest(response);
    }
    
    [HttpGet("GoogleAuthCode")]
    public async Task<ActionResult<BaseResponse<TokenResponse>>> GoogleAuthCodeAsync(string authcode)
    {
        var response = await _googleAuthService.GetAccessTokenAsync(authcode);
        return HandleRequest(response);
    }
}