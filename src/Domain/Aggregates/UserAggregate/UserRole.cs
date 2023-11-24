using AccessControl.Domain.Entities;
using AccessControl.Domain.SeedWork;

namespace AccessControl.Domain.Aggregates.UserAggregate;

public class UserRole : Entity
{
    public User User { get; private set; }
    public Role Role { get; private set; }

    public UserRole(User user, Role role)
    {
        User = user;
        Role = role;
    }

    // Métodos e comportamentos específicos de UserRole
}
