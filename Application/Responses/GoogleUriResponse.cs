namespace Application.Responses;

public class GoogleUriResponse
{
    public string AuthType { get; set; } = "Google OAuth 2.0";
    public string Uri { get; set; }
}