using AccessControl.Domain.Entities;
using AccessControl.Domain.Exceptions;
using AccessControl.Domain.SeedWork;

namespace AccessControl.Domain.Aggregates.GroupAggragate;

public class Group : Entity
{
    public string Name { get; private set; }

    private readonly List<GroupModule> _modules = new List<GroupModule>();
    public IReadOnlyCollection<GroupModule> Modules => _modules.AsReadOnly();

    private readonly List<GroupPermission> _permissions = new List<GroupPermission>();
    public IReadOnlyCollection<GroupPermission> Permissions => _permissions.AsReadOnly();

    public Group(string name)
    {
        Name = name;
    }

    public void AddModule(Module module)
    {
        var groupModule = new GroupModule(this, module);
        var goupModuleExists = _modules.Any(m => m.Module == module);

        if (goupModuleExists)
        {
            throw new DomainException($"O módulo {module.Name} já está associado ao grupo {Name}");
        }

        _modules.Add(groupModule);
    }

    public void RemoveModule(Module module)
    {
        var groupModule = _modules.FirstOrDefault(m => m.Module == module);
        if (groupModule != null)
        {
            _modules.Remove(groupModule);
        }
        else
        {
            throw new DomainException($"O módulo {module.Name} não está associado ao grupo {Name}");
        }
    }

    public void AddPermission(Permission permission)
    {
        var groupPermission = new GroupPermission(this, permission);
        var groupPermissionExists = _permissions.Any(p => p.Permission == permission);

        if(groupPermissionExists)
        {
            throw new DomainException($"A permissão {permission.Name} já está associada ao grupo {Name}.");
        }

        _permissions.Add(groupPermission);
    }

    public void RemovePermission(Permission permission)
    {
        var groupPermission = _permissions.FirstOrDefault(p => p.Permission == permission);
        if (groupPermission != null)
        {
            _permissions.Remove(groupPermission);
        }
        else
        {
            throw new DomainException($"A permissão {permission.Name} não está associada ao grupo {Name}.");
        }
    }

    // Outros métodos e comportamentos relevantes
}

