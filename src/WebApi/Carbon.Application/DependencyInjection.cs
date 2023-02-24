using Carbon.Core.MediatR.Behaviors;

using FluentValidation;

using MediatR;

using Microsoft.Extensions.DependencyInjection;

namespace Carbon.Application;

public static class DependencyInjection
{
    /// <summary>
    /// Добавление слоя приложения в контейнер сервисов
    /// </summary>
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;

        // Pipeline обработки запросов
        services.AddMediatR(assembly);
        {
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        }

        services.AddValidatorsFromAssembly(assembly);
        return services;
    }
}