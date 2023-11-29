namespace AccessControl.Domain;

public interface IAuthorizationService
{
    Task<bool> AuthorizeAsync(Guid userId, string permission, CancellationToken cancellationToken);
}