namespace Application;

public static class AuthOptions
{
    public static string Key = "MySuperPuperUltraMaxLiteSecretKey123";
    public static string Issuer = "DefaultIssuer";
    public static string Audience = "DefaultAudience";
    public static int AccessTokenLifeTime = 60;
    public static int RefreshTokenLifeTime = 60*3*24;
}