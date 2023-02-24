using ErrorOr;

using FluentValidation.Results;

namespace Carbon.Core.MediatR.Extensions;

/// <summary>
/// Класс, содержащий методы расширения для <see cref="ValidationFailure"/>
/// </summary>
public static class ValidationFailureExtensions
{
    /// <summary>
    /// Преобразование <see cref="ValidationFailure"/> в список <see cref="Error"/>
    /// </summary>
    /// <param name="validationErrors">
    /// Ошибки валидации из <see cref="FluentValidation"/>
    /// </param>
    /// <returns>
    /// Список доменных ошибок
    /// </returns>
    public static List<Error> ToDomainErrors(this List<ValidationFailure> validationErrors)
    {
        return validationErrors
            .Select(x => Error.Validation(x.PropertyName, x.ErrorMessage))
            .ToList();
    }
}
