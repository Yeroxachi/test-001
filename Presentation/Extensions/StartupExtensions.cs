using System.Text;
using Application;
using Application.Interfaces;
using Application.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
        serviceCollection.AddScoped<IGoogleAuthService, GoogleAuthService>();
        return serviceCollection;
    }
    
    public static IServiceCollection AddMapper(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddAutoMapper(expression => expression
            .AddMaps(nameof(Application)));
        return serviceCollection;
    }
    
    public static IServiceCollection AddJwtAndGoogleAuthentication(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer("JwtScheme", jwtOptions =>
            {
                var key = Encoding.UTF8.GetBytes(AuthOptions.Key);
                jwtOptions.SaveToken = true;
                jwtOptions.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = AuthOptions.Issuer,
                    ValidAudience = AuthOptions.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            })
            .AddGoogle("GoogleScheme", googleOptions =>
            {
                googleOptions.ClientId = "631581197553-e2l7ltmh336hjtob95uom02lufceu21j.apps.googleusercontent.com";
                googleOptions.ClientSecret = "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";
            });

        serviceCollection.AddAuthorization(options =>
        {
            options.DefaultPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .AddAuthenticationSchemes("JwtScheme", "GoogleScheme")
                .Build();
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