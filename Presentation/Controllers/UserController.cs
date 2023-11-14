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

    [HttpPost("CreateUser")]
    public async Task<ActionResult<BaseResponse<UserResponse>>> CreateUserAsync([FromBody] UserCreateDto dto)
    {
        var response = await _userService.CreateUserAsync(dto);
        return HandleRequest(response);
    }
    
    [HttpGet("GetAllUsers")]
    public async Task<ActionResult<BaseResponse<List<UserResponse>>>> GetAllUserAsync()
    {
        var response = await _userService.GetAllUsersAsync();
        return HandleRequest(response);
    }
    
    [HttpGet("GetUserById")]
    public async Task<ActionResult<BaseResponse<UserResponse>>> GetUserByIdAsync([FromQuery] Guid userId)
    {
        var response = await _userService.GetUserByIdAsync(userId);
        return HandleRequest(response);
    }

    [HttpDelete]
    public async Task<ActionResult<BaseResponse<UserResponse>>> DeleteUserAsync(Guid userId)
    {
        var response = await _userService.DeleteUserAsync(userId);
        return HandleRequest(response);
    }
}