using System.Text.Json;

using Carbon.Contracts.Common.Headers;
using Carbon.Core.DataRequests.Pagination;

using ErrorOr;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Carbon.Server.Controllers.Common;

/// <summary>
/// Базовый класс для контроллеров <br/>
/// <list type="bullet">
/// <item>
/// Включает в себя <see cref="ApiControllerAttribute"/>
/// </item>
/// <item>
/// Защищён <see cref="AuthorizeAttribute"/>
/// </item>
/// <item>
/// Содержит методы обработки ошибок типа <see cref="Error"/>
/// </item>
/// </list>
/// </summary>
[ApiController]
[Authorize]
public abstract class ApiController : ControllerBase
{
    #region Обработка ошибок
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
    #endregion

    #region Пагинация
    /// <summary>
    /// Выставляет заголовок ответа <see cref="Headers.Common.PaginationInfo"/> <br/>
    /// Необходимо использовать при наличии пагинации в запросе
    /// </summary>
    /// <param name="pageOutputInfo">
    /// Информация о пагинации
    /// </param>
    protected void SetPaginationHeader(PageOutputInfo pageOutputInfo)
    {
        Response.Headers.Add(Headers.Common.PaginationInfo, JsonSerializer.Serialize(pageOutputInfo));
    }
    #endregion
}
