using Carbon.Core.Domain.Models.Interfaces;

namespace Carbon.Core.Domain.Exceptions;

/// <summary>
/// Исключение, выбрасываемое, если ValueObject не прошел валидацию <br/>
/// <see cref="IValidatable"/>
/// </summary>
public class ValueObjectIsNotValidException : Exception
{
    public ValueObjectIsNotValidException(string propertyName, string message = "")
        : base($"Value object is not valid. Property name: {propertyName}. {message}") { }
}
