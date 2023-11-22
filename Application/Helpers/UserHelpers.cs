using System.Security.Cryptography;
using System.Text;

namespace Application.Helpers;

public static class UserHelpers
{
    public const string ClientId = "631581197553-e2l7ltmh336hjtob95uom02lufceu21j.apps.googleusercontent.com";
    public const string ClientSecret = "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";
    public const string RedirectUri = "https://localhost:7172/api/Auth/GoogleAuthCode";
    public const string AuthUri = "https://accounts.google.com/o/oauth2/auth";
    public const string TokenUri = "https://oauth2.googleapis.com/token";
    public static string Hash(string rawData)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(rawData));
        var builder = new StringBuilder();
        foreach (var t in bytes)
        {
            builder.Append(t.ToString("x2"));
        }

        return builder.ToString();
    }
}