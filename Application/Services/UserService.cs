using Application.DTO_s;
using Application.Interfaces;
using Application.Responses;
using AutoMapper;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Application.Services;

public class UserService : BaseService, IUserService
{
    public UserService(TestContext dbTestContext, IMapper mapper) : base(dbTestContext, mapper)
    {
    }
    
    public async Task<BaseResponse> CreateUserAsync(UserCreateDto dto)
    {
        var user = await DbTestContext.Users.FirstOrDefaultAsync(x => x.Name == dto.Name && x.Surname == dto.Surname);
        if (user is not null)
        {
            return BadRequest("User Already Exist in System");
        }

        var newUser = Mapper.Map<User>(dto);
        await DbTestContext.Users.AddAsync(newUser);
        await DbTestContext.SaveChangesAsync();
        var response = Mapper.Map<UserResponse>(newUser);
        return Created(response);
    }

    public async Task<BaseResponse> GetUserByIdAsync(Guid userId)
    {
        var user = await DbTestContext.Users.FirstOrDefaultAsync(x => x.UserId == userId);
        if (user is null)
        {
            return NotFound();
        }

        var response = Mapper.Map<UserResponse>(user);
        return Ok(response);
    }

    public async Task<BaseResponse> GetAllUsersAsync()
    {
        var users = await DbTestContext.Users.ToListAsync();
        var response = Mapper.Map<List<UserResponse>>(users);

        return Ok(response);
    }

    public async Task<BaseResponse> DeleteUserAsync(Guid userId)
    {
        var user = await DbTestContext.Users.FirstOrDefaultAsync(x => x.UserId == userId);
        if (user is null)
        {
            return NotFound();
        }

        DbTestContext.Users.Remove(user);
        await DbTestContext.SaveChangesAsync();
        return Ok();
    }
}