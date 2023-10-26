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

    public Task<BaseResponse> GetUserByIdAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    public Task<BaseResponse> GetAllUsersAsync()
    {
        throw new NotImplementedException();
    }

    public Task<BaseResponse> DeleteUserAsync(Guid userId)
    {
        throw new NotImplementedException();
    }
}