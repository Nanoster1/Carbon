using ErrorOr;

using Mari.Server.Filters;

using Microsoft.AspNetCore.Mvc;

namespace Carbon.Server.Controllers.Common;

/// <summary>
/// Базовый класс для контроллеров <br/>
/// <list type="bullet">
/// <item>
/// Включает в себя <see cref="ApiControllerAttribute"/>
/// </item>
/// <item>
/// Защищён <see cref="CarbonAuthorizeFilter"/>
/// </item>
/// <item>
/// Содержит методы обработки ошибок типа <see cref="Error"/>
/// </item>
/// </list>
/// </summary>
[ApiController]
[CarbonAuthorizeFilter]
public abstract class ApiController : ControllerBase
{
    /// <summary>
    /// Обрабатывает ошибки <see cref="Error"/>
    /// </summary>
    /// <param name="errors">Список ошибок</param>
    /// <returns>Ответ с <see cref="ProblemDetails"/> или <see cref="ValidationProblemDetails"/></returns>
    protected ActionResult Problem(IList<Error> errors)
    {
        if (errors.Count is 0) return Problem();
        if (errors.All(e => e.Type == ErrorType.Validation)) return ValidationProblem(errors);
        return Problem(errors[0]);
    }

    /// <summary>
    /// Обрабатывает ошибки валидации <see cref="Error"/> с <see cref="ErrorType.Validation"/>
    /// </summary>
    /// <param name="errors">Список ошибок</param>
    /// <returns>Ответ с <see cref="ValidationProblemDetails"/></returns>
    private ActionResult ValidationProblem(IList<Error> errors)
    {
        foreach (var error in errors)
        {
            ModelState.AddModelError(error.Code, error.Description);
        }
        return ValidationProblem(ModelState);
    }

    /// <summary>
    /// Обрабатывает ошибку <see cref="Error"/>
    /// </summary>
    /// <param name="error">Ошибка</param>
    /// <returns>Ответ с <see cref="ProblemDetails"/> или <see cref="ValidationProblemDetails"/></returns>
    protected ActionResult Problem(Error error)
    {
        if (ErrorType.Validation == error.Type) return ValidationProblem(new[] { error });

        var statusCode = error.Type switch
        {
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            _ => 500
        };

        return Problem(title: error.Description, statusCode: statusCode);
    }
}