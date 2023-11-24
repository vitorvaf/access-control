using AccessControl.Domain.Aggregates.GroupAggragate;
using AccessControl.Domain.SeedWork;

namespace AccessControl.Domain.Aggregates.UserAggregate;
public class UserGroup : Entity
{
    public User User { get; private set; }
    public Group Group { get; private set; }

    public UserGroup(User user, Group group)
    {
        User = user;
        Group = group;
    }

    // Métodos e comportamentos específicos de UserGroup
}
