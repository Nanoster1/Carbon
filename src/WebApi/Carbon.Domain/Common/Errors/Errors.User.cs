using ErrorOr;

namespace Carbon.Domain.Common.Errors;

public static partial class Errors
{
    public static class User
    {
        public static Error UserNotFound => Error.NotFound(description: "User not found");
        public static Error UserWithThisEmailAlreadyExists => Error.Conflict(description: $"User with this email already exists");
        public static Error UserWithThisEmailDoesNotExist => Error.NotFound(description: $"User with this email doesn't exist");
        public static Error WrongPassword(string propertyName) => Error.Validation(propertyName, $"Wrong password");
        public static Error UserAlreadyAuthenticated => Error.Conflict(description: "User already authenticated");
    }
}
