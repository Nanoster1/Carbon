namespace Carbon.Core.Domain.Exceptions;

public class ValueObjectIsNotValidException : Exception
{
    public ValueObjectIsNotValidException(string propertyName, string message = "")
        : base($"Value object is not valid. Property name: {propertyName}. {message}") { }
}