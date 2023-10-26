using Application.DTO_s;
using Application.Responses;
using AutoMapper;
using Domain.Models;

namespace Application.Mappers;

public class UserMapperProfile : Profile
{
    public UserMapperProfile()
    {
        CreateMap<User, UserResponse>(MemberList.Source);
        CreateMap<User, UserCreateDto>(MemberList.Source).ReverseMap();
    }

}