﻿using System.Reflection;
using EngTech.Application.Contract.Common.Behaviours;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace EngTech.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            cfg.AddBehavior(typeof(IPipelineBehavior<,>),
                typeof(UnhandledExceptionBehaviour<,>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        });
        return services;
    }

    public static void ResolveAllTypes(IServiceCollection services,
        ServiceLifetime serviceLifetime, Type refType, string suffix)
    {
        Assembly assembly = refType.GetTypeInfo().Assembly;

        IEnumerable<Type> allServices = assembly.GetTypes().Where(t =>
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
