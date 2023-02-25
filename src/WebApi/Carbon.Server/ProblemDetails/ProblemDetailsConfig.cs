using ProblemDetailsOptions = Hellang.Middleware.ProblemDetails.ProblemDetailsOptions;

namespace Carbon.Server.ProblemDetails;

/// <summary>
/// Конфигурация для ответов в виде <see cref="ProblemDetails"/>
/// </summary>
public static class ProblemDetailsConfig
{
    public static void ConfigureProblemDetails(this ProblemDetailsOptions options)
    {
        // Не нужно добавлять описание исключений, для разработки используется DevelopingExceptionPage
        options.IncludeExceptionDetails = (context, ex) => false;
    }
}
