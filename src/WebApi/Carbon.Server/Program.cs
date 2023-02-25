using Carbon.Application;
using Carbon.Infrastructure;
using Carbon.Server.Swagger;
using Carbon.Server.Configuration;
using Carbon.Server.Logging;
using Carbon.Server.Authentication;
using Carbon.Server.Mapping;
using Carbon.Server.HealthChecking;
using Carbon.Contracts.Common.Routes.Server;
using Carbon.Server.ProblemDetails;

using Serilog;

using Hellang.Middleware.ProblemDetails;

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

        services.AddProblemDetails(options => options.ConfigureProblemDetails());
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
    app.UseProblemDetails();

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
