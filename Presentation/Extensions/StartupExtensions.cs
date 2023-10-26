using Application.Interfaces;
using Application.Services;

namespace Presentation.Extensions;

public static class StartupExtensions
{
    public static IServiceCollection AddManagementServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IUserService, UserService>();
        return serviceCollection;
    }
    
    public static IServiceCollection AddMapper(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddAutoMapper(expression => expression
            .AddMaps(nameof(Application)));
        return serviceCollection;
    }

}