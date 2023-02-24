using Mapster;

using MapsterMapper;

namespace Carbon.Server.Mapping;

public static class DependencyInjection
{
    /// <summary>
    /// Добавляет маппинг в DI контейнер
    /// </summary>
    /// <param name="services">
    /// DI контейнер
    /// </param>
    public static IServiceCollection AddMapping(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(typeof(DependencyInjection).Assembly);
        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();
        return services;
    }
}