using Carbon.Application.Common.Persistance.Repositories.Interfaces;
using Carbon.Application.Common.Services.Interfaces;
using Carbon.Core.Repo.DependencyInjection;
using Carbon.Infrastructure.Authentication.Services;
using Carbon.Infrastructure.Authentication.Settings;
using Carbon.Infrastructure.Persistance;
using Carbon.Infrastructure.Persistance.Repositories;
using Carbon.Infrastructure.Services;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Carbon.Infrastructure;

public static class DependencyInjection
{
    /// <summary>
    /// Добавление слоя инфраструктуры в контейнер сервисов
    /// </summary>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<CarbonDbContext>(options => CarbonDbContext.Configure(options, configuration));

        services.AddRepository<IUserRepository, UserRepository>();
        services.AddUnitOfWork<UnitOfWork>();

        services.AddScoped<IDateTimeProvider, DateTimeProvider>();
        services.AddScoped<IPasswordHashProvider, PasswordHashProvider>();
        services.AddScoped<IJwtTokenProvider, JwtTokenProvider>();

        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
        return services;
    }
}