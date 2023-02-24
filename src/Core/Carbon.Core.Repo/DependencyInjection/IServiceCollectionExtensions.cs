using Carbon.Core.Repo.Interfaces;

using Microsoft.Extensions.DependencyInjection;

namespace Carbon.Core.Repo.DependencyInjection;

/// <summary>
/// Класс, содержащий методы расширения для <see cref="IServiceCollection"/>
/// </summary>
public static class IServiceCollectionExtensions
{
    /// <summary>
    /// Добавляет репозиторий в контейнер зависимостей <br/>
    /// Также добавляет зависимость от <see cref="IRepository{TSelf, TId}"/>
    /// </summary>
    /// <param name="services">
    /// Контейнер зависимостей
    /// </param>
    /// <typeparam name="TRepositoryService">
    /// Интерфейс репозитория
    /// </typeparam>
    /// <typeparam name="TImplementation">
    /// Реализация репозитория
    /// </typeparam>
    public static IServiceCollection AddRepository<TRepositoryService, TImplementation>(this IServiceCollection services)
        where TRepositoryService : class, IRepository
        where TImplementation : class, TRepositoryService
    {
        var genericRepositoryInterface = typeof(TRepositoryService).GetInterfaces()
            .FirstOrDefault(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IRepository<,>));

        if (genericRepositoryInterface is null)
            throw new ArgumentException($"{nameof(TRepositoryService)} must implement IRepository<,>");

        services.AddScoped(genericRepositoryInterface, typeof(TImplementation));
        services.AddScoped<TRepositoryService, TImplementation>();

        return services;
    }

    /// <summary>
    /// Добавляет реализацию <see cref="IUnitOfWork"/> в контейнер зависимостей
    /// </summary>
    /// <param name="services">
    /// Контейнер зависимостей
    /// </param>
    /// <typeparam name="TImplementation">
    /// Реализация <see cref="IUnitOfWork"/> 
    /// </typeparam>
    public static void AddUnitOfWork<TImplementation>(this IServiceCollection services)
        where TImplementation : class, IUnitOfWork
    {
        services.AddScoped<IUnitOfWork, TImplementation>();
    }
}