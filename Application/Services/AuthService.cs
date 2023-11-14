using System.Security.Claims;
using Application.DTO_s;
using Application.Helpers;
using Application.Interfaces;
using Application.Responses;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Application.Services;

public class AuthService : BaseService, IAuthService
{
    private readonly ITokenProvider _tokenProvider;

    public AuthService(TestContext dbTestContext, IMapper mapper, ITokenProvider tokenProvider) : base(dbTestContext, mapper)
    {
        _tokenProvider = tokenProvider;
    }

    public async Task<BaseResponse> LoginAsync(LoginDto login)
    {
        var password = UserHelpers.Hash(login.Password);
        var user = await DbTestContext.Users.FirstOrDefaultAsync(x =>
            x.Username == login.Username && x.PasswordHash == password);
        if (user is null)
        {
            return BadRequest("Username or password is incorrect");
        }

        var response = new TokenResponse
        {
            AccessToken = _tokenProvider.CreateToken(new List<Claim>(), AuthOptions.AccessTokenLifeTime),
            RefreshToken = _tokenProvider.CreateToken(new List<Claim>(), AuthOptions.RefreshTokenLifeTime)
        };
        return Ok(response);
    }

    public Task<BaseResponse> RefreshAsync(string refreshToken)
    {
        throw new NotImplementedException();
    }
}
