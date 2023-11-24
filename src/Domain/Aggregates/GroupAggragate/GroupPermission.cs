using AccessControl.Domain.Entities;
using AccessControl.Domain.SeedWork;

namespace AccessControl.Domain.Aggregates.GroupAggragate;

public class GroupPermission : Entity
{
    public Group Group { get; private set; }
    public Permission Permission { get; private set; }

    public GroupPermission(Group group, Permission permission)
    {
        Group = group;
        Permission = permission;
    }

    // Métodos e comportamentos específicos de GroupPermission
}

