using Carbon.Application.Authentication.Results;
using Carbon.Application.Common.Persistance.Repositories.Interfaces;
using Carbon.Application.Common.Services.Interfaces;
using Carbon.Application.Common.Services.Models;
using Carbon.Application.Common.Specifications.Users;
using Carbon.Core.Repo.Interfaces;
using Carbon.Domain.Common.Errors;
using Carbon.Domain.Users.ValueObjects;

using ErrorOr;

using MediatR;

namespace Carbon.Application.Authentication.Queries.Login;

public sealed class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHashProvider _passwordHashProvider;
    private readonly IJwtTokenProvider _jwtTokenProvider;
    private readonly IUnitOfWork _unitOfWork;

    public LoginQueryHandler(
        IUserRepository userRepository,
        IPasswordHashProvider passwordHashProvider,
        IJwtTokenProvider jwtTokenGenerator,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _passwordHashProvider = passwordHashProvider;
        _jwtTokenProvider = jwtTokenGenerator;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var userEmailSpec = new UserEmailSpecification(request.Email);
        var user = await _userRepository.GetOneAsync(userEmailSpec, cancellationToken);
        if (user is null) return Errors.User.UserWithThisEmailDoesNotExist;

        var hashedPassword = _passwordHashProvider.GetHash(request.Password, user.Password.Salt);
        if (!string.Equals(hashedPassword.Hash, user.Password.Hash, StringComparison.Ordinal))
            return Errors.User.WrongPassword(nameof(request.Password));

        if (user.AccessToken == UserAccessToken.Default || _jwtTokenProvider.IsExpired(user.AccessToken))
        {
            var userData = new JwtUserData(user.Id.ToString(), user.Username, user.Role.ToString());
            var accessToken = UserAccessToken.Create(_jwtTokenProvider.GenerateToken(userData));

            user.ChangeAccessToken(accessToken);
            await _userRepository.UpdateAsync(user, cancellationToken);
            await _unitOfWork.SaveChangesAsync();
        }

        return new AuthenticationResult(user.AccessToken);
    }
}
