using System.Web;
using Application.Helpers;
using Application.Interfaces;
using Application.Responses;
using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Http.HttpResults;
using Newtonsoft.Json.Linq;
using Persistence.Context;

namespace Application.Services;

public class GoogleAuthService : BaseService, IGoogleAuthService
{
    public GoogleAuthService(TestContext dbTestContext, IMapper mapper) : base(dbTestContext, mapper) {}
    
    public async Task<BaseResponse> GetAuthUriAsync()
    {
        var parameters = new System.Collections.Specialized.NameValueCollection
        {
            { "response_type", "code" },
            { "client_id", UserHelpers.ClientId },
            { "redirect_uri", UserHelpers.RedirectUri },
            { "scope", "openid profile email" }, // Adjust scope as needed
            { "state", Guid.NewGuid().ToString("N") } // Optional: add a state parameter for security
        };

        var queryString = HttpUtility.ParseQueryString(string.Empty);
        queryString.Add(parameters);

        var response = new GoogleUriResponse
        {
            Uri = $"{UserHelpers.AuthUri}?{queryString}"
        };
        return Ok(response);
    }

    public async Task<BaseResponse> GetAccessTokenAsync(string authorizationCode)
    {
        using var httpClient = new HttpClient();

        var tokenRequestBody = new
        {
            code = authorizationCode,
            client_id = UserHelpers.ClientId,
            client_secret = UserHelpers.ClientSecret,
            redirect_uri = UserHelpers.RedirectUri,
            grant_type = "authorization_code"
        };

        var content = new FormUrlEncodedContent(JObject.FromObject(tokenRequestBody).ToObject<System.Collections.Generic.Dictionary<string, string>>());

        var response = await httpClient.PostAsync(UserHelpers.TokenUri, content);
        var responseContent = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            var tokenData = JObject.Parse(responseContent);
            var token = new TokenResponse
            {
                AccessToken = tokenData.GetValue("access_token")!.ToString()
            };
            return Ok(token);
        }
        else
        {
            return BadRequest($"Failed to retrieve access token: {responseContent}");
        }
    }

    
}