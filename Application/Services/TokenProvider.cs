using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services;

public class TokenProvider : ITokenProvider
{
    public string CreateToken(IList<Claim> claims, int lifetime)
    {
        var expirationDate = DateTime.UtcNow.AddMinutes(lifetime);
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AuthOptions.Key));
        var securityToken =  new JwtSecurityToken(
            issuer: AuthOptions.Issuer,
            audience: AuthOptions.Audience,
            claims: claims,
            expires: expirationDate,
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
}