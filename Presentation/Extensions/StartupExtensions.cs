using System.Text;
using Application;
using Application.Interfaces;
using Application.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Presentation.Extensions;

public static class StartupExtensions
{
    public static IServiceCollection AddManagementServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IUserService, UserService>();
        serviceCollection.AddScoped<IAuthService, AuthService>();
        serviceCollection.AddScoped<ITokenProvider, TokenProvider>();
        return serviceCollection;
    }
    
    public static IServiceCollection AddMapper(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddAutoMapper(expression => expression
            .AddMaps(nameof(Application)));
        return serviceCollection;
    }
    
    public static IServiceCollection AddJwtAuthentication(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(o =>
        {
            var key = Encoding.UTF8.GetBytes(AuthOptions.Key);
            o.SaveToken = true;
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = AuthOptions.Issuer,
                ValidAudience = AuthOptions.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };
        });
        return serviceCollection;
    }
    
    public static IServiceCollection AddSwaggerSecurity(this IServiceCollection serviceCollection)
    {
        return serviceCollection.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = @"JWT Authorization header using the Bearer",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header
                    },
                    new List<string>()
                }
            });

            c.SwaggerDoc(name: "v1", new OpenApiInfo
            {
                Title = "InDebt",
                Version = "v1"
            });
        });
    }
}