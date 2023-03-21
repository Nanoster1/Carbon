using Carbon.Application.Authentication.Results;
using Carbon.Application.Common.Persistance.Repositories.Interfaces;
using Carbon.Application.Common.Services.Interfaces;
using Carbon.Application.Common.Services.Models;
using Carbon.Application.Common.Specifications.Users;
using Carbon.Core.Repo.Interfaces;
using Carbon.Domain.Common.Errors;
using Carbon.Domain.Users;
using Carbon.Domain.Users.ValueObjects;

using ErrorOr;

using MediatR;

namespace Carbon.Application.Authentication.Commands.Registration;

public sealed class RegistrationCommandHandler : IRequestHandler<RegistrationCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHashProvider _passwordHashProvider;
    private readonly IJwtTokenProvider _jwtTokenGenerator;
    private readonly IUnitOfWork _unitOfWork;

    public RegistrationCommandHandler(
        IUserRepository userRepository,
        IPasswordHashProvider passwordHashProvider,
        IJwtTokenProvider jwtTokenGenerator,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _passwordHashProvider = passwordHashProvider;
        _jwtTokenGenerator = jwtTokenGenerator;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(RegistrationCommand request, CancellationToken cancellationToken)
    {
        var userEmailSpec = new UserEmailSpecification(request.Email);
        var user = await _userRepository.GetOneAsync(userEmailSpec, cancellationToken);
        if (user != null) return Errors.User.UserWithThisEmailAlreadyExists;

        var hashedPassword = _passwordHashProvider.GetHash(request.Password);
        var userPassword = UserPassword.Create(hashedPassword.Hash, hashedPassword.Salt);

        var userCreateResult = User.Create(request.Username, request.Email, userPassword);
        if (userCreateResult.IsError) return userCreateResult.Errors;
        user = userCreateResult.Value;

        await _userRepository.CreateAsync(user, cancellationToken);
        await _unitOfWork.SaveChangesAsync();

        var userData = new JwtUserData(user.Id.ToString(), user.Username, user.Role.ToString());
        var accessToken = UserAccessToken.Create(_jwtTokenGenerator.GenerateToken(userData));
        user.ChangeAccessToken(accessToken);

        return new AuthenticationResult(accessToken);
    }
}
