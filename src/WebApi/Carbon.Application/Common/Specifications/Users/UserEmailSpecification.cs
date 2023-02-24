using System.Linq.Expressions;

using Carbon.Domain.Users;

using LinqSpecs;

namespace Carbon.Application.Common.Specifications.Users;

/// <summary>
/// Фильтрация по Email пользователя
/// </summary>
public sealed class UserEmailSpecification : Specification<User>
{
    public UserEmailSpecification(string email)
    {
        Email = email;
    }

    public string Email { get; }

    public override Expression<Func<User, bool>> ToExpression()
    {
        return user => user.Email == Email;
    }
}