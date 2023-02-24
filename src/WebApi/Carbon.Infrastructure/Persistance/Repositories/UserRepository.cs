using Carbon.Application.Common.Persistance.Repositories.Interfaces;
using Carbon.Core.Repo.EntityFrameworkCore.Services;
using Carbon.Domain.Users;
using Carbon.Domain.Users.ValueObjects;

namespace Carbon.Infrastructure.Persistance.Repositories;

/// <summary>
/// Реализация репозитория пользователей
/// </summary>
public sealed class UserRepository : EntityFrameworkRepository<User, UserId>, IUserRepository
{
    public UserRepository(CarbonDbContext context) : base(context)
    {
    }
}