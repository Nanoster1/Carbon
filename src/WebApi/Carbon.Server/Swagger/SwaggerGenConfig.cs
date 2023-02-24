using System.Reflection.PortableExecutable;

using Carbon.Core.FileSystem.Defaults;
using Carbon.Server.Swagger.Settings;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace Carbon.Server.Swagger;

/// <summary>
/// Настройка SwaggerGen
/// </summary>
public static class SwaggerGenConfig
{
    public static void ConfigureSwaggerGen(this SwaggerGenOptions options, IConfiguration configuration)
    {
        AddSwaggerDoc(options, configuration);
        AddSecurityDefinition(options, configuration);
        options.CustomSchemaIds(t => t.DeclaringType?.Name ?? t.Name);
        options.SupportNonNullableReferenceTypes();
    }

    public static void AddSecurityDefinition(SwaggerGenOptions options, IConfiguration config)
    {
        options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Enter token",
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = JwtBearerDefaults.AuthenticationScheme
        });

        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type=ReferenceType.SecurityScheme,
                        Id="Bearer"
                    }
                },
                new string[]{}
            }
        });
    }


    /// <summary>
    /// Добавляет документацию в Swagger
    /// </summary>
    private static void AddSwaggerDoc(SwaggerGenOptions options, IConfiguration configuration)
    {
        var swaggerGenOptions = configuration.GetSection(SwaggerGenSettings.SectionName)
            .Get<SwaggerGenSettings>() ?? throw new NullReferenceException();

        var assemblyName = typeof(SwaggerGenConfig).Assembly.GetName();

        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = swaggerGenOptions.Title,
            Description = swaggerGenOptions.Description,
            Version = assemblyName.Version?.ToString(),
            Contact = new OpenApiContact
            {
                Name = swaggerGenOptions.Contact.Name,
                Url = swaggerGenOptions.Contact.Url
            }
        });

        var xmlFile = $"{assemblyName.Name}{FileSystemDefaults.Extensions.Xml}";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        options.IncludeXmlComments(xmlPath);
    }
}
