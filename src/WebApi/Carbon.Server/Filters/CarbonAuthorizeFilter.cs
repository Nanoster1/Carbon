using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Mari.Server.Filters;

/// <summary>
/// Фильтр авторизации, возвращающий в случае ошибки ответ в виде <see cref="ProblemDetails"/>
/// </summary>
public sealed class CarbonAuthorizeFilter : ActionFilterAttribute, IAsyncAuthorizationFilter
{
    public AuthorizationPolicy Policy { get; }

    public CarbonAuthorizeFilter()
    {
        Policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (context.Filters.Any(item => item is IAllowAnonymousFilter))
            return;

        var policyEvaluator = context.HttpContext.RequestServices.GetRequiredService<IPolicyEvaluator>();
        var authenticateResult = await policyEvaluator.AuthenticateAsync(Policy, context.HttpContext);
        var authorizeResult = await policyEvaluator.AuthorizeAsync(Policy, authenticateResult, context.HttpContext, context);

        if (authorizeResult.Challenged)
        {
            context.Result = new JsonResult(Unauthorized())
            {
                StatusCode = StatusCodes.Status401Unauthorized
            };
        }

        if (authorizeResult.Forbidden)
        {
            context.Result = new JsonResult(Forbidden())
            {
                StatusCode = StatusCodes.Status403Forbidden
            };
        }
    }

    private ProblemDetails Unauthorized()
    {
        const string title = "Unauthorized";
        const string detail = "You are not authorized to access this resource.";

        return new ProblemDetails()
        {
            Status = StatusCodes.Status401Unauthorized,
            Title = title,
            Detail = detail
        };
    }

    private ProblemDetails Forbidden()
    {
        const string title = "Forbidden";
        const string detail = "You are not allowed to access this resource.";

        return new ProblemDetails()
        {
            Status = StatusCodes.Status403Forbidden,
            Title = title,
            Detail = detail
        };
    }
}
