using AccessControl.Domain.Aggregates.UserAggregate;

namespace AccessControl.Api;

public interface IUserService
{
    Task<User> CreateUserAsync(UserCreateDto newUser, CancellationToken cancellationToken);
    Task<User> UpdateUserAsync(Guid userId, UserUpdatedDto updatedUser, CancellationToken cancellationToken);
    Task<User> GetUserAsync(Guid userId, CancellationToken cancellationToken);
    Task<IEnumerable<User>> GetAllUsersAsync(CancellationToken cancellationToken);
    Task DeleteUserAsync(Guid userId, CancellationToken cancellationToken);

}
