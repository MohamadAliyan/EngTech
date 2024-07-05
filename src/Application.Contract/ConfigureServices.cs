using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace EngTech.Application.Contract;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServicesContract(
        this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        //services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());


        return services;
    }
}
