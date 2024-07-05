using System.Reflection;
using EngTech.Application.Contract.Common.Models;
using EngTech.Domain.Common;
using EngTech.Infrastructure.Common;
using EngTech.Infrastructure.Persistence;
using EngTech.Infrastructure.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EngTech.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<AuditableEntitySaveChangesInterceptor>();

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                builder =>
                    builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly
                        .FullName)));
        services.AddScoped<ApplicationDbContextInitialiser>();

        services.Configure<AppSettings>(configuration);
        services.AddMemoryCache();

        ResolveAllTypes(services, ServiceLifetime.Scoped, typeof(BaseRepository<>),
            "Repository");
        services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));

        return services;
    }

    public static void ResolveAllTypes(IServiceCollection services,
        ServiceLifetime serviceLifetime, Type refType, string suffix)
    {
        Assembly assemblyCurrent = refType.GetTypeInfo().Assembly;
        IEnumerable<Type> allServices = assemblyCurrent.GetTypes().Where(t =>
            t.GetTypeInfo().IsClass && !t.GetTypeInfo().IsAbstract &&
            !t.GetType().IsInterface && t.Name.EndsWith(suffix));


        foreach (Type type in allServices)
        {
            Type[] allInterfaces = type.GetInterfaces();
            IEnumerable<Type> mainInterfaces =
                allInterfaces.Except(allInterfaces.SelectMany(t => t.GetInterfaces()));
            foreach (Type itype in mainInterfaces)
            {
                if (allServices.Any(x => !x.Equals(type) && itype.IsAssignableFrom(x)))
                {
                    throw new Exception("The " + itype.Name +
                                        " type has more than one implementations, please change your filter");
                }

                services.Add(new ServiceDescriptor(itype, type, serviceLifetime));
            }
        }
    }
}
