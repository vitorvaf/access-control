using AccessControl.Domain.Aggregates.UserAggregate;
using AccessControl.Domain.Entities;
using AccessControl.Domain.Repositories;

namespace AccessControl.Api;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IGroupRepository _groupRepository;

    public UserService(IUserRepository userRepository, IRoleRepository roleRepository, IGroupRepository groupRepository)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _groupRepository = groupRepository;
    }
    public async Task<User> CreateUserAsync(UserCreateDto newUser, CancellationToken cancellationToken)
    {
        var user = new User(newUser.Name, newUser.Email, newUser.Password);

        var role = await _roleRepository.FindAsync(newUser.RoleId, cancellationToken);
        if (role == null)
            throw new Exception("Role not found");

        var group = await _groupRepository.FindAsync(newUser.GroupId, cancellationToken);
        if (group == null)
            throw new Exception("Group not found");

        user.JoinGroup(group);
        user.AddRole(role);

        await _userRepository.CreateAsync(user, cancellationToken);
        return user;
    }

    public async Task DeleteUserAsync(Guid userId, CancellationToken cancellationToken)
    {
        var user = await _userRepository.FindAsync(userId, cancellationToken);
        if (user == null)
            throw new Exception("User not found");

        await _userRepository.DeleteAsync(user, cancellationToken);
    }

    public Task<IEnumerable<User>> GetAllUsersAsync(CancellationToken cancellationToken)
    {
        /// TODO: Implement with pagination
        /// 
        throw new NotImplementedException();
    }

    public async Task<User> GetUserAsync(Guid userId, CancellationToken cancellationToken)
    {
        var user = await _userRepository.FindAsync(userId, cancellationToken);
        if (user == null)
            throw new Exception("User not found");
        
        return user;
    }

    public async Task<User> UpdateUserAsync(Guid userId, UserUpdatedDto updatedUser, CancellationToken cancellationToken)
    {
        var user = await _userRepository.FindAsync(userId, cancellationToken);
        if (user == null)
            throw new Exception("User not found");

        var role = await _roleRepository.FindAsync(updatedUser.RoleId, cancellationToken);
        if (role == null)
            throw new Exception("Role not found");

        var group = await _groupRepository.FindAsync(updatedUser.GroupId, cancellationToken);
        if (group == null)
            throw new Exception("Group not found");
        

        //TODO: Update user

        return Task.FromResult(user);
        
        
    }
}
