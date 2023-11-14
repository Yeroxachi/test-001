using Application.DTO_s;
using Application.Interfaces;
using Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

public class AuthController : BaseController
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    
    [HttpPost]
    public async Task<ActionResult<BaseResponse<TokenResponse>>> LoginAsync(LoginDto dto)
    {
        var response = await _authService.LoginAsync(dto);
        return HandleRequest(response);
    }
}