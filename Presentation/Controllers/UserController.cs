using Application.DTO_s;
using Application.Interfaces;
using Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

public class UserController : BaseController
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<ActionResult<BaseResponse<UserResponse>>> CreateUserAsync([FromBody] UserCreateDto dto)
    {
        var response = await _userService.CreateUserAsync(dto);
        return HandleRequest(response);
    }
}