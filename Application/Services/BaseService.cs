using Application.Responses;
using AutoMapper;
using Domain.Enums;
using Persistence.Context;

namespace Application.Services;

public abstract class BaseService
{
    protected readonly TestContext DbTestContext;
    protected readonly IMapper Mapper;

    protected BaseService(TestContext dbTestContext, IMapper mapper)
    {
        DbTestContext = dbTestContext;
        Mapper = mapper;
    }
    
    protected BaseResponse Ok()
    {
        return new BaseResponse();
    }

    protected BaseResponse<TE> Ok<TE>(TE data) where TE : class
    {
        return new BaseResponse<TE>(data);
    }

    protected BaseResponse Created()
    {
        return new BaseResponse(ResponseCode.Created);
    }

    protected BaseResponse<TE> Created<TE>(TE data) where TE : class
    {
        return new BaseResponse<TE>(data, ResponseCode.Created);
    }

    protected BaseResponse UnAuthorize()
    {
        return new BaseResponse(ResponseCode.UnAuthorize);
    }

    protected BaseResponse<TE> UnAuthorize<TE>() where TE : class
    {
        return new BaseResponse<TE>(code: ResponseCode.UnAuthorize);
    }

    protected BaseResponse<TE> Forbid<TE>() where TE : class
    {
        return new BaseResponse<TE>(code: ResponseCode.Forbidden);
    }

    protected BaseResponse Forbid()
    {
        return new BaseResponse(ResponseCode.Forbidden);
    }

    protected BaseResponse NotFound()
    {
        return new BaseResponse(ResponseCode.NotFound);
    }

    protected BaseResponse<TE> NotFound<TE>() where TE : class
    {
        return new BaseResponse<TE>(code: ResponseCode.NotFound);
    }

    protected BaseResponse BadRequest(string message)
    {
        return new BaseResponse(ResponseCode.BadRequest, errorMessage: message);
    }

    protected BaseResponse<TE> BadRequest<TE>(string message) where TE : class
    {
        return new BaseResponse<TE>(code: ResponseCode.BadRequest, errorMessage: message);
    }
}
