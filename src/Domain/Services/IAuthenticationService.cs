using AccessControl.Domain.ValueObjects;

namespace AccessControl.Domain;

public interface IAuthenticationService
{
    Task<Token> AuthenticateAsync(string username, string password, CancellationToken cancellationToken);
}