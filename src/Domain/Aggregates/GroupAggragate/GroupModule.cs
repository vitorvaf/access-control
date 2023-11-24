using AccessControl.Domain.Entities;
using AccessControl.Domain.SeedWork;

namespace AccessControl.Domain.Aggregates.GroupAggragate;

public class GroupModule : Entity
{
    public Group Group { get; private set; }
    public Module Module { get; private set; }

    public GroupModule(Group group, Module module)
    {
        Group = group;
        Module = module;
    }

    // Métodos e comportamentos específicos de GroupModule
}

