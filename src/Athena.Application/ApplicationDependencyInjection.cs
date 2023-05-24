using System.Reflection;
using Athena.Application.Commons.Behaviors;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Athena.Application;

public static class ApplicationDependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        serviceCollection.AddAutoMapper(Assembly.GetExecutingAssembly());
        serviceCollection.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        serviceCollection.AddMediatR(configs =>
        {
            configs.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
        });

        serviceCollection.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehavior<,>));
        serviceCollection.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        serviceCollection.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehavior<,>));


        return serviceCollection;
    }
}