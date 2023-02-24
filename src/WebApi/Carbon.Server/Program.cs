using Carbon.Application;
using Carbon.Contracts.Routes.Server;
using Carbon.Infrastructure;
using Carbon.Server.Swagger;
using Carbon.Server.Configuration;
using Carbon.Server.Logging;

using Serilog;
using Carbon.Server.Authentication;
using Carbon.Server.Mapping;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Carbon.Server.HealthChecking;

var builder = WebApplication.CreateBuilder(args);
{
    // Добавляем конфигурации
    var configuration = builder.Configuration;
    {
        configuration.AddAppConfiguration(builder.Environment);
    }

    // Настраиваем хост
    var host = builder.Host;
    {
        host.UseSerilog(LoggingConfig.ConfigureLogging);
    }

    // Добавляем сервисы в DI контейнер
    var services = builder.Services;
    {
        services.AddApplication();
        services.AddInfrastructure(configuration);
        services.AddMapping();
        services.AddCarbonAuthentication(configuration);
        services.AddCarbonHealthChecking();

        services.AddControllers();
        services.AddEndpointsApiExplorer();

        if (builder.Environment.IsDevelopment())
        {
            services.AddSwaggerGen(options => options.ConfigureSwaggerGen(configuration));
        }
    }
}

// Pipeline приложения
var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseDeveloperExceptionPage();
    }
    else
    {
        app.UseExceptionHandler(ServerRoutes.Controllers.ErrorController);
    }

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapHealthChecks(ServerRoutes.HealthCheck, HealthCheckConfig.Options);
    app.MapControllers();
}

// Запуск приложения
app.Run();
