using Carbon.Core.Repo.Interfaces;
using Carbon.Domain.Users;
using Carbon.Domain.Users.ValueObjects;

namespace Carbon.Application.Common.Persistance.Repositories.Interfaces;

public interface IUserRepository : IRepository<User, UserId>
{

}